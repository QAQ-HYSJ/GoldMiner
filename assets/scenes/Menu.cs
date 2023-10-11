using Godot;
using System;

public partial class Menu : TextureRect
{
	private void On_Begin_Pressed()
	{
		GetNode<ColorRect>("Select").Visible = true;
	}
}
