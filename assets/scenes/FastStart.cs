using Godot;
using System;

public partial class FastStart : TextureRect
{
	private void On_Single_Pressed()
	{
		Data.Singleton.gameMode = false;
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
	private void On_Double_Pressed()
	{
		Data.Singleton.gameMode = true;
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
}
