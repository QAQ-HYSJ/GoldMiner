using Godot;
using System;

public partial class Dialog : ColorRect
{
	private int buttonIndex;
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && !GetRect().HasPoint(mouseEvent.Position))
		{
			Visible = false;
		}
	}
	public void Open(int index)
	{
		Visible = true;
		buttonIndex = index;
	}
	private void On_Single_Pressed()
	{
		Save.SaveGame(buttonIndex, true);
		GetParent<Select>().Update();
	}
	private void On_Double_Pressed()
	{
		Save.SaveGame(buttonIndex, true);
		GetParent<Select>().Update();
	}
}
