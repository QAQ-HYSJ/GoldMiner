using Godot;
using System;

public partial class End : TextureRect
{
	public override void _Ready()
	{
		Save.SaveGame(1, true);
	}

	private	void On_AudioStreamPlayer_Finished()
	{
		GetTree().ChangeSceneToFile("res://assets/scenes/Shop.tscn");
	}
}
