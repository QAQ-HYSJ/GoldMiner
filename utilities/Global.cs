using Godot;
using System;

public partial class Global : Node
{
	public static Player1 player1 = null;
	public static Player2 player2 = null;
	public static spacePlayer1.Hook player1Hook = null;
	public static SpacePlayer2.Hook player2Hook = null;
	public static Level level = null;
	public static int goal = 0;
	public static int currentLevel = 1;
}
