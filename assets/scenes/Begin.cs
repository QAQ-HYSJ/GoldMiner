using Godot;
using Godot.Collections;
using System;

public partial class Begin : TextureRect
{
	public override void _Ready()
	{
		// Global.InShope = false;
		if (Data.Singleton.LevelNum < 10)
			Data.Singleton.goal = 375 + (135 * (1 + Data.Singleton.LevelNum) * Data.Singleton.LevelNum) + 5 * Data.Singleton.LevelNum;
		else
			Data.Singleton.goal = 12575 + 2705 * (Data.Singleton.LevelNum - 9);
		GetNode<Label>("Pannel/Money").Text = "$" + Data.Singleton.goal;
	}

	private void On_AudioStreamPlayer_Finished()
	{
		GetTree().ChangeSceneToFile("res://assets/levels/Level.tscn");
	}
}
