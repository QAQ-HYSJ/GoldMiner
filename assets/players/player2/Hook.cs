using Godot;
using System;

namespace SpacePlayer2;

public enum HookMode { go, back, wave }

public partial class Hook : Node2D
{
	[Signal] public delegate void ModeChangedEventHandler(HookMode from, HookMode to);
	public HookMode HookStatus;
	private Vector2 direction;
	public override void _Ready()
	{
		HookStatus = HookMode.wave;
	}

	public void GoHook()   // 出钩
	{
		if (HookStatus == HookMode.wave)
			ChangeMode(HookMode.go);
	}

	public override void _Process(double delta)
	{
		switch (HookStatus)
		{
			case HookMode.wave: HookWave(); break;
			case HookMode.go: HookGo(delta); break;
			case HookMode.back: HookBack(delta); break;
		}
	}

	private void ChangeMode(HookMode status)
	{
		EmitSignal(SignalName.ModeChanged, new Variant[] { (int)HookStatus, (int)status });
		HookStatus = status;
	}
	private void HookWave()   // wave状态处理
	{

	}
	private void HookGo(double delta)  // go状态处理
	{
		Position += (float)delta * direction * 200f;
	}
	private void HookBack(double delta)    // back 状态处理
	{
		Position -= (float)delta * direction * 200f;
	}
	private void _on_mode_changed(HookMode from, HookMode to)
	{
		switch (from)  // 状态结术
		{
		}
		switch (to)    // 状态开始
		{
			case HookMode.wave:
				GetNode<AnimationPlayer>("HookAnimation").Play();
				break;
			case HookMode.go:
				direction = new Vector2((float)-Math.Sin(Rotation), (float)Math.Cos(Rotation)).Normalized();
				GetNode<AnimationPlayer>("HookAnimation").Pause();
				break;
		}
	}
	private void _on_timer_timeout()
	{
		if (HookStatus == HookMode.go)
			ChangeMode(HookMode.back);
	}
}
