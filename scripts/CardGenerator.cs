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
    public bool generate_card(string type,int score=0){
        PackedScene n_scene=card_scenes.TryGetValue(type,out var scene) ? scene : null;
        if(n_scene==null){
            Debug.WriteLine("card not found");
            return false;
        }
        
        var card=n_scene.Instantiate() as Card;
        card.score=score;
        card.Position = new Vector2(0,0);
		card.card_id=++overall_id;
        Main.get_main().AddChild(card);
		Main.get_main().GetNode<CardWheel>("CardWheel").add_card(card.card_id,0);
        return true;
    }

    public static CardGenerator get_generator(){
        return instance;
    }
}