using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;

public partial class CardGenerator : Node{
    // [Export]
    // public Godot.Collections.Dictionary<string,PackedScene> card_scenes;

    public int overall_id;

    public static CardGenerator instance;

    public CardGenerator(){
        instance=this;
    }
    public int generate_card(string type,int card_level=0,int card_score=-1){
        var card_data=DataLoader.get_card_data();
        cfg.Item n_card_item=null;
        int ni=0;
        for(ni=0;ni<card_data.Count;ni++){
            // Debug.WriteLine(card_data[i].CardTypeS);
            if(card_data[ni].CardTypeS==type){
                n_card_item=card_data[ni];
            }
        }
        // PackedScene n_scene=card_scenes.TryGetValue(type,out var scene) ? scene : null;
        if(n_card_item==null){
            Debug.WriteLine("card not found");
            return -1;
        }
        Card n_base_card=generate_base_card(n_card_item);
        
        var card=n_base_card;
        card.card_level=card_level;
        card.Texture=GD.Load<Texture2D>("res://imgs/borders/"+card_level+".PNG");

        
        card.score=n_base_card.score*(int)Math.Pow(2,card_level);
        if(card_score!=-1){
            card.score=card_score;
        }

        if(card.is_use_in_level){
            if(card_level==0)card.card_description=n_card_item.Description0;
            else if(card_level==1)card.card_description=n_card_item.Description1;
            else if(card_level==2)card.card_description=n_card_item.Description2;
            else if(card_level==3)card.card_description=n_card_item.Description3;
            card.card_description_2=Main.get_main().default_desc_2.Replace("%%",card.score.ToString());
        }

        card.Position = new Vector2(0,1000);
		card.card_id=++overall_id;
        Main.get_main().AddChild(card);
        // Debug.WriteLine("Duplicate");
        return CardWheel.get_wheel().add_card(card.card_id,0);
    }

    public int generate_random_card(int card_level=0,int card_score=-1){
        List<string> all_types=new List<string>();
        var card_data=DataLoader.get_card_data();
        for(int i=0;i<card_data.Count;i++){
            if(card_data[i].IsUseInLevel==true){
                all_types.Add(card_data[i].CardTypeS);
            }
        }
        float nf=GD.Randf(),ng=1.0f/all_types.Count;
        for(int i=0;i<all_types.Count;i++){
            if(nf<ng*(i+1)){
                return generate_card(all_types[i],card_level,card_score);
            }
        }
        return -1;
    }

    Card generate_base_card(cfg.Item n_card_item){
        Card card=new Card(n_card_item);
        
        return card;
    }

    public static CardGenerator get_generator(){
        return instance;
    }
}