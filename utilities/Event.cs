using Godot;
using System;

public partial class Event : Node
{
	[Signal]
	public delegate void BackHookEventHandler();
	[Signal]
	public delegate void TimeoutEventHandler();
}
