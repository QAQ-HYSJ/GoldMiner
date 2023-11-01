using Godot;
using System;

public partial class End : TextureRect
{
	public override void _Ready()
	{
		if (Data.Singleton.Money < Data.Singleton.goal)
		{
			GetNode<Label>("Panel/Label").Text = "您未达成目标\n得分：" + Data.Singleton.Money;
			GetNode<Label>("Panel/Label").Set("theme_override_font_sizes/font_size", 20);
		}
		else
		{
			GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
		}
	}
	private void On_AudioStreamPlayer_Finished()
	{
		if (Data.Singleton.Money < Data.Singleton.goal)
			GetTree().ChangeSceneToFile("res://assets/scenes/FastStart.tscn");
		else
			GetTree().ChangeSceneToFile("res://assets/scenes/Shop.tscn");
	}
}
