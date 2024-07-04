using Godot;
using System;
using System.Diagnostics;
using System.Linq.Expressions;

public partial class MonoControl : Node{

	public enum Gamestate{
		Start,
		StartSettings,
		VideoSetting,
		GameInit,
		LevelInit,
		InLevel,
		Settings,
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
					get_in_state(Gamestate.GameInit);
				}else if(now_event.event_type==CardEvent.Type.GameEnd){
				    GetTree().Quit();
				}else if(now_event.event_type==CardEvent.Type.Settings){
					event_discard();
					get_in_state(Gamestate.StartSettings);
				}
			}
		}else if(now_state==Gamestate.GameInit){
			get_in_state(Gamestate.LevelInit);
		}else if(now_state==Gamestate.LevelInit){
			get_in_state(Gamestate.InLevel);
		}else if(now_state==Gamestate.InLevel){
			if(now_event!=null){
				if(now_event.event_type==CardEvent.Type.Settings){
					event_discard();
					get_in_state(Gamestate.Settings);
				}else if(now_event.event_type==CardEvent.Type.GetIntoNextLevel){
					event_discard();
					get_in_state(Gamestate.NextLevel);
				}else if(now_event.event_type==CardEvent.Type.GameOver){
					event_discard();
					get_in_state(Gamestate.GameOver);
				}
			}
		}else if(now_state==Gamestate.NextLevel){
			if(now_event!=null){
				if(now_event.event_type==CardEvent.Type.NextLevel){
					event_discard();
					now_level_num++;
					get_in_state(Gamestate.LevelInit);
				}else if(now_event.event_type==CardEvent.Type.GameEnd){
					event_discard();
					get_in_state(Gamestate.Start);
				}
			}
		}else if(now_state==Gamestate.GameOver){
			get_in_state(Gamestate.Start);
		}else if(now_state==Gamestate.Settings){
			if(now_event!=null){
			    if(now_event.event_type==CardEvent.Type.Continue){
			        event_discard();
					CardWheel.get_wheel().return_cards();
					get_in_state(Gamestate.InLevel);
			    }else if(now_event.event_type==CardEvent.Type.GameEnd){
					event_discard();
					get_in_state(Gamestate.Start);
				}
			}
		}else if(now_state==Gamestate.StartSettings){
			if(now_event!=null){
			    if(now_event.event_type==CardEvent.Type.Continue){
			        event_discard();
					get_in_state(Gamestate.Start);
			    }else if(now_event.event_type==CardEvent.Type.GameEnd){
					event_discard();
					get_in_state(Gamestate.Start);
				}
				// TODO
				// else if(now_event.event_type==CardEvent.Type.VideoSetting){
				// 	event_discard();

				// 	get_in_state(Gamestate.VideoSetting);
				// }
			}
		}
		//TODO
		// else if(now_state==Gamestate.VideoSetting){
		// 	if(now_event!=null){
				
		// 	}
		// }
	}

	void do_state(){
	    if(now_state==Gamestate.InLevel){
			// TODO
			UI.get_ui().set_label_b_number(now_level.round_score);
			UI.get_ui().set_label_a_number(now_level.goal_score-now_level.now_score);
			UI.get_ui().set_label_mid_numbers(now_level.now_round,now_level.max_round);
			UI.get_ui().set_label_bottom_a_number(left_card());
			UI.get_ui().set_effect(now_level.effect_manager);
			// MonoControl.get_control().GetNode<Label>("Label").Text="Debug: Level="+now_level.level_id.ToString()+" Score="+now_level.now_score.ToString()+"+"+now_level.round_score.ToString()+"/"+now_level.goal_score.ToString()+" Round="+now_level.now_round.ToString()+"/"+now_level.max_round.ToString();
        
			if(now_event!=null){
				if(now_level.handle_event(now_event)){
					event_discard();
				}
			}
			if(now_level.level_end()==1){
				CardEvent ne=new CardEvent(CardEvent.Type.GetIntoNextLevel);
				UI.get_ui().set_description("Win!");
				Main.get_main().wait_time_add_event(1f,ne);
			}else if(now_level.level_end()==-1){
				CardEvent ne=new CardEvent(CardEvent.Type.GameOver);
				UI.get_ui().set_description("Lose");
				Main.get_main().wait_time_add_event(1f,ne);
			}
		}
	}

	public void get_in_state(Gamestate next_state){
		if(next_state==Gamestate.Start){
			now_state=next_state;
		    CardWheel.get_wheel().clear_cards();
			CardWheel.get_wheel().clear_stack();
			UI.get_ui().hide_all();
			now_level_num=0;
			CardGenerator.get_generator().generate_card("Setting");
			CardGenerator.get_generator().generate_card("Start");
			CardWheel.get_wheel().rotate_to(0);
		}else if(next_state==Gamestate.StartSettings){
			UI.get_ui().hide_all();
			now_state=next_state;
		    CardWheel.get_wheel().clear_cards();
			CardGenerator.get_generator().generate_card("VA");
			CardGenerator.get_generator().generate_card("Continue");
			CardWheel.get_wheel().rotate_to(0);
		}else if(next_state==Gamestate.GameInit){
			CardWheel.get_wheel().clear_cards();
			CardWheel.get_wheel().clear_stack();
			UI.get_ui().show_all();
			now_level_num=1;
		}else if(next_state==Gamestate.LevelInit){
			CardWheel.get_wheel().clear_cards();
			CardWheel.get_wheel().clear_stack();
			now_level=new LevelManager(now_level_num);
		}else if(next_state==Gamestate.InLevel){
			UI.get_ui().show_all();
		}else if(next_state==Gamestate.Settings){
			UI.get_ui().hide_all();
			now_state=next_state;
			CardWheel.get_wheel().clear_cards();
			CardGenerator.get_generator().generate_card("VA");
			CardGenerator.get_generator().generate_card("Continue");
			CardWheel.get_wheel().rotate_to(0);
		}else if(next_state==Gamestate.NextLevel){
			CardGenerator.get_generator().generate_card("NextLevel");
		}
		
		now_state=next_state;
	}

	public void generate_cards(EffectManager effect_manager){
		int card_bonus=0;
		for(int i=0;i<effect_manager.effect_queue.Count;i++){
			Tuple<EffectManager.Type,int> nt=effect_manager.effect_queue[i];
			if(nt.Item1==EffectManager.Type.RoundCardBonus){
				card_bonus+=nt.Item2+1;
			}
		}
		int m=5+card_bonus;
		int rare_bonus=0;
		for(int i=0;i<effect_manager.effect_queue.Count;i++){
		    Tuple<EffectManager.Type,int> nt=effect_manager.effect_queue[i];
			if(nt.Item1==EffectManager.Type.RoundRareBonus){
			    rare_bonus+=nt.Item2+1;
			}
			rare_bonus=Math.Min(rare_bonus,12);
		}
		cfg.Probs n_probs=DataLoader.get_probs_data()[rare_bonus];
		float[] rare_probs=new float[4]{n_probs.Lv0,n_probs.Lv1,n_probs.Lv2,n_probs.Lv3};
		for(int i=1;i<=m;i++){
			int card_level=0;
			float nf=GD.Randf(),ng=0;
			for(int j=0;j<4;j++){
				ng+=rare_probs[j];
				if(nf<=ng){
					card_level=j;
					break;
				}
			}
			// int score=(int)Math.Pow(2,card_level);
			nf=GD.Randf();
			if(nf<0.1){
				CardGenerator.get_generator().generate_card("RareBonus",card_level);
			}else if(nf<0.2){
				CardGenerator.get_generator().generate_card("Multiply",card_level);
			}else if(nf<0.7){
				CardGenerator.get_generator().generate_card("Default",card_level);
			}else if(nf<0.8){
				CardGenerator.get_generator().generate_card("LastBonus",card_level);
			}else if(nf<0.9){
				CardGenerator.get_generator().generate_card("ThreeRow",card_level);
			}else{
				CardGenerator.get_generator().generate_card("Sum",card_level);
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
