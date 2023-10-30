using Godot;
using System;

public partial class Level : Node2D
{
	public double LeftTime;
	private Player player1;
	private Player player2;
	public override void _Ready()
	{
		Global.level = this;
		PackedScene PlayerTree = ResourceLoader.Load<PackedScene>("res://assets/players/Player.tscn");
		// 加载玩家
		if (Global.gameMode)
		{
			player1 = PlayerTree.Instantiate<Player>();
			player2 = PlayerTree.Instantiate<Player>();
			player1.Position = new Vector2(120, 20);
			player2.Position = new Vector2(200, 20);
			player1.HookKeyCode = "player1Go";
			player1.DynamiteKeyCode = "player1Dynamite";
			player2.HookKeyCode = "player2Go";
			player2.DynamiteKeyCode = "player2Dynamite";
			AddChild(player1);
			AddChild(player2);
			player1.DynamiteNum = Global.player1DynamiteNum;
			player2.DynamiteNum = Global.player2DynamiteNum;
		}
		else
		{
			player1 = PlayerTree.Instantiate<Player>();
			AddChild(player1);
			player1.DynamiteNum = Global.player1DynamiteNum;
		}
		// 加载关卡内容
		LoadLevel();

		// 加载HUD
		AddChild(ResourceLoader.Load<PackedScene>("res://assets/HUD/HUD.tscn").Instantiate<Control>());

		if (Global.gameMode)
		{
			GetNode<Timer>("Timer").WaitTime = 30;
		}
		else
		{
			GetNode<Timer>("Timer").WaitTime = 60;
		}
		GetNode<Timer>("Timer").Start();
	}
	public override void _Process(double delta)
	{
		LeftTime = GetNode<Timer>("Timer").TimeLeft;
	}
	private void On_Timer_Timeout()
	{
		Global.player1DynamiteNum = player1.DynamiteNum;
		if (Global.gameMode)
			Global.player2DynamiteNum = player2.DynamiteNum;
		GetTree().ChangeSceneToFile("res://assets/scenes/End.tscn");
	}
	private void LoadLevel()
	{
		if (Global.currentLevelNum == 1)
		{
			AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/L1-1.tscn").Instantiate<Node2D>());
		}
		else if (Global.currentLevelNum == 2)
		{
			AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/L2-1.tscn").Instantiate<Node2D>());
		}
		else if (Global.currentLevelNum == 3)
		{
			AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/L3-1.tscn").Instantiate<Node2D>());
		}
		else if( Global.currentLevelNum == 4)
		{
			AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/L4-1.tscn").Instantiate<Node2D>());
		}
	}
}
