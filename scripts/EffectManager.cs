using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;

public partial class EffectManager{
    public enum Type{
        RoundCardBonus,
		Multiply,
        RoundRareBonus,
        Duplicate
	}
    public List<Tuple<Type,int>> effect_queue=new List<Tuple<Type, int>>();
    // public int next_round_bonus_card=0;
    // public int next_card_bonus=1,next_card_duplicate;
    // public int[] next_round_bonus_rare=new int[4];

    // public void reset_card_effect(){
    //     // next_card_bonus=1;
    // }
    public void reset_round_effect(){
        effect_queue.Clear();
        // next_round_bonus_card=0;
        // for(int i=0;i<4;i++){
        //     next_round_bonus_rare[i]=0;
        // }
    }

    public void apply_effect(CardEvent n_card_event){
        List<int> remove_index=new List<int>();
        for(int i=0;i<effect_queue.Count;i++){
            if(can_apply(n_card_event,effect_queue[i])){
                apply_single_effect(n_card_event,effect_queue[i]);
                remove_index.Add(i);
            }
        }
        for(int i=0;i<remove_index.Count;i++){
            effect_queue.RemoveAt(remove_index[i]-i);
        }
        // if(n_card_event.event_type==CardEvent.Type.AddScore){
        //     n_card_event.score*=next_card_bonus;
        //     next_card_bonus=1;
        // }

    }

    public bool can_apply(CardEvent n_card_event,Tuple<Type,int> n_effect){
        if(n_effect.Item1==Type.RoundCardBonus){
            return false;
        }else if(n_effect.Item1==Type.Multiply){
            if(n_card_event.event_type==CardEvent.Type.AddScore){
                return true;
            }else{
                return false;
            }
        }else if(n_effect.Item1==Type.RoundRareBonus){
            return false;
        }else if(n_effect.Item1==Type.Duplicate){
            return true;
        }
        return false;
    }

    public void apply_single_effect(CardEvent n_card_event,Tuple<Type,int> n_effect){
        if(n_effect.Item1==Type.Multiply){
            n_card_event.score*=n_effect.Item2+2;
        }else if(n_effect.Item1==Type.Duplicate){
            int nt=Math.Max(1,n_effect.Item2);
            for(int i=0;i<nt;i++){
                int np=CardGenerator.get_generator().generate_card(n_card_event.card_type_s,n_card_event.card_level,n_card_event.score);
                CardWheel.get_wheel().rotate_to(np);
            }
        }
    }

    public void apply_round_effect(){
        
    }
}