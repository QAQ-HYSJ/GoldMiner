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
	private void On_SingleGame_Pressed()
	{
		Global.gameMode = false;
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
	private void On_DoubleGame_Pressed()
	{
		Global.gameMode = true;
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
}
