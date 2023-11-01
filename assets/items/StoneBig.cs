using Godot;
using System;

public partial class StoneBig : Item
{
    public override void _Ready()
    {
        if(Data.Singleton.RockBuff)
            Value = 200;
    }
}
