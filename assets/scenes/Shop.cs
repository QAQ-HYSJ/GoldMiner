using Godot;
using System;

public partial class Shop : TextureRect
{
	public override void _Ready()
	{
		Global.currentLevel ++;
		Global.InShope = true;
	}

	private void On_Button_Pressed()
	{
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
}
