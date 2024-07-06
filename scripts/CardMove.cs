using Godot;
using System;
using System.Diagnostics;

public partial class CardMove : Sprite2D{
	[Export]
	public int card_id,card_pos;
	private Vector2 _initialPosition;
	private bool _dragging=false;
	private Timer _returnTimer;
	public Label card_label;

	public AnimatedSprite2D animated;
	[Export]
	public float _returnSpeed = 5f;
	[Export]
	public float upper_y_bound=0.85f,lower_y_bound=0.4f;
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
		

		// card_label=new Label();
		// card_label.Position=new Vector2(0,20);
		// card_label.HorizontalAlignment=HorizontalAlignment.Center;
		// AddChild(card_label);

		
		animated.Play();
		AddChild(animated);
		animated.ZIndex=-1;
	}
	

	public override void _Process(double delta){
		check_dragging();
		check_state();
		do_state();

		check_sprite();
		
	}

	private void check_dragging(){
		if(MouseInput.get_mouse_input().mouse_on_node_name==Name){
			if(can_drag==1){
				// Debug.WriteLine("on"+card_id.ToString());
				_dragging=MouseInput.get_mouse_input().is_mouse_press;
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
				leave_place=CardWheel.get_wheel().delete_card(card_id);
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
					CardWheel.get_wheel().add_card(card_id,leave_place);
					state=0;
				}
			}
		}else if(state==2){
			//TODO
			// delete
			n_scale-=new Vector2(0.05f,0.05f);
			if(n_scale.X<0.1f){
				use_card();
				// MonoControl.get_control().add_card_event(this);
				
				QueueFree();
			}
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

	virtual protected void use_card(){

	}

	private void do_state(){
		if(state==1){
			can_drag=1;
			float top_y=viewrect.Position.Y+viewrect.Size.Y,bottom_y=viewrect.Position.Y;
			float mouse_y=MouseInput.get_mouse_input().mouse_pos.Y;
			Position=MouseInput.get_mouse_input().mouse_pos;
			upper_y = viewrect.Position.Y + viewrect.Size.Y*upper_y_bound;
			lower_y = viewrect.Position.Y+viewrect.Size.Y*lower_y_bound;
			if(Position.Y>upper_y-shake_r||Position.Y<lower_y+shake_r){
				Vector2 random_shake=new Vector2(GD.Randf()*shake_r*2-shake_r,GD.Randf()*shake_r*2-shake_r);
				Position=Position+random_shake;
				n_scale=new Vector2(1.2f,1.2f);
			}else{
				n_scale=new Vector2(1f,1f);
			}
			Modulate=new Color(1,1,1,1);
		}
	}

	virtual protected void check_sprite(){
		Scale+=(0.5f*n_scale-Scale)*0.1f;
		
	}

	private void OnReturnTimerTimeout()
	{
		Vector2 direction = _initialPosition - Position;
		float distance = direction.Length();
		Position = (_initialPosition - Position) * _returnSpeed * (float)_returnTimer.WaitTime + Position;
		
	}

}
