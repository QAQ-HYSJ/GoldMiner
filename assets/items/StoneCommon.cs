using Godot;
using System;

public partial class StoneCommon : Item
{
    public override void _Ready()
    {
        if (Global.RockBuff)
            Value = 40;
    }
}
