using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;

public partial class CardGenerator : Node{
    [Export]
    public Godot.Collections.Dictionary<string,PackedScene> card_scenes;

    public int overall_id;

    public static CardGenerator instance;

    public CardGenerator(){
        instance=this;
    }
    public int generate_card(string type,int card_level=0,int score=0){
        PackedScene n_scene=card_scenes.TryGetValue(type,out var scene) ? scene : null;
        if(n_scene==null){
            Debug.WriteLine("card not found");
            return -1;
        }
        
        var card=n_scene.Instantiate() as Card;
        card.card_level=card_level;
        card.score=score;
        card.Position = new Vector2(0,1000);
		card.card_id=++overall_id;
        Main.get_main().AddChild(card);
        // Debug.WriteLine("Duplicate");
        return CardWheel.get_wheel().add_card(card.card_id,0);
    }

    public static CardGenerator get_generator(){
        return instance;
    }
}