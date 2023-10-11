using Godot;
using System;

public partial class Select : ColorRect
{
	public override void _Ready()
	{
		foreach (Button button in GetNode("VBoxContainer").GetChildren())
		{
			button.Connect(Button.SignalName.Pressed, Callable.From(() => On_Button_Pressed(button.GetIndex())));
			button.GetNode("Button").Connect(Button.SignalName.Pressed,
			Callable.From(() => On_SubButton_Pressed(button.GetIndex(), true)));
		}
	}
	private void On_Button_Pressed(int index)
	{
		GD.Print("选" + index);
	}
	private void On_SubButton_Pressed(int index, bool AoD)
	{
		GD.Print("添" + index);
		Save.SaveGame(index, newGame: AoD);
	}
}
