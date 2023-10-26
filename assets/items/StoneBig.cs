using Godot;
using System;

public partial class StoneBig : Item
{
    public override void _Ready()
    {
        if(Global.RockBuff)
            Value = 200;
    }
}
