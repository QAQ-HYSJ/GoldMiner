using Godot;
using System;

public partial class Begin : TextureRect
{
	public override void _Ready()
	{
		Global.goal += 650;
		GetNode<Label>("Pannel/Money").Text = "$" + Global.goal;
	}

	private void _on_audio_stream_player_finished()
	{
		GetTree().ChangeSceneToFile("res://assets/levels/Level.tscn");
	}
}
