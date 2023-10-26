using Godot;
using System;
using System.Collections.Generic;

public enum HookMode { go, back, wave }

public partial class Hook : Node2D
{
	[Signal] public delegate void ModeChangedEventHandler(HookMode from, HookMode to);
	public bool StrengthBuff = false;
	public HookMode HookStatus;
	private Vector2 direction;
	public Vector2 OriginPoint { get; } = new Vector2(-7, 11);
	private List<Item> items = new List<Item>();				// 钩中了物品，将其加入列表中，再进行处理，防止钩中多个
	private bool pause = false;
	private Node2D ItemSlot;
	private bool HookHasItem = false;
	private int ItemValue = 0;
	private int _itemWeight = 0;
	private int ItemWeight
	{
		set { _itemWeight = Mathf.Clamp(value, 0, 100); }
		get { return _itemWeight; }
	}
	private AnimatedSprite2D Player;
	public override void _Ready()
	{
		HookStatus = HookMode.wave;
		ItemSlot = GetNode<Node2D>("ItemSlot");
		Player = GetParent<AnimatedSprite2D>();
	}

	public void GoHook()   // 出钩
	{
		if (HookStatus == HookMode.wave)
			SwitchMode(HookMode.go);
	}

	private void SwitchMode(HookMode status)
	{
		EmitSignal(SignalName.ModeChanged, new Variant[] { (int)HookStatus, (int)status });
		HookStatus = status;
	}

	public override async void _Process(double delta)
	{
		switch (HookStatus)
		{
			case HookMode.wave:
				break;
			case HookMode.go:
				{
					Position += (float)delta * direction * 110f;
					if (items.Count != 0)           		 // 绶存的列表中有钩中的物品则进行处理，不止一个则只处理一个
					{
						HookThing(items[0]);
						items.Clear();
					}
				}
				break;
			case HookMode.back:
				{
					if (!pause)
					{
						Position -= (float)delta * direction * 110f * ((100 - ItemWeight) / 100f);
						if (Position.Y <= OriginPoint.Y)
						{
							pause = true;
							Player.Pause();
							Player.Frame = 0;
							GetNode<AudioStreamPlayer>("BackHook").Stop();
							if (HookHasItem)
							{
								GetNode<AudioStreamPlayer>("MoneyGain").Play();
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
	private void On_ModeChanged(HookMode from, HookMode to)
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
						Global.Money += ItemValue;
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
					Player.Pause();
					Player.Frame = 0;
					GetNode<Sprite2D>("Sprite").Frame = 0;
				}
				break;
			case HookMode.go:
				{
					GetNode<Timer>("Timer").Start();
					direction = new Vector2((float)-Math.Sin(Rotation), (float)Math.Cos(Rotation)).Normalized();
					GetNode<AnimationPlayer>("HookAnimation").Pause();
					Player.Pause();
					Player.Frame = 2;
					GetNode<AudioStreamPlayer>("GoHook").Play();
				}
				break;
			case HookMode.back:
				{
					Player.Play();
					GetNode<AudioStreamPlayer>("BackHook").Play();
					CallDeferred(MethodName.BokehHook);             // 延迟调用虚化钩子，否则碰撞时会报错
				}
				break;
		}
	}
	private void On_Timer_Timeout()
	{
		if (HookStatus == HookMode.go)
			SwitchMode(HookMode.back);
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
				SpriteFrames = item.GetNode<AnimatedSprite2D>("AnimatedSprite2D").SpriteFrames,
				Position = item.Offect,
				ZIndex = -1
			};
			ItemSlot.AddChild(animatedSprite2D);
			animatedSprite2D.SpriteFrames.SetAnimationSpeed("default", 30);
			animatedSprite2D.Play();
		}else if (item is DiamondMouse)      // 如果是老鼠
		{
			AnimatedSprite2D animatedSprite2D = new AnimatedSprite2D
			{
				SpriteFrames = item.GetNode<AnimatedSprite2D>("AnimatedSprite2D").SpriteFrames,
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

		if (item is not TNT)                    // 如果是tnt，则让它自己爆炸
			item.QueueFree();
	}
}
