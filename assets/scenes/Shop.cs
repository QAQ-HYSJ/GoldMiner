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
    public override void _Process(double delta)
    {
        GetNode<Label>("Money").Text = "当前：$" + Data.Singleton.Money;
    }

    private void On_Button_Pressed()
	{
		GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
	}
}
