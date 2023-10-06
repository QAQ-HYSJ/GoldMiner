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
		Global.player1 = ResourceLoader.Load<PackedScene>("res://assets/players/player1.tscn").Instantiate<Player1>();
		Global.gameMode = false;
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
	private void _on_double_game_pressed()
	{
		Global.gameMode = true;
		Global.player1 = ResourceLoader.Load<PackedScene>("res://assets/players/player1.tscn").Instantiate<Player1>();
		Global.player2 = ResourceLoader.Load<PackedScene>("res://assets/players/Player2.tscn").Instantiate<Player2>();
		Global.player1.Position = new Vector2(120, 20);
		Global.player2.Position = new Vector2(200, 20);
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
}
