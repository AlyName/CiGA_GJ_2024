using Godot;
using System;
using System.Diagnostics;

public partial class MonoControl : Node{

	public enum Gamestate{
		Start,
		GameInit,
		LevelInit,
		InLevel,
		// RoundInit,
		// Carding,
		// NextRound,
		NextLevel,
		GameOver
	}
	public static MonoControl instance;

	public Gamestate now_state=Gamestate.Start;
	public CardEvent now_event=null;

	public LevelManager now_level=null;
	public int now_level_num=0;

	public MonoControl(){instance=this;}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		check_state();
		do_state();

	}

	void check_state(){
	    if(now_state==Gamestate.Start){
			if(now_event!=null){
				if(now_event.event_type==CardEvent.Type.GameStart){
					event_discard();
					now_state=Gamestate.GameInit;
				}else if(now_event.event_type==CardEvent.Type.GameEnd){
				    GetTree().Quit();
				}
			}
		}else if(now_state==Gamestate.GameInit){
		    now_level_num=1;
			now_state=Gamestate.LevelInit;
		}else if(now_state==Gamestate.LevelInit){
			now_level=new LevelManager(now_level_num);
			now_state=Gamestate.InLevel;
		}else if(now_state==Gamestate.InLevel){
		    if(now_level.level_end()==1){
				now_state=Gamestate.NextLevel;
			}else if(now_level.level_end()==-1){
				now_state=Gamestate.GameOver;
			}
		    // level_goal_score=1000*now_level;
			// level_max_round=2+now_level;
			// now_score=0;
			// now_round=1;
		// 	now_state=Gamestate.RoundInit;
		// }else if(now_state==Gamestate.RoundInit){
		// 	generate_cards();
		//     now_state=Gamestate.Carding;
		// }else if(now_state==Gamestate.Carding){
		// 	if(now_event!=null&&now_event.event_type==CardEvent.Type.AddScore){
		// 		now_score+=now_event.score;
		// 		event_discard();
		// 		if(left_card()==0){
		// 			now_state=Gamestate.NextRound;
		// 		}
		// 	}
		// }else if(now_state==Gamestate.NextRound){
		// 	if(now_score>=level_goal_score){
		// 		now_state=Gamestate.NextLevel;
		// 	}else if(now_round>=level_max_round){
		// 		now_state=Gamestate.GameOver;
		// 	}else{
		// 		now_round++;
		// 		now_state=Gamestate.RoundInit;
		// 	}
		}else if(now_state==Gamestate.NextLevel){
			now_level_num++;
			now_state=Gamestate.LevelInit;
		}else if(now_state==Gamestate.GameOver){
			Main.get_main().GetNode<Label>("Description").Text="Game Over";
		    now_level_num=0;
			CardGenerator.get_generator().generate_card("Start");
			now_state=Gamestate.Start;
		}
	}

	void do_state(){
	    if(now_state==Gamestate.InLevel){
			MonoControl.get_control().GetNode<Label>("Label").Text="Debug: Level="+now_level.level_id.ToString()+" Score="+now_level.now_score.ToString()+"+"+now_level.round_score.ToString()+"/"+now_level.goal_score.ToString()+" Round="+now_level.now_round.ToString()+"/"+now_level.max_round.ToString();
        
			if(now_event!=null){
				now_level.handle_event(now_event);
				event_discard();
			}
		}
	}

	public void generate_cards(){
	    int m=(int)(GD.Randi()%5)+2;
		for(int i=1;i<=m;i++){
			if(GD.Randf()<0.25){
				CardGenerator.get_generator().generate_card("Sum",(int)(GD.Randi()%10));
			}else if(GD.Randf()<0.25){
				CardGenerator.get_generator().generate_card("Multiply2",-(int)(GD.Randi()%100));
			}else{
				CardGenerator.get_generator().generate_card("Default",(int)(GD.Randi()%100));
			}
		}
	}

	public bool add_card_event(CardEvent nevent){
		now_event=nevent;
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
