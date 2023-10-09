using Godot;
using Godot.Collections;
using System;

public partial class Save : GodotObject
{
    static public void SaveGame()
    {
        FileAccess saveFile = FileAccess.Open("user://game.save", FileAccess.ModeFlags.Write);
        Dictionary<string, Variant> data = new Dictionary<string, Variant>()
        {
            {"Level", Global.currentLevel},
            {"Money", Global.Money}
        };
        string jsonStr = Json.Stringify(data);
        saveFile.StoreLine(jsonStr);
        // saveFile.GetVar()
        GD.Print(jsonStr);
    }
}
