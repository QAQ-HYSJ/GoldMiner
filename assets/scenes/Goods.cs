using Godot;
using System;

public partial class Goods : Control
{
    private int[] prices = new int[5];
    private string[] describes = new string[5]
    {
        "雷管：获得一个雷管",
        "力量药剂：下一关加快钩子速度",
        "幸运草：下一关更高概率袋子中出现高价值物品",
        "宝石抛光剂：下一关钻石能卖出更高价值",
        "石头收藏书：下一关石头能卖出更高价值"
    };
    public override void _Ready()  // 随机出现物品
    {
        bool temp = true;  //保底得有一个
        for (int i = 0; i < 5; i++)
        {
            if (GD.RandRange(1, 2) == 1)
            {
                GetChild<TextureRect>(i).Visible = false;
            }
            else
            {
                prices[i] = GD.RandRange(2, 999);
                GetChild<TextureRect>(i).GetNode<Label>("Label").Text = "$" + prices[i];
                temp = false;  // 一旦有了物品，则为false，后面无需再管
            }
        }
        if (temp)
        {
            int num = GD.RandRange(0, 5);
            GetChild<TextureRect>(num).Visible = true;
            prices[num] = GD.RandRange(1, 999);
            GetChild<TextureRect>(num).GetNode<Label>("Label").Text = "$" + prices[num];
        }
        GetParent().GetNode<Label>("Label").Text = "";
    }
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton inputEventMouseButton && inputEventMouseButton.IsReleased() && inputEventMouseButton.ButtonIndex == MouseButton.Left)
        {
            for (int i = 0; i < 5; i++)
            {
                if (GetChild<TextureRect>(i).GetRect().HasPoint(inputEventMouseButton.Position) && GetChild<TextureRect>(i).Visible)  // 且可见
                {
                    if (Global.Money > prices[i])
                    {
                        switch (i)
                        {
                            case 0: Global.player1DynamiteNum++; if (Global.gameMode) Global.player2DynamiteNum++; break;
                            case 1: Global.HasBuyStrengthBuff = true; break;
                            case 2: Global.LuckyBuff = true; break;
                            case 3: Global.GemPolishBuff = true; break;
                            case 4: Global.RockBuff = true; break;
                        }
                        GetChild<TextureRect>(i).Visible = false;
                        Global.Money -= prices[i];
                    }
                }
            }
        }
    }
    private void On_TextureRect_MouseEntered(int index)
    {
        if (GetChild<TextureRect>(index).Visible)  // 且可见
        {
            GetChild<TextureRect>(index).SelfModulate = Colors.Gray;
            GetParent().GetNode<Label>("Label").Text = describes[index];
        }
    }
    private void On_TextureRect_MouseExited()
    {
        for (int i = 0; i < 5; i++)
        {
            GetChild<TextureRect>(i).SelfModulate = Colors.White;
        }
        GetParent().GetNode<Label>("Label").Text = "";
    }
}
