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
	public override void _Ready()
	{
		Global.player2Hook = this;
		HookStatus = HookMode.wave;
	}

	public void GoHook()   // 出钩
	{
		if (HookStatus == HookMode.wave)
			ChangeMode(HookMode.go);
	}

	private void ChangeMode(HookMode status)
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
							ChangeMode(HookMode.wave);
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

				}
				break;
		}
		switch (to)    // 状态开始
		{
			case HookMode.wave:
				{
					Position = OriginPoint;
					GetNode<AnimationPlayer>("HookAnimation").Play();
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
			ChangeMode(HookMode.back);
	}
}
