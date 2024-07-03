using Godot;
using System;
using System.Diagnostics;

public partial class DefaultCard : Card{

	public DefaultCard(){
		card_type_s="Default";
		card_description="Default - 上划加分，下划啥也没有";
        card_type=0;
		// score=n_score;
	}
}
