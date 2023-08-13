using Godot;
using System;

public partial class Hook : Marker2D
{
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
		Global.hook = this;
	}
	public override void _Process(double delta)
	{
		QueueRedraw();
		if (_hooking)
		{
			Position += (float)delta * _direction * 200f * (_goback ? 1 : -1);
			if (hit())
			{
				_goback = false;
			}
			if (Position.Y <= 10)
			{
				Position = new Vector2(-7, 10);      // 勾子原点
				GetNode<AnimationPlayer>("AnimationPlayer").Play();
				_hooking = false;
			}
		}
	}
	private bool hit()
	{
		if (GetNode<Area2D>("HitBox").HasOverlappingAreas())
		{
			return true;
		}
		return false;
	}

	private void OnBack()
	{
		_goback = false;
	}


}
