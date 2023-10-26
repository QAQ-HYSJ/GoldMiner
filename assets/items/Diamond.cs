using Godot;
using System;

public partial class Diamond : Item
{
    public override void _Ready()
    {
        GD.Print(Global.GemPolishBuff);
        GD.Print(Value);
        if(Global.GemPolishBuff)
            Value = 900;
        GD.Print(Value);
    }
}
