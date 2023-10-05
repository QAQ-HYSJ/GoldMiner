using Godot;
using System;

public partial class Level : Node2D
{
	public double LeftTime;
	public override void _Ready()
	{
		Global.level = this;

		AddChild(Global.player1);

		// 加载关卡内容
		AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/contents1.tscn").Instantiate<Node2D>());

		// 加载HUD
		AddChild(ResourceLoader.Load<PackedScene>("res://assets/HUD/HUD.tscn").Instantiate<Control>());

		// 重置玩家状态
		if (Global.gameMode)
		{
			Global.player2Hook.Reset();
			Global.player1Hook.Reset();
		}
		else
		{
			Global.player1Hook.Reset();
		}
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
