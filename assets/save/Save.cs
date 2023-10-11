using Godot;
using Godot.Collections;
using System;

public partial class Save : GodotObject
{
    static public void SaveGame(int numb, bool newGame)
    {
        if (!FileAccess.FileExists("user://game.save"))
        {
            FileAccess.Open("user://game.save", FileAccess.ModeFlags.Write);
        }
        FileAccess saveFile = FileAccess.Open("user://game.save", FileAccess.ModeFlags.Read);
        string jsonString = saveFile.GetAsText();
        Json json = new Json();
        Error parseResult = json.Parse(jsonString);
        if (parseResult != Error.Ok)
        {
            GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
        }

        Dictionary<string, Dictionary<string, Variant>> data = new Dictionary<string, Dictionary<string, Variant>>((Dictionary)json.Data);   
        saveFile.Close();

        // 以上是读取存档，并转换为字典
        // 然后下面则是将字典中数据覆盖,保存游戏

        saveFile = FileAccess.Open("user://game.save", FileAccess.ModeFlags.Write);

        if (newGame)
        {
            data[numb.ToString()] = new Dictionary<string, Variant>();
        }

        string jsonStr = Json.Stringify(data);
        saveFile.StoreString(jsonStr);
        saveFile.Close();
        GD.Print(jsonStr);
    }
    static public void LoadGame()
    {
        if (!FileAccess.FileExists("user://game.save"))
        {
            return; // Error! We don't have a save to load.
        }
        FileAccess saveFile = FileAccess.Open("user://game.save", FileAccess.ModeFlags.Read);
        string jsonString = saveFile.GetAsText();
        Json json = new Json();
        Error parseResult = json.Parse(jsonString);
        if (parseResult != Error.Ok)
        {
            GD.Print($"JSON Parse Error: {json.GetErrorMessage()} in {jsonString} at line {json.GetErrorLine()}");
        }

        Dictionary<string, Variant> data = new Dictionary<string, Variant>((Dictionary)json.Data);
        saveFile.Close();
        GD.Print(data);
        GD.Print(data["InShop"]);
        GD.Print(data["Money"]);
        GD.Print(data["Level"]);
    }
}
