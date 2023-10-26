using Godot;
using System;

public partial class Player : AnimatedSprite2D
{
	[Export] public string KeyCode = "player1Go";
	public bool StrengthBuff = false;
    public override void _Ready()
    {
        if(Global.HasBuyStrengthBuff)
			StrengthBuff = true;
    }
    public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed(KeyCode))
		{
			GetNode<Hook>("Hook").GoHook();
		}
		QueueRedraw();
	}
	public override void _Draw()
	{
		DrawLine(GetNode<Hook>("Hook").OriginPoint, GetNode<Hook>("Hook").Position, new Color(0.2f, 0.2f, 0.2f), 1.0f);
	}
}
