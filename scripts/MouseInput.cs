using Godot;
using System;

public partial class MouseInput : Node{

	[Export]
	public string mouse_on_node_name,last_on_node_name;
	public Vector2 mouse_pos,mouse_v;
	public bool is_mouse_press,n_press;

	public float time_until_last_press=0;

	private Vector2 last_mouse_pos;

	public static MouseInput instance;

	public MouseInput(){
		instance=this;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		calc_mouse((float)delta);
		calc_mouse_on_node();
	}

	public override void _Input(InputEvent @event){
		if (@event is InputEventMouseButton eventMouseButton){
			if (eventMouseButton.ButtonIndex == MouseButton.Left){
				if(eventMouseButton.Pressed){
					n_press=true;
				}else{
					n_press=false;
				}
				// if(eventMouseButton.Pressed&&(sprite.GetRect()).HasPoint(ToLocal(GetGlobalMousePosition()))){
				// 	_dragging = true;
				// }else{
				// 	_dragging = false;
				// }
			}
		}
	}

	private void calc_mouse(float delta){
		mouse_pos=GetViewport().GetMousePosition();
		mouse_v=(mouse_pos-last_mouse_pos)/delta;
		last_mouse_pos=mouse_pos;

		is_mouse_press=n_press;
		if(is_mouse_press){
			time_until_last_press=0;
		}else{
			time_until_last_press+=delta;
		}
	}

	private void calc_mouse_on_node(){
		if(is_mouse_press)return;
		mouse_on_node_name="";

		foreach(Node node in Main.get_main().GetChildren()){
			if(node is CardMove card){
				
				if(card.can_drag==1&&card.GetRect().HasPoint(card.ToLocal(mouse_pos))){
					if(Main.get_main().GetNodeOrNull(mouse_on_node_name) is null){
						mouse_on_node_name=card.Name;
					}else if((Main.get_main().GetNode(mouse_on_node_name) as Sprite2D).ZIndex<card.ZIndex){
						mouse_on_node_name=card.Name;
					}
				}
			}
		}

		if(is_mouse_press){
			last_on_node_name=mouse_on_node_name;
		}
	}

	public static MouseInput get_mouse_input(){
		return instance;
	}
}
