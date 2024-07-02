using Godot;
using System;
using System.IO;
using System.Linq;
using SimpleJSON;

public partial class TableLoader : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var tables = new cfg.Tables(LoadJson);
		
		GD.Print(ProjectSettings.GlobalizePath("res://"));
		GD.Print("-----------------");
		GD.Print(tables.TbItem.DataList.FirstOrDefault()?.ToString());
		GD.Print("-----------------");
	}


	private static JSONNode LoadJson(string file)
	{
		return JSON.Parse(File.ReadAllText($"{ProjectSettings.GlobalizePath("res://")}TableJson/{file}.json", System.Text.Encoding.UTF8));
	}
}
