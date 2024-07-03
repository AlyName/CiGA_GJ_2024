using Godot;
using System;
using System.Diagnostics;

public partial class MultiplyTwoCard : Card{

	public MultiplyTwoCard(){
		card_type_s="Multiply2";
		card_description="x2 : 下一张牌分数乘...";
		card_type=2;
	    // score=n_score;
	}
	protected override void effect_1(LevelManager n_levelmanager,int n_card_level){
		n_levelmanager.effect_manager.effect_queue.Add(
				new Tuple<EffectManager.Type,int>(
					EffectManager.Type.Multiply,
					n_card_level
				)
		);
	}
}
