using Godot;
using System;

public partial class Level : Node2D
{
	public double LeftTime;
	public override void _Ready()
	{
		Global.level = this;
		// 加载玩家
		if (Global.gameMode)
		{
			Player1 player1 = ResourceLoader.Load<PackedScene>("res://assets/players/player1.tscn").Instantiate<Player1>();
			Player2 player2 = ResourceLoader.Load<PackedScene>("res://assets/players/Player2.tscn").Instantiate<Player2>();
			player1.Position = new Vector2(120, 20);
			player2.Position = new Vector2(200, 20);
			AddChild(player1);
			AddChild(player2);
		}
		else
		{
			AddChild(ResourceLoader.Load<PackedScene>("res://assets/players/player1.tscn").Instantiate<Player1>());
		}
		// 加载关卡内容
		AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/contents1.tscn").Instantiate<Node2D>());

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
		GetNode<Event>("/root/Event").EmitSignal(Event.SignalName.Timeout);
		GetTree().ChangeSceneToFile("res://assets/scenes/End.tscn");
	}
}
