using Godot;
using System;

public partial class Shop : TextureRect
{
	public override void _Ready()
	{
		Global.currentLevelNum ++;
		// Global.InShope = true;

		Global.GemPolishBuff = false;
		Global.RockBuff = false;
		Global.LuckyBuff = false;
		Global.HasBuyStrengthBuff = false;
	}

	private void On_Button_Pressed()
	{
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
}
