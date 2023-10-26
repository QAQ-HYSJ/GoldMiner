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
			PackedScene PlayerTree = ResourceLoader.Load<PackedScene>("res://assets/players/Player.tscn");
			Player player1 = PlayerTree.Instantiate<Player>();
			Player player2 = PlayerTree.Instantiate<Player>();
			player1.Position = new Vector2(120, 20);
			player2.Position = new Vector2(200, 20);
			player1.KeyCode = "player1Go";
			player2.KeyCode = "player2Go";
			AddChild(player1);
			AddChild(player2);
		}
		else
		{
			AddChild(ResourceLoader.Load<PackedScene>("res://assets/players/Player.tscn").Instantiate<Player>());
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
