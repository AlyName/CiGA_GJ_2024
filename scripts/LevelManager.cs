using Godot;
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

public partial class LevelManager{
    public string current_level_name;
    public int level_id,level_end_flag=0;
    public int goal_score,now_score,max_round,now_round,round_score=0;
    public CardGameFlow flow=null;
    public EffectManager effect_manager=null;
    public LevelManager(int now_level_id){
        level_id=now_level_id;
        //test level init
        goal_score=100+(level_id-1)*200;
        now_score=0;
        max_round=4+level_id;
        now_round=0;
        round_score=0;
        //end
        flow=new CardGameFlow();
        effect_manager=new EffectManager();
        round_start();
    }

    public int level_end(){
        return level_end_flag;
    }

    public bool handle_event(CardEvent now_event){
        if(now_event.event_type!=CardEvent.Type.AddScore&&now_event.event_type!=CardEvent.Type.Effect){
            return false;
        }
        effect_manager.apply_effect(now_event,CardWheel.get_wheel().cards);
        if(now_event.event_type==CardEvent.Type.AddScore){
            Particles.get_particles().card_explode(now_event.pos,now_event.score);
            round_score+=now_event.score;
        }else if(now_event.event_type==CardEvent.Type.Effect){
            Particles.get_particles().card_explode(now_event.pos,0);
            // Debug.Print(now_event.card_level.ToString());
            Action<LevelManager,int> n_action=now_event.action;
            FlowNode nnode=new FlowNode("effect test",n_action);
            flow.PushNode(nnode);
            flow.NextNode(this,now_event.card_level);
        }
        // Debug.WriteLine("check");
        check_round_end();
        return true;
    }

    async Task check_round_end(){
        
        if(MonoControl.get_control().left_card()==0){
            await Main.get_main().block_wait_time(1f);
            UI.get_ui().label_b_boom();
            round_score=effect_manager.apply_round_effect(round_score);//TODO
            await Main.get_main().block_wait_time(1f);
            UI.get_ui().label_a_boom();
            now_score+=round_score;
            await Main.get_main().block_wait_time(0.5f);
            check_level_end_flag();
            if(level_end_flag==0)round_start();
        }
    }

    void check_level_end_flag(){
        if(now_score>=goal_score){
            level_end_flag=1;
        }else if(now_round>=max_round){
            level_end_flag=-1;
        }else{
            level_end_flag=0;
        }
    }

    void round_start(){
        now_round++;
        MonoControl.get_control().generate_cards(effect_manager);
        round_score=0;
        effect_manager.reset_round_effect();
    }

    public int left_card(){
        return MonoControl.get_control().left_card();
    }
}