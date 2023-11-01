using Godot;
using System;

public partial class StoneSmall : Item
{
    public override void _Ready()
    {
        if(Data.Singleton.RockBuff)
            Value = 22;
    }
}
