using Godot;
using System;

public partial class Goods : Control
{
    public override void _Ready()
    {
        for (int i = 0; i < 5; i++)
        {
            if (GD.RandRange(1, 2) == 1)
            {
                GetChild<TextureRect>(i).Visible = false;
            }
        }
    }
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton inputEventMouseButton && inputEventMouseButton.IsReleased() && inputEventMouseButton.ButtonIndex == MouseButton.Left)
        {
            for (int i = 0; i < 5; i++)
            {
                if (GetChild<TextureRect>(i).GetRect().HasPoint(inputEventMouseButton.Position) && GetChild<TextureRect>(i).Visible)  // 且可见
                {
                    GD.Print("点击了第", i, "件商品");
                }
            }
        }
    }
    private void On_TextureRect_MouseEntered(int index)
    {
        if (GetChild<TextureRect>(index).Visible)  // 且可见
        {
            GD.Print("选中了第" + index + "件商品");
            GetChild<TextureRect>(index).Modulate = Colors.Gray;
        }
    }
    private void On_TextureRect_MouseExited()
    {
        for (int i = 0; i < 5; i++)
        {
            GetChild<TextureRect>(i).Modulate = Colors.White;
        }
    }
}
