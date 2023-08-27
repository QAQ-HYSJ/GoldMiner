using Godot;
using System;

public partial class Begin : TextureRect
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Timer>("Timer").Timeout += OnTimeout;
		Global.goal += 650;
		GetNode<Label>("Pannel/Money").Text = "$" + Global.goal;
	}

	private void OnTimeout()
	{
		switch (Global.nextLevel)
		{
			case 1:
				GetTree().ChangeSceneToFile("res://assets/levels/Level1.tscn");
				break;
			case 2:
				GetTree().ChangeSceneToFile("res://assets/levels/Level2.tscn");
				break;

		}
	}
}
