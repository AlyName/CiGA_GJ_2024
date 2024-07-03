using Godot;
using System.IO;
using System.Linq;
using SimpleJSON;

public partial class TableLoader : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("-----------------");
		var tables = new cfg.Tables(LoadJson);
		GD.Print(tables.TbItem.DataList.FirstOrDefault()?.ToString());
		GD.Print("-----------------");
	}

	private static JSONNode LoadJson(string file)
	{
		using var fileJson = Godot.FileAccess.Open($"res://TableJson/{file}.json", Godot.FileAccess.ModeFlags.Read);
		var content = fileJson.GetAsText();
		return JSON.Parse(content);
	}
}