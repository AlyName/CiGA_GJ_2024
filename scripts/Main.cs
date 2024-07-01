using Godot;
using System;

public partial class Main : Node2D{
	[Export]
	public PackedScene Card;

	[Export]
	public PackedScene PackedStar;


	public static Main instance;

	public int overall_id;

	public Main(){
		instance=this;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		// for(int i=1;i<=10;i++){

		// 	var card = Card.Instantiate() as CardMove;
		// 	card.Position = new Vector2(i*100,0);
		// 	card.card_id=i;
		// 	AddChild(card);
		// 	GetNode<CardWheel>("CardWheel").add_card(i,0);
		// }
		generate_card(-1);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
		
	}

	public void generate_card(int card_score){
		var card = Card.Instantiate() as CardMove;
		card.Position = new Vector2(0,0);
		card.card_id=++overall_id;
		card.score=card_score;
		AddChild(card);
		GetNode<CardWheel>("CardWheel").add_card(card.card_id,0);
	}

	public static Main get_main(){
		return instance;
	}

}
