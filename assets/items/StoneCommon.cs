using Godot;
using System;

public partial class StoneCommon : Item
{
    public override void _Ready()
    {
        if (Data.Singleton.RockBuff)
            Value = 40;
    }
}
