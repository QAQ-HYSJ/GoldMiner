using Godot;
using System;

[GlobalClass]
public partial class ItemProperties : Resource
{
    [Export] public int Weight;
    [Export] public int Value;
}
