using Godot;
using System;

public partial class InfoBoard : Sprite2D
{
	public static InfoBoard Instance;
	[Export]
	public Texture2D up,middle,down;

	public InfoBoard(){
		Instance = this;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
	}

	public void set_up(){
		Texture=up;
		UI.get_ui().get_fire_arrow().Animation="red";
		UI.get_ui().get_fire_arrow().Play();
	}
	public void set_down(){
		Texture=down;
		UI.get_ui().get_fire_arrow().Animation="blue";
		UI.get_ui().get_fire_arrow().Play();
	}
	public void set_center(){
		Texture=middle;
		UI.get_ui().get_fire_arrow().Frame=0;
		UI.get_ui().get_fire_arrow().Stop();
	}
}
