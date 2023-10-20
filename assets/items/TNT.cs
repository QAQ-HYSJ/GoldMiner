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
	}
	private void On_AudioStreamPlayer_Finished()
	{
		QueueFree();
	}
	private void On_Explosion_AnimationFinished()
	{
		GetNode<AnimatedSprite2D>("Explosion").Visible = false;
	}
}
