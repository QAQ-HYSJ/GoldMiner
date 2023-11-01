using Godot;
using System;
using System.Collections.Generic;

public partial class Hook : Node2D
{
	public HookMode HookStatus;
	private Vector2 direction;
	public Vector2 OriginPoint { get; } = new Vector2(-7, 11);
	private List<Item> items = new List<Item>();                // 钩中了物品，将其加入列表中，再进行处理，防止钩中多个
	private bool pause = false;
	private Node2D ItemSlot;
	private bool HookHasItem = false;
	private int ItemValue = 0;
	private Type itemType;
	private int _itemWeight = 0;
	private int ItemWeight
	{
		set { _itemWeight = Mathf.Clamp(value, 0, 100); }
		get { return _itemWeight; }
	}
	private PackedScene ExplosionTree;
	private Texture2D DynamiteUI;
	private Texture2D StrengthUI;
	public override void _Ready()
	{
		GetParent<Player>().Animation = "default";
		HookStatus = HookMode.wave;
		ItemSlot = GetNode<Node2D>("ItemSlot");
		ExplosionTree = ResourceLoader.Load<PackedScene>("res://assets/players/Explosion.tscn");
		DynamiteUI = ResourceLoader.Load<Texture2D>("res://resources/images/ui_dynamite.png");
		StrengthUI = ResourceLoader.Load<Texture2D>("res://resources/images/text_strength.png");
	}

	public void GoHook()   // 出钩
	{
		if (HookStatus == HookMode.wave)
			SwitchMode(HookMode.go);
	}
	public async void ThrowDynamite()
	{
		if (HookStatus == HookMode.back && HookHasItem && GetParent<Player>().DynamiteNum > 0)
		{
			PlayThrowDynamiteAnimation();  // 因为有两个await所以放后面
			HookHasItem = false;
			ItemValue = 0;
			ItemWeight = 0;
			foreach (Node x in ItemSlot.GetChildren())
			{
				x.QueueFree();
			}
			GetNode<Sprite2D>("Sprite").Frame = 0;
			GetParent<Player>().DynamiteNum--;
			GetNode<AudioStreamPlayer>("Explosion").Play();
			AnimatedSprite2D ExplosionAnimation = ExplosionTree.Instantiate<AnimatedSprite2D>();
			ExplosionAnimation.GlobalPosition = GlobalPosition;
			GetTree().CurrentScene.AddChild(ExplosionAnimation);
			await ToSignal(ExplosionAnimation, AnimatedSprite2D.SignalName.AnimationFinished);
			ExplosionAnimation.QueueFree();
		}
	}
	private async void PlayThrowDynamiteAnimation()
	{
		GetParent<Player>().Play("throw_dynamite");
		await ToSignal(GetParent<Player>(), Player.SignalName.AnimationFinished);
		GetParent<Player>().Animation = "default";
	}
	public override async void _Process(double delta)
	{
		switch (HookStatus)
		{
			case HookMode.wave:
				break;
			case HookMode.go:
				{
					if (GetParent<Player>().StrengthBuff)
						Position += (float)delta * direction * 200f;
					else
						Position += (float)delta * direction * 110f;

					if (items.Count != 0)                    // 绶存的列表中有钩中的物品则进行处理，不止一个则只处理一个
					{
						HookThing(items[0]);
						items.Clear();
					}

					if ((Position - OriginPoint).Length() >= 220)
					{
						SwitchMode(HookMode.back);
					}
				}
				break;
			case HookMode.back:
				{
					if (!pause)
					{
						if (GetParent<Player>().StrengthBuff)
							Position -= (float)delta * direction * 200f * ((100 - ItemWeight) / 100f);
						else
							Position -= (float)delta * direction * 110f * ((100 - ItemWeight) / 100f);

						if (Position.Y <= OriginPoint.Y)
						{
							pause = true;
							GetParent<Player>().Animation = "default";
							GetParent<Player>().Pause();
							GetParent<Player>().Frame = 0;
							GetNode<AudioStreamPlayer>("BackHook").Stop();
							if (HookHasItem)
							{
								HandleItem();  // 用到两个await，要放后面
							}
							await ToSignal(GetTree().CreateTimer(0.5), SceneTreeTimer.SignalName.Timeout);
							SwitchMode(HookMode.wave);
							pause = false;
						}
					}
				}
				break;
		}
	}
	private void SwitchMode(HookMode status)
	{
		ModeChanged(HookStatus, status);
		HookStatus = status;
	}
	private async void ModeChanged(HookMode from, HookMode to)
	{
		switch (from)  // 状态结术
		{
			case HookMode.wave:
				break;
			case HookMode.go:
				break;
			case HookMode.back:
				{
					if (HookHasItem)
					{
						switch (itemType)
						{
							case Type.Dynamite:
								{
									GetParent<Player>().DynamiteNum++;
								}
								break;
							case Type.Strength:
								{
									GetParent<Player>().StrengthBuff = true;
								}
								break;
							default:
								{
									Global.Money += ItemValue;
								}
								break;   // 默认情况即itemType为Money
						}
					}
					foreach (Node x in ItemSlot.GetChildren())
					{
						x.QueueFree();
					}
					GetNode<AudioStreamPlayer>("BackHook").Stop();
					GetNode<Area2D>("HitBox").Monitorable = true;
					GetNode<Area2D>("HitBox").Monitoring = true;
				}
				break;
		}
		switch (to)    // 状态开始
		{
			case HookMode.wave:
				{
					Position = OriginPoint;
					GetNode<AnimationPlayer>("HookAnimation").Play();
					GetNode<AudioStreamPlayer>("HookReset").Play();
					HookHasItem = false;
					ItemValue = 0;
					ItemWeight = 0;
					if (GetParent<Player>().Animation != "default")
					{
						await ToSignal(GetParent<Player>(), Player.SignalName.AnimationFinished);
					}
					GetParent<Player>().Animation = "default";
					GetParent<Player>().Pause();
					GetParent<Player>().Frame = 0;
					GetNode<Sprite2D>("Sprite").Frame = 0;
				}
				break;
			case HookMode.go:
				{
					direction = new Vector2((float)-Math.Sin(Rotation), (float)Math.Cos(Rotation)).Normalized();
					GetNode<AnimationPlayer>("HookAnimation").Pause();
					GetParent<Player>().Animation = "default";
					GetParent<Player>().Pause();
					GetParent<Player>().Frame = 2;
					GetNode<AudioStreamPlayer>("GoHook").Play();
				}
				break;
			case HookMode.back:
				{
					GetParent<Player>().Play("default");
					GetNode<AudioStreamPlayer>("BackHook").Play();
					CallDeferred(MethodName.BokehHook);             // 延迟调用虚化钩子，否则碰撞时会报错
				}
				break;
		}
	}
	private void On_HitBox_AreaEntered(Area2D area)             // 抓到物体，钩中的物体加入列表中，而不是直接处理，防止钩中多个物体
	{
		Item item = area as Item;
		items.Add(item);
	}
	private void BokehHook()    // 虚化钩子
	{
		GetNode<Area2D>("HitBox").Monitorable = false;
		GetNode<Area2D>("HitBox").Monitoring = false;
	}
	private void HookThing(Item item)   //钩中物品时的处理
	{
		if (item is Mouse)      // 如果是老鼠
		{
			AnimatedSprite2D animatedSprite2D = new AnimatedSprite2D
			{
				SpriteFrames = (SpriteFrames)item.GetNode<AnimatedSprite2D>("AnimatedSprite2D").SpriteFrames.Duplicate(),
				Position = item.Offect,
				ZIndex = -1
			};
			ItemSlot.AddChild(animatedSprite2D);
			animatedSprite2D.SpriteFrames.SetAnimationSpeed("default", 30);
			animatedSprite2D.Play();
		}
		else if (item is DiamondMouse)      // 如果是钻石鼠
		{
			AnimatedSprite2D animatedSprite2D = new AnimatedSprite2D
			{
				SpriteFrames = (SpriteFrames)item.GetNode<AnimatedSprite2D>("AnimatedSprite2D").SpriteFrames.Duplicate(),
				Position = item.Offect,
				ZIndex = -1
			};
			ItemSlot.AddChild(animatedSprite2D);
			animatedSprite2D.SpriteFrames.SetAnimationSpeed("default", 30);
			animatedSprite2D.Play();
		}
		else if (item is TNT tnt)   // 如果是炸药
		{
			Sprite2D sprite = new Sprite2D
			{
				Texture = tnt.texture,
				Position = item.Offect,
				ZIndex = -1
			};
			ItemSlot.AddChild(sprite);
			tnt.Explosion();
		}
		else if (item is Item)  // 如果是普通物体
		{
			Sprite2D sprite = new Sprite2D
			{
				Texture = item.GetNode<Sprite2D>("Sprite2D").Texture,
				Position = item.Offect,
				ZIndex = -1
			};
			ItemSlot.AddChild(sprite);
		}

		SwitchMode(HookMode.back);
		HookHasItem = true;
		ItemValue = item.Value;
		ItemWeight = item.Weight;
		switch (item.valueLevel)     // 播放音效
		{
			case Item.ValueLevel.low: GetNode<AudioStreamPlayer>("LowValue").Play(); break;
			case Item.ValueLevel.mid: GetNode<AudioStreamPlayer>("MidValue").Play(); break;
			case Item.ValueLevel.high: GetNode<AudioStreamPlayer>("HighValue").Play(); break;
		}
		switch (item.sizeLevel)      // 改变钩子形状
		{
			case Item.SizeLevel.big: GetNode<Sprite2D>("Sprite").Frame = 1; break;
			case Item.SizeLevel.small: GetNode<Sprite2D>("Sprite").Frame = 2; break;
		}

		if (item is Bag bag)
		{
			itemType = bag.type;
		}
		else
		{
			itemType = Type.Money;
		}

		if (item is not TNT)                    // 如果是tnt，则让它自己爆炸
			item.QueueFree();
	}
	private async void HandleItem()
	{
		GetNode<AudioStreamPlayer>("MoneyGain").Play();
		switch (itemType)
		{
			case Type.Dynamite:
				{
					Sprite2D sprite2D = new Sprite2D();
					sprite2D.Texture = DynamiteUI;
					sprite2D.Scale = new Vector2(1.5f, 1.5f);
					AddChild(sprite2D);
					sprite2D.Position = new Vector2(0, 20);
					sprite2D.GlobalRotation = 0;
					await ToSignal(GetTree().CreateTimer(0.5), SceneTreeTimer.SignalName.Timeout);
					Tween tween = GetTree().CreateTween();
					tween.Parallel().TweenProperty(sprite2D, "scale", new Vector2(0.5f, 0.5f), 0.3);
					tween.Parallel().TweenProperty(sprite2D, "global_position", GetParent<Node2D>().Position + new Vector2(18, 13), 0.3);
					tween.TweenCallback(Callable.From(sprite2D.QueueFree));
				}
				break;
			case Type.Strength:
				{
					GetParent<Player>().Play("strength");
					GetNode<AudioStreamPlayer>("HighValue").Play();
					Sprite2D sprite2D = new Sprite2D();
					sprite2D.Texture = StrengthUI;
					GetParent().AddChild(sprite2D);
					sprite2D.Position = new Vector2(-40, 0);
					await ToSignal(GetParent<Player>(), Player.SignalName.AnimationFinished);
					sprite2D.QueueFree();
				}
				break;
			default:
				{
					Label label = new Label();
					label.Text = "$" + ItemValue;
					label.GlobalPosition = GlobalPosition - new Vector2(0, 10);
					label.Set("theme_override_colors/font_color", Colors.Green);
					label.Set("theme_override_font_sizes/font_size", 12);
					GetTree().CurrentScene.AddChild(label);
					await ToSignal(GetTree().CreateTimer(0.5), SceneTreeTimer.SignalName.Timeout);
					Tween tween = GetTree().CreateTween();
					tween.Parallel().TweenProperty(label, "scale", new Vector2(0.5f, 0.5f), 0.5);
					tween.Parallel().TweenProperty(label, "global_position", new Vector2(60, 6), 0.5);
					await ToSignal(tween, Tween.SignalName.Finished);
					label.QueueFree();
				}
				break;   // 默认情况即itemType为Money
		}
	}

}
