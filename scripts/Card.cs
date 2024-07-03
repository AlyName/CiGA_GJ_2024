using Godot;
using System;
using System.Diagnostics;

public partial class Card : CardMove{
    public int card_level,score,card_type;

    public string card_type_s,card_description;

    public Color n_color;

    protected bool is_effect_0_add_score=true;
    protected bool is_use_in_level=true;
    protected CardEvent event_0=null,event_1=null;
    protected override void use_card(){
        CardEvent now_event=new CardEvent();
        if(is_use_in_level){
            now_event.card_level=card_level;
            now_event.card_type=card_type;
            now_event.card_type_s=card_type_s;
            if(is_use==0){
                if(is_effect_0_add_score){
                    now_event.event_type=CardEvent.Type.AddScore;
                    now_event.score=score;
                }else{
                    now_event.event_type=CardEvent.Type.Effect;
                    now_event.action=new Action<LevelManager,int>(effect_0);
                }
            }else{
                now_event.event_type=CardEvent.Type.Effect;
                now_event.action=new Action<LevelManager,int>(effect_1);
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
        if(is_use_in_level)card_label.Text="+"+score.ToString();
        else card_label.Text="";
        //TODO
        if(card_level==1)n_color=new Color(0,0.5f,1,1);
        else if(card_level==2)n_color=new Color(1,0,1,1);
        else if(card_level==3)n_color=new Color(1,0.5f,0,1);
        else n_color=new Color(1,1,1,1);
        if(state!=0){
            Modulate=n_color;
        }
        if(can_drag==1){
            // TODO
			UI.get_ui().set_description(card_description);
		}
    }
    virtual protected void effect_0(LevelManager n_levelmanager,int card_level){

    }
    virtual protected void effect_1(LevelManager n_levelmanager,int card_level){

    }
}