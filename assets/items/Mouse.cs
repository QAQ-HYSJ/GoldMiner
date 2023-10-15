using Godot;
using System;

public partial class Mouse : Item
{
	private enum Direction { left, right, idle }
	private Direction direction;
	private int Counter = 0;
	public override void _Ready()
	{
		direction = Direction.idle;
		GetNode<Timer>("Timer").WaitTime = 1;
		GetNode<Timer>("Timer").Start();
	}
	public override void _Process(double delta)
	{
		switch (direction)
		{
			case Direction.idle: break;
			case Direction.left: Position = new Vector2(Position.X - (float)delta * 20f, Position.Y); break;
			case Direction.right: Position = new Vector2(Position.X + (float)delta * 20f, Position.Y); break;
		}
	}
	private void On_Timer_Timeout()
	{
		Counter++;
		if (Counter % 4 == 0 || Counter % 4 == 2)
		{
			direction = Direction.idle;
			GetNode<Timer>("Timer").WaitTime = 2;
			GetNode<Timer>("Timer").Start();
		}
		if (Counter % 4 == 1)
		{
			direction = Direction.left;
			GetNode<Timer>("Timer").WaitTime = 4;
			GetNode<Timer>("Timer").Start();
		}
		if (Counter % 4 == 3)
		{
			direction = Direction.right;
			GetNode<Timer>("Timer").WaitTime = 4;
			GetNode<Timer>("Timer").Start();
		}
	}
}

