using Godot;
using System;

public partial class Data : Node
{
	public static Data Singleton { get { return data; } set { data = value; } }
	private static Data data;
	public override void _Ready()
	{
		data = this;
	}
	[Export] public int Money { set; get; } = 0;
	[Export] public int goal = 0;
	[Export] public int LevelNum = 1;

	/// <summary>
	/// false单人模式 true双人模式
	/// </summary>
	[Export] public bool gameMode { get; set; }

	// public static bool InShope { get; set; }
	[Export] public bool GemPolishBuff = false;
	[Export] public bool RockBuff = false;
	[Export] public bool LuckyBuff = false;
	public bool HasBuyStrengthBuff = false;				// 临时存储加量加强buff，此时无法获取玩家
	public int TempPlayer1DynamiteNum = 0;				// 临时存储玩家雷管数量，此时无法获取玩家
	public int TempPlayer2DynamiteNum = 0;
}
