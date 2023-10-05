using Godot;
using System;

public partial class Global : Node
{
	public static Player1 player1 = null;
	public static Player2 player2 = null;
	public static SpacePlayer1.Hook player1Hook = null;
	public static SpacePlayer2.Hook player2Hook = null;
	public static Level level = null;
	public static int Money { set; get; } = 0;
	public static int goal = 0;
	public static int currentLevel = 1;
	/// <summary>
	/// false单人模式 true双人模式
	/// </summary>
	public static bool gameMode { get; set; }
}
