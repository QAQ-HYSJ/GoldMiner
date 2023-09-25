using Godot;
using System;

public partial class Begin : TextureRect
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<AudioStreamPlayer>("AudioStreamPlayer").Finished += OnTimeout;
		Global.goal += 650;
		GetNode<Label>("Pannel/Money").Text = "$" + Global.goal;
	}

	private void OnTimeout()
	{
		GetTree().ChangeSceneToFile("res://assets/levels/Level.tscn");
	}
}
