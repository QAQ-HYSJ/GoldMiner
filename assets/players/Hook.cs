using Godot;
using System;

public enum HookMode { go, back, wave }

public partial class Hook : Node2D
{
	[Signal] public delegate void ModeChangedEventHandler(HookMode from, HookMode to);

	public HookMode HookStatus;
	private Vector2 direction;
	public Vector2 OriginPoint { get; } = new Vector2(-7, 11);
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
				Position += (float)delta * direction * 110f;
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
				}
				break;
		}
	}
	private void On_Timer_Timeout()
	{
		if (HookStatus == HookMode.go)
			SwitchMode(HookMode.back);
	}
	private void On_HitBox_AreaEntered(Area2D area)
	{
		Item item = area as Item;
		Sprite2D sprite = new Sprite2D
		{
			Texture = area.GetNode<Sprite2D>("Sprite2D").Texture,
			Position = item.Properties.Offect,
			ZIndex = -1
		};
		ItemSlot.AddChild(sprite);
		SwitchMode(HookMode.back);
		HookHasItem = true;
		ItemValue = item.Properties.Value;
		ItemWeight = item.Properties.Weight;
		switch (item.Properties.valueLevel)
		{
			case ItemProperties.ValueLevel.low: GetNode<AudioStreamPlayer>("LowValue").Play(); break;
			case ItemProperties.ValueLevel.mid: GetNode<AudioStreamPlayer>("MidValue").Play(); break;
			case ItemProperties.ValueLevel.high: GetNode<AudioStreamPlayer>("HighValue").Play(); break;
		}
		switch (item.Properties.sizeLevel)
		{
			case ItemProperties.SizeLevel.big: GetNode<Sprite2D>("Sprite").Frame = 1; break;
			case ItemProperties.SizeLevel.small: GetNode<Sprite2D>("Sprite").Frame = 2; break;
		}

		area.QueueFree();
	}
}
