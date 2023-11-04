using Godot;
using System;

public partial class FastStart : TextureRect
{
	public override void _Ready()
	{
		Data.Singleton.LevelNum = 1;
		Data.Singleton.Money = 0;
		Data.Singleton.HasBuyStrengthBuff = false;
		Data.Singleton.RockBuff = false;
		Data.Singleton.LuckyBuff = false;
		Data.Singleton.GemPolishBuff = false;
		Data.Singleton.TempPlayer1DynamiteNum = 0;
		Data.Singleton.TempPlayer2DynamiteNum = 0;
	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed)
		{
			GetNode<Control>("DescribeBoard").Hide();
			GetNode<Control>("AboutBoard").Hide();
		}
	}
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
	private void On_Describe_Pressed()
	{
		GetNode<Control>("DescribeBoard").Show();
	}
	private void On_About_Pressed()
	{
		GetNode<Control>("AboutBoard").Show();
	}
}
