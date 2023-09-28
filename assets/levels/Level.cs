using Godot;
using System;

public partial class Level : Node2D
{
	public double LeftTime;
	public override void _Ready()
	{
		Global.level = this;
		GetNode<Timer>("Timer").Timeout += OnTimeout;
		AddChild(Global.player1);
		Global.player1.GetNode<Hook>("Hook").Reset();
		AddChild(ResourceLoader.Load<PackedScene>("res://assets/levels/contents1.tscn").Instantiate<Node2D>());
		AddChild(ResourceLoader.Load<PackedScene>("res://assets/HUD/HUD.tscn").Instantiate<Control>());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		LeftTime = GetNode<Timer>("Timer").TimeLeft;
	}
	private void OnTimeout()
	{
		GetNode<Event>("/root/Event").EmitSignal(Event.SignalName.Timeout);
		RemoveChild(Global.player1);
		GetTree().ChangeSceneToFile("res://assets/scenes/End.tscn");
	}
}
