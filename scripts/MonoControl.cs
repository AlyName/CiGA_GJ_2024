using Godot;
using System;
using System.Diagnostics;

public partial class MonoControl : Node{

	public enum Gamestate{
		Start,
		GameInit,
		LevelInit,
		RoundInit,
		Carding,
		NextRound,
		NextLevel,
		GameOver
	}
	public static MonoControl instance;

	public Gamestate now_state=Gamestate.Start;
	public CardEvent now_event=null;

	public int now_level=0;

	public int level_goal_score=0,level_max_round=0,now_score=0,now_round=0;

	public MonoControl(){instance=this;}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		check_state();
		do_state();

		GetNode<Label>("Label").Text="Debug: Level="+now_level.ToString()+" Score="+now_score.ToString()+"/"+level_goal_score.ToString()+" Round="+now_round.ToString()+"/"+level_max_round.ToString();
	}

	void check_state(){
	    if(now_state==Gamestate.Start){
			if(now_event!=null&&now_event.event_type==CardEvent.Type.GameStart){
				event_discard();
				now_state=Gamestate.GameInit;
			}
		}else if(now_state==Gamestate.GameInit){
		    now_level=1;
			now_state=Gamestate.LevelInit;
		}else if(now_state==Gamestate.LevelInit){
		    level_goal_score=1000*now_level;
			level_max_round=2+now_level;
			now_score=0;
			now_round=1;
			now_state=Gamestate.RoundInit;
		}else if(now_state==Gamestate.RoundInit){
			generate_cards();
		    now_state=Gamestate.Carding;
		}else if(now_state==Gamestate.Carding){
			if(now_event!=null&&now_event.event_type==CardEvent.Type.AddScore){
				now_score+=now_event.score;
				event_discard();
				if(left_card()==0){
					now_state=Gamestate.NextRound;
				}
			}
		}else if(now_state==Gamestate.NextRound){
			if(now_score>=level_goal_score){
				now_state=Gamestate.NextLevel;
			}else if(now_round>=level_max_round){
				now_state=Gamestate.GameOver;
			}else{
				now_round++;
				now_state=Gamestate.RoundInit;
			}
		}else if(now_state==Gamestate.NextLevel){
			now_level++;
			now_state=Gamestate.LevelInit;
		}else if(now_state==Gamestate.GameOver){
		    now_level=0;
			Main.get_main().generate_card(-1);
			now_state=Gamestate.Start;
		}
	}

	void do_state(){
	    
	}

	void generate_cards(){
	    int m=10;
		for(int i=1;i<=m;i++){
			Main.get_main().generate_card((int)(GD.Randi()%100));
		}
	}

	public bool add_card_event(CardMove card){
		now_event=new CardEvent();
		if(card.score==-1){
			if(card.is_use==0){
				now_event.event_type=CardEvent.Type.GameStart;
			}else{
				GetTree().Quit();
			}
		}else{
			if(card.is_use==0){
				now_event.event_type=CardEvent.Type.AddScore;
				now_event.score=card.score;
			}else{
				now_event.event_type=CardEvent.Type.AddScore;
				now_event.score=0;
			}
		}
		return true;
	}

	void event_discard(){
	    now_event=null;
	}
	public static MonoControl get_control(){
		return instance;
	}

	public int left_card(){
		return Main.get_main().GetNode<CardWheel>("CardWheel").left_card();
	}
}
