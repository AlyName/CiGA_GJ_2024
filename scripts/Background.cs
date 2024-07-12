using Godot;
using System;

public partial class Background : Sprite2D
{
	public bool set_run=false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		if(set_run){
			// Position+=new Vector2(-1,0);
			// if(Position.X<960-93*6){
			// 	Position=new Vector2(960,540);
			// }
		}
	}
}
