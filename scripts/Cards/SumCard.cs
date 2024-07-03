using Godot;
using System;
using System.Diagnostics;

public partial class SumCard : Card{

	public SumCard(){
		card_type_s="Sum";
	    card_description="Sum - 下回合额外抽...";
		card_type=1;
	}
	protected override void effect_1(LevelManager n_levelmanager,int n_card_level){
		n_levelmanager.effect_manager.effect_queue.Add(
				new Tuple<EffectManager.Type,int>(
					EffectManager.Type.RoundCardBonus,
					n_card_level
				)
		);
	}
}
