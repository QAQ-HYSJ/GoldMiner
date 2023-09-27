using Godot;
using System;

public partial class Hook : Marker2D
{
	public int Weight = 0;
	private bool _goback = true;
	private Vector2 _direction;
	private bool _hooking;
	public void GoHook()  // 出钩
	{
		if (!_hooking)
		{
			_direction = new Vector2((float)-Math.Sin(Rotation), (float)Math.Cos(Rotation)).Normalized();
			_hooking = true;
			_goback = true;

			GetParent<Player1>().Pause();
			GetParent<Player1>().Frame = 2;
			GetNode<AnimationPlayer>("AnimationPlayer").Pause();
			GetNode<Timer>("Timer").Start();

			GetNode<AudioStreamPlayer>("GrabStart").Play();
		}
	}

	public override void _Ready()
	{
		GetNode<Timer>("Timer").Timeout += OnBack;
		GetNode<Area2D>("HitBox").AreaEntered += (@object) => OnAreaEntered(@object);
		Global.hook = this;
		GetParent<Player1>().Pause();
		GetParent<Player1>().Frame = 0;
	}
	public override async void _Process(double delta)
	{
		QueueRedraw();
		if (_hooking)
		{
			if (_goback)  // 出钩
			{
				Position += (float)delta * _direction * 200f;
			}
			else         // 回钩
			{
				Position += -(float)delta * _direction * (200f - Weight);
			}

			if (Position.Y <= 10)
			{
				Position = new Vector2(-7, 10);      // 勾子原点
				GetNode<Event>("/root/Event").EmitSignal(Event.SignalName.BackHook);
				GetNode<AudioStreamPlayer>("GrabBack").Stop();
				GetParent<Player1>().Pause();
				GetParent<Player1>().Frame = 0;
				Weight = 0;

				await ToSignal(GetTree().CreateTimer(0.5), SceneTreeTimer.SignalName.Timeout);
				GetNode<AnimationPlayer>("AnimationPlayer").Play();
				_hooking = false;
			}
		}
	}
	private void OnAreaEntered(Area2D @object)
	{
		_goback = false;
		GetParent<Player1>().Play();
		GetNode<AudioStreamPlayer>("GrabBack").Play();
	}

	private void OnBack()
	{
		if (_goback)
		{
			GetParent<Player1>().Play();
			GetNode<AudioStreamPlayer>("GrabBack").Play();
		}
		_goback = false;

	}

	public void Reset()
	{
		GetNode<AnimationPlayer>("AnimationPlayer").ClearCaches();
		Position = new Vector2(-7, 10);
		GetNode<AnimationPlayer>("AnimationPlayer").Play();
	}
}
