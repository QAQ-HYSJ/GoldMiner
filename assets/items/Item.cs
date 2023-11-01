using Godot;
using System;

public enum Type { Dynamite, Strength, Money };

[GlobalClass]
public partial class Item : Area2D
{
	public enum ValueLevel { low, mid, high }
	public enum SizeLevel { small, big }
	[Export(PropertyHint.Range, "0, 100,")] public int Weight { get; set; }
	[Export] public int Value;

	/// <summary>
	/// 影响钩中时的音效
	/// </summary>
	[Export] public ValueLevel valueLevel;
	[Export] public Vector2 Offect;

	/// <summary>
	/// 影响钩中时的钩子形状
	/// </summary>
	[Export] public SizeLevel sizeLevel;
}
