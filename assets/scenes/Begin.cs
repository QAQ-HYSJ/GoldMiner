using Godot;
using Godot.Collections;
using System;

public partial class Begin : TextureRect
{
	public override void _Ready()
	{
		Global.InShope = false;
		if (Global.currentLevelNum < 10)
			Global.goal = 375 + (135 * (1 + Global.currentLevelNum) * Global.currentLevelNum) + 5 * Global.currentLevelNum;
		else
			Global.goal = 12575 + 2705 * (Global.currentLevelNum - 9);
		GetNode<Label>("Pannel/Money").Text = "$" + Global.goal;
	}

	private void On_AudioStreamPlayer_Finished()
	{
		GetTree().ChangeSceneToFile("res://assets/levels/Level.tscn");
	}
}
