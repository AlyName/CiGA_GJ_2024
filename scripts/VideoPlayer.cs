using Godot;
using System;
using System.Threading.Tasks;

public partial class VideoPlayer : VideoStreamPlayer
{
	public static VideoPlayer Instance;
	public VideoPlayer(){
		Instance=this;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		Visible=false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
	}

	public async Task change_and_play(string path,float nt){
		// TODO
		// VideoStreamPlayer nv=new VideoStreamPlayer();
		Stream = GD.Load<VideoStreamTheora>(path);
		Visible=true;
		Play();
		// AddChild(nv);
		await Main.get_main().wait_time(nt);
		// nv.QueueFree();
		Stop();
		Visible=false;
	}
}
