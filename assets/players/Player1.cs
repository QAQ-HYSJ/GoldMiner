using Godot;
using System;

public partial class Player1 : AnimatedSprite2D
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("ui_accept"))
		{
			GetNode<Hook>("Hook").GoHook();
		}
	}
}
