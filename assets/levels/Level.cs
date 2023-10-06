using Godot;
using System;

public partial class Level : Node2D
{
	public double LeftTime;
	public override void _Ready()
	{
		Global.level = this;
		if (Global.gameMode)
		{
			AddChild(Global.player2);
			AddChild(Global.player1);
		}
		else
		{
			AddChild(Global.player1);
		}
		// 加载关卡内容
		AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/contents1.tscn").Instantiate<Node2D>());

		// 加载HUD
		AddChild(ResourceLoader.Load<PackedScene>("res://assets/HUD/HUD.tscn").Instantiate<Control>());

		// 重置玩家状态
		if (Global.gameMode)
		{
			Global.player2.GetNode<Hook>("Hook").Reset();
			Global.player1.GetNode<Hook>("Hook").Reset();
		}
		else
		{
			Global.player1.GetNode<Hook>("Hook").Reset();
		}
		if(Global.gameMode)
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
	private void _on_timer_timeout()
	{
		GetNode<Event>("/root/Event").EmitSignal(Event.SignalName.Timeout);

		if (Global.gameMode)
		{
			RemoveChild(Global.player2);
			RemoveChild(Global.player1);
		}
		else
		{
			RemoveChild(Global.player1);
		}
		GetTree().ChangeSceneToFile("res://assets/scenes/End.tscn");
	}
}
