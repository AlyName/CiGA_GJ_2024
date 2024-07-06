using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;

public partial class EffectManager{
    public enum Type{
        None,
        RoundThreeRow,
        RoundRareBonus,
        LineColor,
        Mysterious,
        Multiply,
        Duplicate,
        RoundCardBonus,
        Sound,
        TimeCardBonus,
        LastCardBonus,
	}
    public List<bool> b_instant_effect=new List<bool>();
    public List<Texture> effect_imgs=new List<Texture>();
    public List<Tuple<Type,int>> effect_queue=new List<Tuple<Type, int>>();
    string last_card_type_s;
    int last_card_level,last_row,max_last_row;
    // public int next_round_bonus_card=0;
    // public int next_card_bonus=1,next_card_duplicate;
    // public int[] next_round_bonus_rare=new int[4];

    // public void reset_card_effect(){
    //     // next_card_bonus=1;
    // }
    public EffectManager(){
        List<cfg.Item> card_data=DataLoader.get_card_data();
        for(int j=0;j<Enum.GetNames(typeof(Type)).Length;j++){
            bool nfl=false;
            for(int i=0;i<card_data.Count;i++){
                if(card_data[i].Effect1==Enum.GetNames(typeof(Type))[j]){
                    nfl=true;
                    b_instant_effect.Add(card_data[i].IsInstantEffect);
                    Debug.WriteLine(card_data[i].IsInstantEffect);
                    if(!card_data[i].IsInstantEffect&&card_data[i].EffectImg!=""){
                        effect_imgs.Add(GD.Load<Texture>("res://imgs/buff/"+card_data[i].EffectImg));
                    }else{
                        effect_imgs.Add(null);
                    }
                }
            }
            if(!nfl){
                b_instant_effect.Add(true);
                effect_imgs.Add(null);
            }
        }
        
    }
    public void reset_round_effect(){
        effect_queue.Clear();
        last_card_type_s="";
        last_card_level=0;
        last_row=1;
        max_last_row=1;
    }

    public void apply_effect(CardEvent n_card_event,List<Card> cards){
        List<int> remove_index=new List<int>();
        for(int i=0;i<effect_queue.Count;i++){
            if(can_apply(n_card_event,cards,effect_queue[i])){
                apply_single_effect(n_card_event,effect_queue[i]);
                if(should_delete(effect_queue[i])){
                    remove_index.Add(i);
                }
            }
        }
        for(int i=0;i<remove_index.Count;i++){
            effect_queue.RemoveAt(remove_index[i]-i);
        }
        if(n_card_event.card_type_s==last_card_type_s){
            last_row++;
            max_last_row=Math.Max(max_last_row,last_row);
        }else{
            last_row=1;
            last_card_type_s=n_card_event.card_type_s;
            last_card_level=n_card_event.card_level;
            max_last_row=Math.Max(max_last_row,last_row);
        }
    }

    public void apply_instant_effect(){
        List<int> remove_index=new List<int>();
        for(int i=0;i<effect_queue.Count;i++){
            if(is_instant_effect(effect_queue[i])){
                apply_single_instant_effect(effect_queue[i]);
                if(should_delete(effect_queue[i])){
                    remove_index.Add(i);
                }
            }
        }
        for(int i=0;i<remove_index.Count;i++){
            effect_queue.RemoveAt(remove_index[i]-i);
        }
    }

    public bool can_apply(CardEvent n_card_event,List<Card> cards,Tuple<Type,int> n_effect){
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
        }else if(n_effect.Item1==Type.LastCardBonus){
            if(n_card_event.event_type==CardEvent.Type.AddScore&&cards.Count==0){
                return true;
            }else{
                return false;
            }
        }else if(n_effect.Item1==Type.TimeCardBonus){
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
        }else if(n_effect.Item1==Type.LastCardBonus){
            int[] n4=new int[4]{10,15,25,40};
            n_card_event.score+=n4[n_effect.Item2];
        }else if(n_effect.Item1==Type.TimeCardBonus){
            int[] n4=new int[4]{3,4,5,6};
            n_card_event.score+=n4[n_effect.Item2];
        }
    }

    public int apply_round_effect(int round_score){
        for(int i=0;i<effect_queue.Count;i++){
            if(effect_queue[i].Item1==Type.RoundThreeRow){
                if(max_last_row>=3){
                    round_score*=effect_queue[i].Item2+2;
                }
            }
        }
        return round_score;
    }

    bool is_instant_effect(Tuple<Type,int> n_effect){
        return b_instant_effect[(int)n_effect.Item1];
    }

    void apply_single_instant_effect(Tuple<Type,int> n_effect){
        Debug.WriteLine((int)n_effect.Item1);
    }

    bool should_delete(Tuple<Type,int> n_effect){
        if(n_effect.Item1==Type.TimeCardBonus){
            return false;
        }
        return true;
    }

    public void remove_a_time_effect(){
        List<int> remove_index=new List<int>();
        for(int i=0;i<effect_queue.Count;i++){
            if(effect_queue[i].Item1==Type.TimeCardBonus){
                remove_index.Add(i);
                break;
            }
        }
        for(int i=0;i<remove_index.Count;i++){
            effect_queue.RemoveAt(remove_index[i]-i);
        }
    }
}