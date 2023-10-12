using Godot;
using System;

public partial class Select : ColorRect
{
	private bool[] buttonMode = new bool[6]; // true为有， false为空
	public override void _Ready()
	{
		Update();
		foreach (Button button in GetNode("VBoxContainer").GetChildren())
		{
			button.Connect(Button.SignalName.Pressed, Callable.From(() => On_Button_Pressed(button.GetIndex())));
			button.GetNode("Button").Connect(Button.SignalName.Pressed,
			Callable.From(() => On_SubButton_Pressed(button.GetIndex())));
		}
	}
	public void Update()
	{
		for (int i = 0; i < 5; i++)
		{
			if (Save.LoadGame(i) == null || Save.LoadGame(i).Count == 0)
			{
				GetNode("VBoxContainer").GetChild<Button>(i).Text = "空";
				GetNode("VBoxContainer").GetChild(i).GetNode<Button>("Button").Text = "+";
				buttonMode[i] = false;
			}
			else
			{
				// 加载内容
				GetNode("VBoxContainer").GetChild<Button>(i).Text = "非空";
				GetNode("VBoxContainer").GetChild(i).GetNode<Button>("Button").Text = "x";
				buttonMode[i] = true;
			}
		}
	}
	private void On_Button_Pressed(int index)
	{
		GD.Print("选" + index);
	}
	private void On_SubButton_Pressed(int index)
	{
		if (buttonMode[index])
		{
			GD.Print("删档" + index);
		}
		else
		{
			GetNode<Dialog>("Dialog").Open(index);
		}
	}
}
