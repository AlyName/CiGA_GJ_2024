using Godot;
using System.IO;
using System.Linq;
using SimpleJSON;
using System.Collections.Generic;
using cfg.item;

public partial class DataLoader : Node2D{
	public static cfg.Tables tables;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("-----------------");
		tables = new cfg.Tables(LoadJson);
		GD.Print(tables.TbItem.DataList.FirstOrDefault()?.ToString());
		GD.Print(tables.TbProbs.DataList.FirstOrDefault()?.ToString());
		GD.Print("-----------------");
	}

	private static JSONNode LoadJson(string file)
	{
		using var fileJson = Godot.FileAccess.Open($"res://TableJson/{file}.json", Godot.FileAccess.ModeFlags.Read);
		var content = fileJson.GetAsText();
		return JSON.Parse(content);
	}

	public static List<cfg.Item> get_card_data(){
		return tables.TbItem.DataList;
	}

	public static List<cfg.Probs> get_probs_data(){
		return tables.TbProbs.DataList;
	}
}
