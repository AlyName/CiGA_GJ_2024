using Godot;
using System;
using System.Diagnostics;

public partial class UI : VBoxContainer{
	public static UI instance;
	public UI(){
		instance = this;
	}

	int label_a_n_num,label_b_n_num,label_a_goal_num,label_b_goal_num;
	NumTween label_a,label_b;
	Label label_mid,label_desc,label_bottom_a;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		label_a=GetNode<NumTween>("Scores/PanelA/LabelA");
		label_b=GetNode<NumTween>("Scores/PanelB/LabelB");
		label_mid=GetNode<Label>("Scores/PanelMid/LabelMid");
		label_desc=GetNode<Label>("Description/PanelDescription/LabelDescription");
		label_bottom_a=GetNode<Label>("Bottom/PanelBottomA/LabelBottomA");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		set_all_position();
	}

	void set_all_position(){
		Size=GetViewport().GetVisibleRect().Size;
		Position=GetViewport().GetVisibleRect().Position;
		CardWheel.get_wheel().center=2*GetNode<Control>("CardDeck").GetRect().Position+GetNode<Control>("CardDeck").GetRect().Size;
		if(label_a_goal_num!=label_a_n_num){
			label_a.NumTweenScroll(numStart:label_a_n_num,numEnd:label_a_goal_num,tweenTime:0.5f);
			label_a_n_num=label_a_goal_num;
		}
		// Debug.WriteLine("label_a_n_num:"+label_a_n_num.ToString());
		// Debug.WriteLine("label_a_goal_num:"+label_a_goal_num.ToString());
		if(label_b_goal_num!=label_b_n_num){
			if(Math.Abs(label_b_goal_num-label_b_n_num)>100){
			    label_b.NumTweenSize(numStart:label_b_n_num,numEnd:label_b_goal_num,targetScale:1.5f,tweenTime:0.5f);
			}else{
		    	label_b.NumTweenScroll(numStart:label_b_n_num,numEnd:label_b_goal_num,tweenTime:0.5f);
			}
			label_b_n_num=label_b_goal_num;
		}
		// Debug.WriteLine("label_b_n_num:"+label_b_n_num.ToString());
		// Debug.WriteLine("label_b_goal_num:"+label_b_goal_num.ToString());
	}

	// int smooth_change_number(int n_num,int goal_num){
	// 	if(goal_num<0)goal_num=0;
	// 	if(Math.Abs(goal_num-n_num)>10){
	// 		return n_num+(goal_num-n_num)/5;
	// 	}else{
	// 		if(goal_num>n_num){
	// 		    return n_num+1;
	// 		}else if(goal_num<n_num){
	// 			return n_num-1;
	// 		}else{
	// 			return n_num;
	// 		}
	// 	}
	// }

	public void set_label_a_number(int nn){
		label_a_goal_num=nn;
	}

	public void set_label_b_number(int nn){
		label_b_goal_num=nn;
	}

	public void label_a_boom(){
		Vector2 pos=GetNode<Panel>("Scores/PanelA").Position+GetNode<Panel>("Scores/PanelA").GetRect().Size/2;
		Particles.get_particles().explode_ring(pos);
	}

	public void label_b_boom(){
	    Vector2 pos=GetNode<Panel>("Scores/PanelB").Position+GetNode<Panel>("Scores/PanelB").GetRect().Size/2;
		Particles.get_particles().explode_ring(pos);
	}

	public void set_label_mid_numbers(int na,int nb){
		// Label label_mid=GetNode<Label>("Scores/PanelMid/LabelMid");
		label_mid.Text=na.ToString()+" / "+nb.ToString();
	}

	public void set_description(string s){
		// Label label_desc=GetNode<Label>("Description/PanelDescription/LabelDescription");
		label_desc.Text=s;
	}

	public void set_label_bottom_a_number(int nn){
		// Label label_bottom_a=GetNode<Label>("Bottom/PanelBottomA/LabelBottomA");
		label_bottom_a.Text=nn.ToString();
	}
	public void set_effect(EffectManager effect_manager){
		Label debug=GetNode<Label>("Bottom/PanelBottomMid/EffectDebug");
		string effects="Debug Effects: ";
		for(int i=0;i<effect_manager.effect_queue.Count;i++){
			Tuple<EffectManager.Type,int> nt=effect_manager.effect_queue[i];
			effects+=nt.Item1.ToString()+":"+nt.Item2.ToString()+" ";
		}
		debug.Text=effects;
		// debug.Text="Debug Effect: next card bonus:"+effects.next_card_bonus.ToString()+" next round bonus card:"+effects.next_round_bonus_card.ToString();
	}

	public void hide_all(){
		GetNode<Control>("Scores").Hide();
		GetNode<Control>("Bottom").Hide();
	}

	public void show_all(){
	    GetNode<Control>("Scores").Show();
		GetNode<Control>("Bottom").Show();
	}

	public Control get_bottom_b(){
		return GetNode<Control>("Bottom/PanelBottomB");
	}

	public static UI get_ui(){
		return instance;
	}
}
