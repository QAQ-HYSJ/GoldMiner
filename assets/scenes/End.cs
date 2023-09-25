using Godot;
using System;

public partial class End : TextureRect
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<AudioStreamPlayer>("AudioStreamPlayer").Finished += OnBgmFinished;
	}

	private	void OnBgmFinished()
	{
		
	}
}
