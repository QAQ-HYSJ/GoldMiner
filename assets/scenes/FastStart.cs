using Godot;
using System;

public partial class FastStart : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void On_Single_Pressed()
	{
		Global.gameMode = false;
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
	private void On_Double_Pressed()
	{
		Global.gameMode = true;
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
}
