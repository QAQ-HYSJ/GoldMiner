using Godot;
using System;

public partial class Shop : TextureRect
{
	public override void _Ready()
	{
		Data.Singleton.LevelNum ++;
		// Global.InShope = true;

		Data.Singleton.GemPolishBuff = false;
		Data.Singleton.RockBuff = false;
		Data.Singleton.LuckyBuff = false;
		Data.Singleton.HasBuyStrengthBuff = false;
	}

	private void On_Button_Pressed()
	{
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
}
