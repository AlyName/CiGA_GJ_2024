using Godot;
using System;
using System.Diagnostics;

public partial class CardMove : Sprite2D{
	[Export]
	public int card_id,card_pos;
	private Vector2 _initialPosition;
	private bool _dragging=false;
	private Timer _returnTimer;
	[Export]
	public float _returnSpeed = 5f;
	[Export]
	public float YBound=100f;
	[Export]
	public float limit_r=5f,shake_r=4f;
	private float upper_y,lower_y;
	public int state=0;
	/*
	0 - Initial
	1 - Dragging
	2 - Dragged out
	//3 - Returning
	*/
	private Rect2 viewrect;
	private float drag_original_y;
	public int is_use,can_drag=0,leave_place=0;

	public Vector2 n_scale=new Vector2(1,1);
	
	public override void _Ready(){
		_returnTimer = new Timer();
		AddChild(_returnTimer);
		_returnTimer.WaitTime = 0.01f;
		_returnTimer.OneShot = false;
		_returnTimer.Connect("timeout", new Callable(this, nameof(OnReturnTimerTimeout)));

		viewrect=GetViewport().GetVisibleRect();
		upper_y = viewrect.Position.Y + viewrect.Size.Y-YBound;
		lower_y = viewrect.Position.Y+YBound;
	}
	

	public override void _Process(double delta){
		check_dragging();
		check_state();
		do_state();

		check_sprite();
		
	}

	private void check_dragging(){
		if(get_mouse_input().mouse_on_node_name==Name){
			if(can_drag==1){
				// Debug.WriteLine("on"+card_id.ToString());
				_dragging=get_mouse_input().is_mouse_press;
			}
		}else{
			_dragging=false;
		}
	}

	private void check_state(){
		if(state==0){
			if(_dragging){
				_initialPosition = Position;
				drag_original_y=_initialPosition.Y;
				//TODO
				leave_place=GetNode<CardWheel>("../CardWheel").delete_card(card_id);
				ZIndex=100;
				state=1;
			}
		}else if(state==1){
			if(!_dragging){
				if(Position.Y>upper_y){
					is_use=1;
					state=2;
				}else if(Position.Y<lower_y){
					is_use=0;
					state=2;
				}else{
					// _returnTimer.Start();
					// state=3;
					// TODO
					GetNode<CardWheel>("../CardWheel").add_card(card_id,leave_place);
					state=0;
				}
			}
		}else if(state==2){
			//TODO
			// delete
			n_scale-=new Vector2(0.05f,0.05f);
			if(n_scale.X<0.1f)QueueFree();
			return;
		}else if(state==3){
			Vector2 direction = _initialPosition - Position;
			float distance = direction.Length();
			if (distance < limit_r){
				Position = _initialPosition;
				_returnTimer.Stop();
				state=0;
			}
		}
	}

	private void do_state(){
		if(state==1){
			can_drag=1;
			float top_y=viewrect.Position.Y+viewrect.Size.Y,bottom_y=viewrect.Position.Y;
			float mouse_y=get_mouse_input().mouse_pos.Y;
			Position=get_mouse_input().mouse_pos;
			if(Position.Y>upper_y-shake_r||Position.Y<lower_y+shake_r){
				Vector2 random_shake=new Vector2(GD.Randf()*shake_r*2-shake_r,GD.Randf()*shake_r*2-shake_r);
				Position=Position+random_shake;
				n_scale=new Vector2(1.2f,1.2f);
			}else{
				n_scale=new Vector2(1,1);
			}
			Modulate=new Color(1,1,1,1);
		}
	}

	private void check_sprite(){
		GetNode<Label>("Label").Text="# "+card_id.ToString();
		Scale+=(n_scale-Scale)*0.1f;
	}

	private void OnReturnTimerTimeout()
	{
		Vector2 direction = _initialPosition - Position;
		float distance = direction.Length();
		Position = (_initialPosition - Position) * _returnSpeed * (float)_returnTimer.WaitTime + Position;
		
	}

	private Main get_main(){return GetNode("/root/Main") as Main;}
	private MouseInput get_mouse_input(){return get_main().GetNode("MouseInput") as MouseInput;}
}
