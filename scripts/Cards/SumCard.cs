using Godot;
using System;
using System.Diagnostics;

public partial class SumCard : Card{

	public SumCard(){
	    card_description="Sum - 上划加分，下划加上剩余牌数x100这么多分";
	}
	protected override void effect_1(LevelManager n_levelmanager){
		n_levelmanager.round_score+=n_levelmanager.left_card()*100;
	}
}
