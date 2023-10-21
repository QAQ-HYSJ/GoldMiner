using Godot;
using System;

public partial class TNT : Item
{
	[Export] public Texture2D texture;
	public void Explosion()
	{
		GetNode<Sprite2D>("Sprite2D").Visible = false;
		GetNode<AnimatedSprite2D>("Explosion").Visible = true;
		GetNode<AnimatedSprite2D>("Explosion").Play();
		GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
		GetNode<Area2D>("ExplosionArea").Monitoring = true;
	}
	private void On_AudioStreamPlayer_Finished()
	{
		QueueFree();
	}
	private void On_Explosion_AnimationFinished()
	{
		GetNode<AnimatedSprite2D>("Explosion").Visible = false;
	}
	private async void On_ExplosionArea_AreaEntered(Area2D area)
	{
		if(area is TNT tnt)
		{
			await ToSignal(GetTree().CreateTimer(0.1), SceneTreeTimer.SignalName.Timeout);
			tnt.Explosion();
		}
		else if (area is Item item)
		{
			item.QueueFree();
		}
	}
}
