using Godot;
using System;

public partial class Bag : Item
{
    public override void _Ready()
    {
        Weight = GD.RandRange(10, 80);
        Value = GD.RandRange(1, 900);
        if(Global.LuckyBuff)
        {
            Weight = GD.RandRange(10, 70);
            Value = GD.RandRange(200, 999);
        }
    }
}
