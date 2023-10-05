using Godot;
using System;

public partial class Shop : TextureRect
{
	public override void _Ready()
	{
		Global.currentLevel ++;
	}

	private void _on_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
}
