using Godot;
using System;

public partial class Shop : TextureRect
{
	public override void _Ready()
	{
		Global.currentLevel ++;
		Global.InShope = true;
	}

	private void _on_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
}
