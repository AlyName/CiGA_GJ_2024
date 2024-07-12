using Godot;
using System;

public partial class Sun : Sprite2D{
	[Export]
	public Texture2D[] suns;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 pos=Position;
		Vector2 mouse_pos=MouseInput.get_mouse_input().mouse_pos;
		if((pos-mouse_pos).Length()<200)Texture=suns[0];
		else if(mouse_pos.Y<pos.Y)Texture=suns[1];
		else if(Math.Abs(pos.X-mouse_pos.X)<200)Texture=suns[2];
		else if(mouse_pos.X<pos.X)Texture=suns[3];
		else Texture=suns[4];
	}
}
