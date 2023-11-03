using Godot;
using System;

public partial class End : TextureRect
{
	public override async void _Ready()
	{
		if (Data.Singleton.Money < Data.Singleton.goal)
		{
			GetNode<Label>("Panel/Label").Text = "您未达成目标\n得分：" + Data.Singleton.Money;
			GetNode<Label>("Panel/Label").Set("theme_override_font_sizes/font_size", 20);
			await ToSignal(GetTree().CreateTimer(1), SceneTreeTimer.SignalName.Timeout);
			GetNode<Button>("Panel/Button").Show();
		}
		else
		{
			GetNode<AudioStreamPlayer>("AudioStreamPlayer").Play();
		}
	}
	private void On_AudioStreamPlayer_Finished()
	{
		GetTree().ChangeSceneToFile("res://assets/scenes/Shop.tscn");
	}
	private void On_Button_Pressed()
	{
		GetTree().ChangeSceneToFile("res://assets/scenes/FastStart.tscn");
	}
}
