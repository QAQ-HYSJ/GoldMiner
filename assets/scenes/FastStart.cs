using Godot;
using System;

public partial class FastStart : Control
{
	private void On_Single_Pressed()
	{
		Global.gameMode = false;
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
	private void On_Double_Pressed()
	{
		Global.gameMode = true;
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
}
