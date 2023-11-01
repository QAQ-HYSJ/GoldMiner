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
		money.Text = "当前：$" + Data.Singleton.Money;
		goal.Text = "目标：$" + Data.Singleton.goal;
		level.Text = "第" + Data.Singleton.LevelNum + "关";
		time.Text = "剩余时间：" + (int)(GetTree().CurrentScene as Level).LeftTime;
	}
	private void On_Pause_Pressed()
	{
		GetTree().Paused = !GetTree().Paused;
	}
}
