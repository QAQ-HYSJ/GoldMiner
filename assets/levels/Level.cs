using Godot;
using System;

public partial class Level : Node2D
{
	public double LeftTime;
	private Player player1;
	private Player player2;
	public override void _Ready()
	{
		PackedScene PlayerTree = ResourceLoader.Load<PackedScene>("res://assets/players/Player.tscn");
		// 加载玩家
		if (Data.Singleton.gameMode)
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
			player1.DynamiteNum = Data.Singleton.TempPlayer1DynamiteNum;
			player2.DynamiteNum = Data.Singleton.TempPlayer2DynamiteNum;
		}
		else
		{
			player1 = PlayerTree.Instantiate<Player>();
			AddChild(player1);
			player1.DynamiteNum = Data.Singleton.TempPlayer1DynamiteNum;
		}
		// 加载关卡内容
		LoadLevel();

		// 加载HUD
		AddChild(ResourceLoader.Load<PackedScene>("res://assets/HUD/HUD.tscn").Instantiate<Control>());

		if (Data.Singleton.gameMode)
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
		Data.Singleton.TempPlayer1DynamiteNum = player1.DynamiteNum;
		if (Data.Singleton.gameMode)
			Data.Singleton.TempPlayer2DynamiteNum = player2.DynamiteNum;
		GetTree().ChangeSceneToFile("res://assets/scenes/End.tscn");
	}
	private void LoadLevel()
	{
		switch (Data.Singleton.LevelNum % 10)
		{
			case 0: AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/L0-1.tscn").Instantiate()); break;
			case 1: AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/L1-1.tscn").Instantiate()); break;
			case 2: AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/L2-1.tscn").Instantiate()); break;
			case 3: AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/L3-1.tscn").Instantiate()); break;
			case 4: AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/L4-1.tscn").Instantiate()); break;
			case 5: AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/L5-1.tscn").Instantiate()); break;
			case 6: AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/L6-1.tscn").Instantiate()); break;
			case 7: AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/L7-1.tscn").Instantiate()); break;
			case 8: AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/L8-1.tscn").Instantiate()); break;
			case 9: AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/L9-1.tscn").Instantiate()); break;
		}
	}
}
