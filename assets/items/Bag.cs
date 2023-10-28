using Godot;
using System;

public partial class Bag : Item
{
    public Type type;
    public override void _Ready()
    {
        int rand = GD.RandRange(1, 5);
        if (rand == 1)
        {
            type = Type.Dynamite;
            Weight = 30;
        }
        else if (rand == 2)
        {
            type = Type.Strength;
            Weight = 30;
        }
        else
        {
            type = Type.Money;
            Weight = GD.RandRange(10, 80);
            Value = GD.RandRange(1, 900);
            if (Global.LuckyBuff)
            {
                Weight = GD.RandRange(10, 70);
                Value = GD.RandRange(200, 999);
            }
        }
    }
}
