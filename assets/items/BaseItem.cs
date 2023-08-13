using Godot;
using System;

[GlobalClass]
public partial class BaseItem : Area2D
{
	public bool holding = false;
	public override void _Ready()
	{
		this.AreaEntered += (area) => OnHookHit(area);
	}

	public override void _Process(double delta)
	{
		if (holding)
			GlobalPosition = Global.hook.GlobalPosition;
	}

	private void OnHookHit(Area2D area)
	{
		holding = true;
	}
}
