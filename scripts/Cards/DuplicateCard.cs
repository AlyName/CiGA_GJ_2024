using Godot;
using System;
using System.Diagnostics;

public partial class DuplicateCard : Card{

	public DuplicateCard(){
		card_type_s="Duplicate";
		card_description="Dup : 获得下一张牌的复制...";
		card_type=8;
		// score=n_score;
	}
	protected override void effect_1(LevelManager n_levelmanager,int n_card_level){
		n_levelmanager.effect_manager.effect_queue.Add(
				new Tuple<EffectManager.Type,int>(
					EffectManager.Type.Duplicate,
					n_card_level
				)
		);
	}
}
