using Godot;
using System;

public partial class Player2 : AnimatedSprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("player2Go"))
		{
			GetNode<SpacePlayer2.Hook>("Hook").GoHook();
		}
	}
}
