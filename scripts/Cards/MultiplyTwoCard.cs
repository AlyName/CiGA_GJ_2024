using Godot;
using System;
using System.Diagnostics;

public partial class MultiplyTwoCard : Card{

	public MultiplyTwoCard(){
		card_description="x2 - 上划加分，下划当前轮分数x2，之前总分/2";
	    // score=n_score;
	}
	protected override void effect_1(LevelManager n_levelmanager){
		n_levelmanager.now_score/=2;
		n_levelmanager.round_score*=2;
	}
}
