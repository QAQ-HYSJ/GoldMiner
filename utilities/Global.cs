using Godot;
using System;

public partial class Global : Node
{
	public static Level level = null;
	public static int Money { set; get; } = 0;
	public static int goal = 0;
	public static int currentLevel = 1;
	
	/// <summary>
	/// false单人模式 true双人模式
	/// </summary>
	public static bool gameMode { get; set; }
	public static bool InShope { get; set; }
	public static bool GemPolishBuff = false;
	public static bool RockBuff = false;
}
