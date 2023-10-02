using Godot;
using System;

public partial class Menu : TextureRect
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Button>("SingleGame").Pressed += () => OnPressed("single");
		GetNode<Button>("DoubleGame").Pressed += () => OnPressed("double");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void OnPressed(string button)
	{
		switch (button)
		{
			case "single":
				Global.player1 = ResourceLoader.Load<PackedScene>("res://assets/players/player1/player1.tscn").Instantiate<Player1>();
				Global.player2 = ResourceLoader.Load<PackedScene>("res://assets/players/player2/Player2.tscn").Instantiate<Player2>();
				GetTree().ChangeSceneToFile("res://assets/scenes/Begin.tscn");
				break;
			case "double":
				GD.Print("double");
				break;
		}
	}
}
