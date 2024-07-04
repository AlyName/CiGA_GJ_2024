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
        for(int i=0;i<card_data.Count;i++){
            // Debug.WriteLine(card_data[i].CardTypeS);
            if(card_data[i].CardTypeS==type){
                n_card_item=card_data[i];
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
        card.score=n_base_card.score*(int)Math.Pow(2,card_level);
        if(card_score!=-1){
            card.score=card_score;
        }
        card.Position = new Vector2(0,1000);
		card.card_id=++overall_id;
        Main.get_main().AddChild(card);
        // Debug.WriteLine("Duplicate");
        return CardWheel.get_wheel().add_card(card.card_id,0);
    }

    Card generate_base_card(cfg.Item n_card_item){
        Card card=new Card(n_card_item);
        
        return card;
    }

    public static CardGenerator get_generator(){
        return instance;
    }
}