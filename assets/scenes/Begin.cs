using Godot;
using System;

public partial class Begin : TextureRect
{
	public override void _Ready()
	{
		Global.InShope = false;
		Global.goal += 650;
		GetNode<Label>("Pannel/Money").Text = "$" + Global.goal;
		//Save.LoadGame();
		Save.SaveGame(1, newGame: true);
		Save.SaveGame(2, newGame: true);
		Save.SaveGame(3, newGame: true);
		Save.SaveGame(4, newGame: true);
		Save.SaveGame(5, newGame: true);
		Save.SaveGame(6, newGame: true);
	}

	private void _on_audio_stream_player_finished()
	{
		GetTree().ChangeSceneToFile("res://assets/levels/Level.tscn");
	}
}
