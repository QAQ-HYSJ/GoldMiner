using Godot;
using System;

[GlobalClass]
public partial class Item : Area2D
{
	public bool holding = false;
	[Export] public ItemProperties Properties;
	public override void _Ready()
	{
		this.AreaEntered += (area) => OnHookHit(area);
		GetNode<Event>("/root/Event").BackHook += OnBackHook;
	}

	public override void _Process(double delta)
	{
		if (holding)
			GlobalPosition = Global.hook.GetNode<CollisionShape2D>("HitBox/CollisionShape2D").GlobalPosition;
	}

	private void OnHookHit(Area2D area)
	{
		holding = true;
		Global.hook.Weight = Properties.Weight;
	}

	private void OnBackHook()
	{
		if(holding)
		{
			GetNode<Event>("/root/Event").BackHook -= OnBackHook;
			Global.player1.GetNode<Score>("CanvasLayer/Score").CurrentScore += Properties.Value;
			QueueFree();	
		}	
	}
}
