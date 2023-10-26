using Godot;
using System;

public partial class Diamond : Item
{
    public override void _Ready()
    {
        if(Global.GemPolishBuff)
            Value = 900;
    }
}
