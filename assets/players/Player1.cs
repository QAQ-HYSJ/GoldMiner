using Godot;
using System;

public partial class Player1 : AnimatedSprite2D
{
	public override void _Ready()
	{
		Global.player1 = this;
	}

	public override void _Draw()
	{
		DrawLine(new Vector2(-7, 10), GetNode<Hook>("Hook").Position, new Color(0.2f, 0.2f, 0.2f), 1.0f);
	}

	public override void _Process(double delta)
	{
		QueueRedraw();
		if (Input.IsActionPressed("ui_accept"))
		{
			GetNode<Hook>("Hook").GoHook();
		}
	}
}
