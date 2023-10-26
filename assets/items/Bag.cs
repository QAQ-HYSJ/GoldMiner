using Godot;
using System;

public partial class Bag : Item
{
    public override void _Ready()
    {
        Weight = GD.RandRange(10, 80);
        Value = GD.RandRange(50, 900);
    }
}
