using Godot;
using System;

[GlobalClass]
public partial class ItemProperties : Resource
{
    public enum ValueLevel { low, mid, high }
    [Export] public int Weight;
    [Export] public int Value;
    [Export] public ValueLevel valueLevel;
}
