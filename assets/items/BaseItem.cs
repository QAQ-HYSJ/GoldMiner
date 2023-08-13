using Godot;
using System;

[GlobalClass]
public partial class BaseItem : Area2D
{
	public bool holding = false;
	[Export] public ItemProperties Properties;
	public override void _Ready()
	{
		this.AreaEntered += (area) => OnHookHit(area);
	}

	public override void _Process(double delta)
	{
		if (holding)
			GlobalPosition = Global.hook.GetNode<CollisionShape2D>("HitBox/CollisionShape2D").GlobalPosition;
		if (Position.Y <= 60)
			QueueFree();
	}

	private void OnHookHit(Area2D area)
	{
		holding = true;
		Global.hook.Weight = Properties.Weight;
	}
}
