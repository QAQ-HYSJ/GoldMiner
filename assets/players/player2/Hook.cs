using Godot;
using System;

namespace SpacePlayer2;

public enum HookMode { go, back, wave }

public partial class Hook : Node2D
{
	[Signal] public delegate void ModeChangedEventHandler(HookMode from, HookMode to);

	public HookMode HookStatus;
	private Vector2 direction;
	private Vector2 OriginPoint { get; } = new Vector2(-7, 11);
	private bool pause = false;
	private Node2D ItemSlot;
	private bool HookHasItem = false;
	private int ItemValue = 0;
	private Player2 Player;
	public override void _Ready()
	{
		Global.player2Hook = this;
		HookStatus = HookMode.wave;
		ItemSlot = GetNode<Node2D>("ItemSlot");
		Player = GetParent<Player2>();
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
				Position += (float)delta * direction * 200f;
				break;
			case HookMode.back:
				{
					if (!pause)
					{
						Position -= (float)delta * direction * 200f;
						if (Position.Y <= OriginPoint.Y)
						{
							pause = true;
							await ToSignal(GetTree().CreateTimer(0.5), SceneTreeTimer.SignalName.Timeout);
							SwitchMode(HookMode.wave);
							pause = false;
						}
					}
				}
				break;
		}
	}
	private void _on_mode_changed(HookMode from, HookMode to)
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
						Player.Money += ItemValue;
					foreach (Node x in ItemSlot.GetChildren())
					{
						x.QueueFree();
					}
				}
				break;
		}
		switch (to)    // 状态开始
		{
			case HookMode.wave:
				{
					Position = OriginPoint;
					GetNode<AnimationPlayer>("HookAnimation").Play();
					HookHasItem = false;
					ItemValue = 0;
				}
				break;
			case HookMode.go:
				{
					GetNode<Timer>("Timer").Start();
					direction = new Vector2((float)-Math.Sin(Rotation), (float)Math.Cos(Rotation)).Normalized();
					GetNode<AnimationPlayer>("HookAnimation").Pause();
				}
				break;
			case HookMode.back:
				break;
		}
	}
	private void _on_timer_timeout()
	{
		if (HookStatus == HookMode.go)
			SwitchMode(HookMode.back);
	}
	private void _on_hit_box_area_entered(Area2D area)
	{
		Sprite2D sprite = new Sprite2D();
		sprite.Texture = area.GetNode<Sprite2D>("Sprite2D").Texture;
		area.QueueFree();
		ItemSlot.AddChild(sprite);
		SwitchMode(HookMode.back);
		HookHasItem = true;
		ItemValue = (area as Item).Properties.Value;
	}
}
