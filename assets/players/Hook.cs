using Godot;
using System;

public partial class Hook : Marker2D
{
	public int Weight = 0;
	private bool _goback = true;
	private Vector2 _direction;
	private bool _hooking;
	public void GoHook()
	{
		if (!_hooking)
		{
			_direction = new Vector2((float)-Math.Sin(Rotation), (float)Math.Cos(Rotation)).Normalized();
			_hooking = true;
			_goback = true;
			GetNode<AnimationPlayer>("AnimationPlayer").Pause();
			GetNode<Timer>("Timer").Start();
		}
	}

	public override void _Ready()
	{
		GetNode<Timer>("Timer").Timeout += OnBack;
		GetNode<Area2D>("HitBox").AreaEntered += (@object) => OnAreaEntered(@object);
		Global.hook = this;
	}
	public override void _Process(double delta)
	{
		QueueRedraw();
		if (_hooking)
		{
			if(_goback)  // 出钩
			{
				Position += (float)delta * _direction * 200f;
			}
			else   		 // 回钩
			{
				Position += -(float)delta * _direction * (200f - Weight);
			}

			if (Position.Y <= 10)
			{
				Position = new Vector2(-7, 10);      // 勾子原点
				GetNode<AnimationPlayer>("AnimationPlayer").Play();
				GetNode<Event>("/root/Event").EmitSignal(Event.SignalName.BackHook);
				_hooking = false;
				Weight = 0;
			}
		}
	}
	private void OnAreaEntered(Area2D @object)
	{
		_goback = false;
	}

	private void OnBack()
	{
		_goback = false;
	}

	public void Reset()
	{
		GetNode<AnimationPlayer>("AnimationPlayer").ClearCaches();
		Position = new Vector2(-7, 10);
		GetNode<AnimationPlayer>("AnimationPlayer").Play();
	}
}
