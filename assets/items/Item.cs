using Godot;
using System;

[GlobalClass]
public partial class Item : Area2D
{
	public bool holding = false;
	[Export] public ItemProperties Properties;
	public override void _Ready()
	{
		// this.AreaEntered += (area) => OnHookHit(area);
		// GetNode<Event>("/root/Event").BackHook += OnBackHook;
		//Callable a = Callable.From((Area2D area) => OnTestConnect(true, 2));

		// Callable a = Callable.From((Area2D area) => OnTestConnect(area, 1, 2));
		// Connect(SignalName.AreaEntered, a);

		// this.Connect(SignalName.AreaEntered, new Callable(this, "OnHookHit"));
		this.Connect(SignalName.AreaEntered, Callable.From((Area2D area) => OnHookHit(area)));
		GetNode<Event>("/root/Event").Connect(Event.SignalName.BackHook, Callable.From(OnBackHook));
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

	private async void OnBackHook()
	{
		if (holding)
		{
			// GetNode<Event>("/root/Event").BackHook -= OnBackHook;
			Global.player1.GetNode<Money>("CanvasLayer/Money").CurrentMoney += Properties.Value;
			await ToSignal(GetTree().CreateTimer(0.5), SceneTreeTimer.SignalName.Timeout);
			QueueFree();
		}
	}
	public override void _ExitTree()
	{
		// GetNode<Event>("/root/Event").BackHook -= OnBackHook;
		// this.AreaEntered -= (area) => OnHookHit(area);
	}
	// private void OnTestConnect(Area2D area, int arg1, int arg2)
	// {

	// }
}
