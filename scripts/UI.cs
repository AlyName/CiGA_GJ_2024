using Godot;
using System;
using System.Diagnostics;

using System.Threading.Tasks;
public partial class UI : Control{
	[Export]
	public Vector2 up_std_pos=new Vector2(0,0),up_up_pos=new Vector2(0,-240);
	[Export]
	public Texture2D[] label_mid_texture;
	public static UI instance;
	public UI(){
		instance = this;
	}

	int label_a_n_num,label_b_n_num,label_a_goal_num,label_b_goal_num;
	NumTween label_a,label_b;
	RichTextLabel label_name;
	Label label_desc,label_desc_2,label_bottom_a,prolog_label;
	Sprite2D sun,up_s,setting,label_mid,info_board,title;
	AnimatedSprite2D card_name,finger,fire_arrow;
	Vector2 label_a_offset,label_b_offset,label_mid_offset,sun_offset,up_s_offset,card_name_offset,label_name_offset,info_board_offset
	,label_desc_offset,label_desc_2_offset;

	Node2D point_light_2d,effect_node;

	Tween m_tween;
	bool has_open=false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		label_a=GetNode<NumTween>("LabelA");
		label_b=GetNode<NumTween>("LabelB");
		label_mid=GetNode<Sprite2D>("LabelMid");
		label_desc=GetNode<Label>("LabelDescription");
		label_desc_2=GetNode<Label>("LabelDescription2");
		label_bottom_a=GetNode<Label>("LabelBottomA");
		label_name=GetNode<RichTextLabel>("LabelName");
		sun=Main.get_main().GetNode<Sprite2D>("UISprites/Sun");
		up_s=Main.get_main().GetNode<Sprite2D>("UISprites/Up");
		card_name=Main.get_main().GetNode<AnimatedSprite2D>("UISprites/CardName");
		setting=Main.get_main().GetNode<Sprite2D>("UISprites/Setting");
		finger=Main.get_main().GetNode<AnimatedSprite2D>("UISprites/Finger");
		info_board=Main.get_main().GetNode<Sprite2D>("UISprites/InfoBoard");
		title=Main.get_main().GetNode<Sprite2D>("UISprites/Title");
		fire_arrow=Main.get_main().GetNode<AnimatedSprite2D>("UISprites/FireArrow");
		point_light_2d=Main.get_main().GetNode<Node2D>("UISprites/PointLight2D");
		effect_node=Main.get_main().GetNode<Node2D>("EffectDisplay");
		prolog_label=GetNode<Label>("PrologLabel");
		title.Visible=false;
		title.Modulate=new Color(0,0,0,0);
		label_a_offset=label_a.Position;
		label_b_offset=label_b.Position;
		label_mid_offset=label_mid.Position;
		sun_offset=sun.Position;
		up_s_offset=up_s.Position;
		card_name_offset=card_name.Position;
		label_name_offset=label_name.Position;
		info_board_offset=info_board.Position;
		label_desc_offset=label_desc.Position;
		label_desc_2_offset=label_desc_2.Position;
		finger_anim_loop();
	}
	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		set_all_position();
	}

	void set_all_position(){
		Size=GetViewport().GetVisibleRect().Size;
		Position=GetViewport().GetVisibleRect().Position;
		CardWheel.get_wheel().center=new Vector2(1920,1200);

		label_a.Position=label_a_offset+up_std_pos;
		label_b.Position=label_b_offset+up_std_pos;
		label_mid.Position=label_mid_offset+up_std_pos;
		sun.Position=sun_offset+up_std_pos;
		up_s.Position=up_s_offset+up_std_pos;
		card_name.Position=card_name_offset+up_std_pos;
		label_name.Position=label_name_offset+up_std_pos;
		info_board.Position=info_board_offset+up_std_pos;
		label_desc.Position=label_desc_offset+up_std_pos;
		label_desc_2.Position=label_desc_2_offset+up_std_pos;
		// 2*GetNode<Control>("CardDeck").GetRect().Position+GetNode<Control>("CardDeck").GetRect().Size+new Vector2(0,150);
		if(label_a_goal_num!=label_a_n_num){
			label_a.NumTweenScroll(numStart:label_a_n_num,numEnd:label_a_goal_num,tweenTime:0.5f);
			label_a_n_num=label_a_goal_num;
		}
		if(label_b_goal_num!=label_b_n_num){
			if(Math.Abs(label_b_goal_num-label_b_n_num)>100){
			    label_b.NumTweenSize(numStart:label_b_n_num,numEnd:label_b_goal_num,targetScale:1.5f,tweenTime:1f,size2TarTime:0.25f);
				shake_label_b();
			}else{
		    	label_b.NumTweenScroll(numStart:label_b_n_num,numEnd:label_b_goal_num,tweenTime:0.5f);
			}
			label_b_n_num=label_b_goal_num;
		}
		var n_card=CardWheel.get_wheel().front_card();
		if(n_card==null){
			//TODO
		}else{
			label_name.Text="[center]"+n_card.card_name;
		}
		// Debug.WriteLine("label_b_n_num:"+label_b_n_num.ToString());
		// Debug.WriteLine("label_b_goal_num:"+label_b_goal_num.ToString());
	}

	async Task shake_label_b(){
	    label_b.SetShake();
		await Main.get_main().wait_time(2f);
		label_b.shake=false;
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
		if(nn<0)nn=0;
		label_a_goal_num=nn;
	}

	public void set_label_b_number(int nn){
		if(nn<0)nn=0;
		label_b_goal_num=nn;
	}

	public void label_a_boom(){
		Vector2 pos=label_a.Position+label_a.GetRect().Size/2;
		Particles.get_particles().explode_ring(pos);
	}

	public void label_b_boom(){
	    Vector2 pos=label_b.Position+label_b.GetRect().Size/2;
		Particles.get_particles().explode_ring(pos);
	}

	public void set_label_mid_numbers(int na,int nb){
		// Label label_mid=GetNode<Label>("Scores/PanelMid/LabelMid");
		label_mid.Texture=label_mid_texture[na];
	}

	public void set_description(string s){
		// Label label_desc=GetNode<Label>("Description/PanelDescription/LabelDescription");
		label_desc.Text=s;
	}

	public void set_description_2(string s){
		// Label label_desc=GetNode<Label>("Description/PanelDescription/LabelDescription");
		label_desc_2.Text=s;
	}

	public void set_label_bottom_a_number(int nn){
		if(nn==-1)label_bottom_a.Text="";
		// Label label_bottom_a=GetNode<Label>("Bottom/PanelBottomA/LabelBottomA");
		else label_bottom_a.Text=nn.ToString();
	}
	public void set_effect(EffectManager effect_manager){
		// TODO: effect移到这里
		// Label debug=GetNode<Label>("Bottom/PanelBottomMid/EffectDebug");
		// string effects="Debug Effects: ";
		// for(int i=0;i<effect_manager.effect_queue.Count;i++){
		// 	Tuple<EffectManager.Type,int> nt=effect_manager.effect_queue[i];
		// 	effects+=nt.Item1.ToString()+":"+nt.Item2.ToString()+" ";
		// }
		// debug.Text=effects;
		// debug.Text="Debug Effect: next card bonus:"+effects.next_card_bonus.ToString()+" next round bonus card:"+effects.next_round_bonus_card.ToString();
	}

	public void hide_all(){
		m_tween?.Kill();
		
		m_tween = GetTree().CreateTween();
		m_tween.TweenProperty(this, "up_std_pos", up_up_pos, 0.5)
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out);
		// GetNode<Control>("Scores").Hide();
		// GetNode<Control>("Bottom").Hide();
	}

	public void show_all(){
		m_tween?.Kill();
		
		m_tween = GetTree().CreateTween();
		m_tween.TweenProperty(this, "up_std_pos", new Vector2(0,0), 0.5)
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out);
	    // GetNode<Control>("Scores").Show();
		// GetNode<Control>("Bottom").Show();
	}

	

	public Sprite2D get_setting(){
		return setting;
	}
	// public Control get_bottom_mid(){
	// 	return GetNode<Control>("Bottom/PanelBottomMid");
	// }
	public Vector2 get_bottom_mid_position(){
		return new Vector2(1450,1030);
	}

	async Task finger_anim_loop(){
		finger.Play("Idle");
		// DANGER!!
		while(true){
			await Main.get_main().wait_time(1.25f*7);
			finger.Play("Click");
			await Main.get_main().wait_time(1.25f);
			finger.Play("Idle");
		}
	}

	public async Task open_and_close_card_name(){
		if(has_open)return;
		has_open=true;
		card_name.Play("mono");
		label_name.Visible=false;
		await Main.get_main().wait_time(0.5f);
		label_name.Visible=true;
		Particles.get_particles().explode_orbs(label_name.Position+label_name.GetRect().Size/2,32);
		await Main.get_main().wait_time(9.5f);
		label_name.Visible=false;
		Particles.get_particles().explode_orbs(label_name.Position+label_name.GetRect().Size/2,32);
		card_name.Play("inv");
		has_open=false;
	}

	public async Task display_title(){
		title.Modulate=new Color(0,0,0,0);
		title.Visible=true;
		Tween mTween=GetTree().CreateTween();
		mTween.TweenProperty(title, "modulate", new Color(1,1,1,1), 0.5f);
	}
	public async Task hide_title(){
		Tween mTween=GetTree().CreateTween();
		mTween.TweenProperty(title, "modulate", new Color(0,0,0,0), 0.5f);
		await Main.get_main().wait_time(0.5f);
		title.Visible=false;
		title.Modulate=new Color(0,0,0,0);
	}

	public AnimatedSprite2D get_fire_arrow(){
		return fire_arrow;
	}
	public Label get_prolog_label(){
		return GetNode<Label>("PrologLabel");
	}

	public void light_off(){
		point_light_2d.Visible=false;
	}

	public async Task light_on(){

		point_light_2d.Visible=true;
		for(int i=1;i<=4;i++){
			await Main.get_main().wait_time(0.1f);
			point_light_2d.Visible=false;
			await Main.get_main().wait_time(0.1f);
			point_light_2d.Visible=true;
		}

		point_light_2d.Visible=true;
	}

	public void clear_effect_node(){
		foreach(Node node in effect_node.GetChildren()){
			node.QueueFree();
		}
	}


	public static UI get_ui(){
		return instance;
	}
}
