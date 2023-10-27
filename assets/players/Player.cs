using Godot;
using System;

public partial class Player : AnimatedSprite2D
{
	[Export] public string KeyCode = "player1Go";
	public bool StrengthBuff = false;
	private int _dynamiteNum = 0;
	public int DynamiteNum
	{
		get { return _dynamiteNum; }
		set { _dynamiteNum = Mathf.Clamp(value, 0, 5); Updata(); }
	}
	public override void _Ready()
	{
		if (Global.HasBuyStrengthBuff)
			StrengthBuff = true;
		Updata();
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
	public void Updata()
	{
		for (int i = 0; i < 5; i++)
		{
			if (i < DynamiteNum)
				GetNode("Dynamite").GetChild<Sprite2D>(i).Visible = true;
			else
				GetNode("Dynamite").GetChild<Sprite2D>(i).Visible = false;
		}

	}
}
