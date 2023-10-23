using Godot;
using System;

public partial class Bag : Item
{
    public override void _Ready()
    {
        Properties.Weight = GD.RandRange(10, 80);
        Properties.Value = GD.RandRange(50, 900);
    }
}
