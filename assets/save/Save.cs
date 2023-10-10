using Godot;
using Godot.Collections;
using System;

public partial class Save : GodotObject
{
    static public void SaveGame()
    {
        FileAccess saveFile = FileAccess.Open("user://game.save", FileAccess.ModeFlags.Write);
        Dictionary<int, Dictionary<string, Variant>> data = new Dictionary<int, Dictionary<string, Variant>>()
        {
            {1, new Dictionary<string, Variant>(){
                {"Level", Global.currentLevel},
                {"Money", Global.Money},
                {"InShop", Global.InShope}
            }},
            {1, new Dictionary<string, Variant>(){

            }},
            {1, new Dictionary<string, Variant>(){

            }},
            {1, new Dictionary<string, Variant>(){

            }},
            {1, new Dictionary<string, Variant>(){

            }},
            {1, new Dictionary<string, Variant>(){

            }}
        };
        // string jsonStr = Json.Stringify(data);
        // saveFile.StoreString(jsonStr);
        // saveFile.Close();
        // GD.Print(jsonStr);
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
