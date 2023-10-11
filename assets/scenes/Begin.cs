using Godot;
using Godot.Collections;
using System;

public partial class Begin : TextureRect
{
	public override void _Ready()
	{
		Global.InShope = false;
		Global.goal += 650;
		GetNode<Label>("Pannel/Money").Text = "$" + Global.goal;

		Dictionary<string, Variant> data = Save.LoadGame(1);
		// Global.Money = (int)data["Money"];
	}

	private void On_AudioStreamPlayer_Finished()
	{
		GetTree().ChangeSceneToFile("res://assets/levels/Level.tscn");
	}
}
