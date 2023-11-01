using Godot;
using System;

public partial class Diamond : Item
{
    public override void _Ready()
    {
        if(Data.Singleton.GemPolishBuff)
            Value = 900;
    }
}
