using Godot;
using System;

public partial class HUD : Control
{
	private Label money;
	private Label goal;
	private Label level;
	private Label time;
	public override void _Ready()
	{
		money = GetNode<Label>("Money");
		goal = GetNode<Label>("Goal");
		level = GetNode<Label>("Level");
		time = GetNode<Label>("Time");
	}

	public override void _Process(double delta)
	{
		money.Text = "当前：$" + Global.player2.Money;
		goal.Text = "目标：$" + Global.goal;
		level.Text = "第" + Global.currentLevel + "关";
		time.Text = "剩余时间：" + (int)Global.level.LeftTime;
	}
}
