using Godot;
using System;

public partial class Level2 : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Timer>("Timer").Timeout += OnTimeout;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnTimeout()
	{
		
	}
}
