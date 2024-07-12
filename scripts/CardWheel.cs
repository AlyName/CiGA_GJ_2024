using Godot;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public partial class CardWheel : Node2D{
	public static CardWheel instance;
	public CardWheel(){
		instance=this;
	}
	public List<Card> cards=new List<Card>();
	public Stack<List<Tuple<string,int,int>>> stack_history_cards=new Stack<List<Tuple<string,int,int>>>();
	[Export]
	public float eta=120000f,tau=0.25f,circle_a=900f,circle_b=200f,sa=-0.0025f,a_angle=0.05f;
	[Export]
	public Vector2 center;

	private int[] add_wait=new int[101],delete_wait=new int[101],add_pos=new int[101];
	private int add_wait_num,delete_wait_num,now_pressed=0;

	[Export]
	public float angle=0f,press_angle;

	public bool is_fixed;
	public int fixed_angle_index=0;
	private Vector2 press_mouse_pos;
	public override void _Ready(){
		add_wait_num=0;delete_wait_num=0;
		//TODO
		foreach(Node node in GetParent().GetChildren()){
			if(node is Card){
				Card card=node as Card;
				cards.Add(card);
			}
		}
	}

	public override void _Process(double delta){
		handle_add_delete();
		move_cards();
		adjust_angle();
	}

	public int add_card(int card_id,int pos){
		add_wait[add_wait_num++]=card_id;
		add_pos[add_wait_num-1]=pos;
		return pos;
	}

	public int delete_card(int card_id){
		delete_wait[delete_wait_num++]=card_id;
		for(int j=0;j<cards.Count;j++){
			if(cards[j]!=null&&cards[j].card_id==card_id){
				return j;
			}
		}
		return 0;
	}

	private void handle_add_delete(){
		for(int i=0;i<add_wait_num;i++){
			// TODO
			foreach(Node node in GetParent().GetChildren()){
				if(node is Card){
					Card card=node as Card;
					if(card.card_id==add_wait[i]){
						// Debug.WriteLine(i);
						cards.Insert(add_pos[i],card);
						break;
					}
				}
			}
		}
		add_wait_num=0;
		for(int i=0;i<delete_wait_num;i++){
			for(int j=0;j<cards.Count;j++){
				if(cards[j]!=null&&cards[j].card_id==delete_wait[i]){
					cards.RemoveAt(j);
					break;
				}
			}
		}
		delete_wait_num=0;
	}
	private void move_cards(){
		int card_num=cards.Count;


		if(MouseInput.get_mouse_input().is_mouse_press&&MouseInput.get_mouse_input().mouse_on_node_name!="")return;
		// if(MouseInput.get_mouse_input().time_until_last_press<0.1f&&MouseInput.get_mouse_input().last_on_node_name!="")return;

		List<Vector2> circle_p=new List<Vector2>();
		for(int i=0;i<card_num+add_wait_num;i++){
			circle_p.Add(new Vector2(
				(float)Math.Cos(i*2*Math.PI/card_num+angle)*circle_a,
				(float)Math.Sin(i*2*Math.PI/card_num+angle)*circle_b)
				+center
			);
		}
		for(int i=0;i<card_num;i++){
			Vector2 pi=cards[i].Position;
			Vector2 dest=circle_p[i]-pi;
			cards[i].Position+=(dest-pi)*tau;
		}

		List<float> card_y=new List<float>();
		for(int i=0;i<card_num;i++){
			card_y.Add(cards[i].Position.Y);
		}
		card_y.Sort();
		for(int i=0;i<card_num;i++){
			cards[i].ZIndex=card_y.IndexOf(cards[i].Position.Y)*2+ZIndex;
			if(cards[i].ZIndex==2*(card_num-1)+ZIndex){
				cards[i].can_drag=1;
				cards[i].Modulate=new Color(1,1,1,1);
				cards[i].n_scale=new Vector2(1,1);
			}else{
				cards[i].can_drag=0;
				cards[i].Modulate=new Color(0.35f,0.35f,0.35f,1)+new Color(0.125f,0.125f,0.125f,0)/card_num*cards[i].ZIndex;
				cards[i].n_scale=new Vector2(0.75f,0.75f)+new Vector2(0.125f,0.125f)/card_num*cards[i].ZIndex;
			}
			cards[i].SelfModulate=cards[i].Modulate*cards[i].n_color;
		}

	}
	private void adjust_angle(){
		
		// angle+=v_angle;
		if(angle>2*Math.PI)angle-=2*(float)Math.PI;
		if(angle<0)angle+=2*(float)Math.PI;

		if(MouseInput.get_mouse_input().is_mouse_press&&MouseInput.get_mouse_input().mouse_on_node_name==""){
			if(now_pressed==0){
				press_angle=angle;
				press_mouse_pos=MouseInput.get_mouse_input().mouse_pos;
			}
			now_pressed=1;
			angle=press_angle+(MouseInput.get_mouse_input().mouse_pos-press_mouse_pos).X*sa*calc_mp(cards.Count);
		}else{
			now_pressed=0;
			int card_num=cards.Count;
			if(card_num!=0){
				List<float> circle_angle=new List<float>();
				for(int i=0;i<card_num;i++){
					float nf=(float)(i*2*Math.PI/card_num+Math.PI/2);
					if(nf>2*Math.PI)nf-=2*(float)Math.PI;
					circle_angle.Add(nf);
				}

				int angle_min_index=0;
				for(int i=0;i<card_num;i++){
					float ni=(float)Math.Min(Math.Abs(circle_angle[i]-angle),Math.Abs(circle_angle[i]-angle-2*Math.PI));
					ni=(float)Math.Min(ni,Math.Abs(circle_angle[i]-angle+2*Math.PI));
					float nj=(float)Math.Min(Math.Abs(circle_angle[angle_min_index]-angle),Math.Abs(circle_angle[angle_min_index]-angle-2*Math.PI));
					nj=(float)Math.Min(nj,Math.Abs(circle_angle[angle_min_index]-angle+2*Math.PI));
					if(ni<nj)angle_min_index=i;
				}
				if(is_fixed){
					if(angle_min_index==fixed_angle_index){
						is_fixed=false;
					}else{
						angle_min_index=fixed_angle_index;
					}
				}
				float angle_min=circle_angle[angle_min_index];
				angle+=(angle_min-angle)*a_angle;
			}

		}

	}

	float calc_mp(int n_count){
		if(n_count==0)return 1f;
		else{
			return (float)Math.Pow(6f/n_count,0.7)*1.2f;
		}
	}

	public void rotate_to(int fp){
		int card_num=cards.Count;
		is_fixed=true;
		fixed_angle_index=fp;
	}

	public void clear_cards(){
		List<Tuple<string,int,int>> now_cards=new List<Tuple<string,int,int>>();
		for(int i=0;i<cards.Count;i++){
		    now_cards.Add(new Tuple<string,int,int>(cards[i].card_type_s,cards[i].card_level,cards[i].score));
		}
		stack_history_cards.Push(now_cards);
		for(int i=0;i<cards.Count;i++){
			cards[i].QueueFree();
		}
		cards.Clear();
	}

	public bool return_cards(){
	    if(stack_history_cards.Count==0)return false;
		for(int i=0;i<cards.Count;i++){
			cards[i].QueueFree();
		}
		cards.Clear();
		List<Tuple<string,int,int>> now_cards=stack_history_cards.Pop();
		for(int i=0;i<now_cards.Count;i++){
		    CardGenerator.get_generator().generate_card(now_cards[i].Item1,now_cards[i].Item2,now_cards[i].Item3);
		}
		return true;
	}

	public Card front_card(){
		for(int i=0;i<cards.Count;i++){
			if(cards[i].can_drag==1){
			    return cards[i];
			}
		}
		return null;
	}

	public void clear_stack(){
		while(stack_history_cards.Count!=0)stack_history_cards.Pop();
		UI.get_ui().clear_effect_node();
	}

	public int left_card(){
		return cards.Count+add_wait_num;
	}

	public static CardWheel get_wheel(){
		return instance;
	}

}
