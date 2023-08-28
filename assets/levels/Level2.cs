using Godot;
using System;

public partial class Level2 : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// #if DEBUG
		// Global.player1 = ResourceLoader.Load<PackedScene>("res://assets/players/player1.tscn").Instantiate<Player1>();
		// #endif
		GetNode<Timer>("Timer").Timeout += OnTimeout;
		AddChild(Global.player1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnTimeout()
	{
		
	}
}
