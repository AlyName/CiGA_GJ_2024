using Godot;
using System;
using System.Diagnostics;

public partial class Card : CardMove{
    public int score;

    public string card_description;

    protected bool is_effect_0_add_score=true;
    protected bool is_use_in_level=true;
    protected CardEvent event_0=null,event_1=null;
    protected override void use_card(){
        CardEvent now_event=new CardEvent();
        if(is_use_in_level){
            if(is_use==0){
                if(is_effect_0_add_score){
                    now_event.event_type=CardEvent.Type.AddScore;
                    now_event.score=score;
                }else{
                    now_event.event_type=CardEvent.Type.Effect;
                    now_event.action=new Action<LevelManager>(effect_0);
                }
            }else{
                now_event.event_type=CardEvent.Type.Effect;
                now_event.action=new Action<LevelManager>(effect_1);
            }
            
        }else{
            if(is_use==0){
                now_event=event_0;
            }else{
                now_event=event_1;
            }
        }
        MonoControl.get_control().add_card_event(now_event);
    }
    protected override void check_sprite(){
        base.check_sprite();
        card_label.Text=score.ToString();
        if(can_drag==1){
			Main.get_main().GetNode<Label>("Description").Text=card_description;
		}
    }
    virtual protected void effect_0(LevelManager n_levelmanager){

    }
    virtual protected void effect_1(LevelManager n_levelmanager){

    }
}