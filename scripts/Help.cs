using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Help : Node2D
{
	[Export]
	public Texture2D[] helps;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
	}

	public async Task display_help(){
		MouseInput.get_mouse_input().is_abled=false;
		int hc=helps.Length;
		List<Sprite2D> sp_list=new List<Sprite2D>();
		for(int i=0;i<hc;i++){
			Sprite2D sp = new Sprite2D();
			sp.Texture=helps[i];
			sp.Modulate=new Color(1,1,1,0);
			sp.ZIndex=hc-i;
			sp.Position=new Vector2(0,0);
			AddChild(sp);
			sp_list.Add(sp);
			Tween mTween = GetTree().CreateTween();
			mTween.TweenProperty(sp,"modulate",new Color(1,1,1,1),0.5f);
		}

		for(int i=0;i<hc;i++){
			await Main.get_main().wait_time(5f);
			Tween mTween = GetTree().CreateTween();
			mTween.TweenProperty(sp_list[i],"modulate",new Color(1,1,1,0),0.5f);
		}
		
		await Main.get_main().wait_time(1f);
		for(int i=0;i<hc;i++){
			sp_list[i].QueueFree();
		}
		MouseInput.get_mouse_input().is_abled=true;
	}
}
