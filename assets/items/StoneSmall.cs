using Godot;
using System;

public partial class StoneSmall : Item
{
    public override void _Ready()
    {
        if(Global.RockBuff)
            Value = 22;
    }
}
