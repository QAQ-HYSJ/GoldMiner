using Godot;
using System;

public partial class Player1 : AnimatedSprite2D
{
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("player1Go"))
		{
			GetNode<Hook>("Hook").GoHook();
		}
		QueueRedraw();
	}
	public override void _Draw()
	{
		DrawLine(GetNode<Hook>("Hook").OriginPoint, GetNode<Hook>("Hook").Position, new Color(0.2f, 0.2f, 0.2f), 1.0f);
	}
}
