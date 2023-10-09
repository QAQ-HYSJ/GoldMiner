using Godot;
using System;

public partial class End : TextureRect
{
	public override void _Ready()
	{
		Save.SaveGame();
	}

	private	void _on_audio_stream_player_finished()
	{
		GetTree().ChangeSceneToFile("res://assets/scenes/Shop.tscn");
	}
}
