using Godot;
using System;

public partial class Menu : TextureRect
{
	public override void _Ready()
	{

	}
	public override void _Process(double delta)
	{
	}
	private void _on_single_game_pressed()
	{
		Global.gameMode = false;
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
	private void _on_double_game_pressed()
	{
		Global.gameMode = true;
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
}
