using Godot;
using Godot.Collections;
using System;

public partial class Begin : TextureRect
{
	public override void _Ready()
	{
		Global.InShope = false;
		Global.goal += 650;
		GetNode<Label>("Pannel/Money").Text = "$" + Global.goal;

		// Dictionary<string, Variant> data = Save.LoadGame(1);
		// if(data.Count == 0)
		// {
		// 	GD.Print("空档");
		// }
		// else
		// {
        //     data.TryGetValue("Money", out Variant money);
        //     GD.Print(Global.Money = (int)money);
		// }
	}

	private void On_AudioStreamPlayer_Finished()
	{
		GetTree().ChangeSceneToFile("res://assets/levels/Level.tscn");
	}
}
