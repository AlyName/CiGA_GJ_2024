using Godot;
using System;
using System.Threading.Tasks;

public partial class Camera : Camera2D{
	public static Camera instance;
	public Camera(){
		instance=this;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public async Task shake(float duration, float strength,float eta=0.95f){
		for(int i=0; i<20*duration; i++){
			Position=new Vector2(GD.Randf()*strength, GD.Randf()*strength);
			await Main.get_main().wait_time(0.01f);
			strength*=eta;
		}
		Position=Vector2.Zero;
	}
	public static Camera get_camera(){
		return instance;
	}
}
