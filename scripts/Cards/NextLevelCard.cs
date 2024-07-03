using Godot;
using System;
using System.Diagnostics;

public partial class NextLevelCard : Card{

	public NextLevelCard(){
		card_type_s="NextLevel";
		card_description="Next Level - 上划下一关，下划退出";
		is_use_in_level=false;
		event_0=new CardEvent();
		event_0.event_type=CardEvent.Type.NextLevel;
		event_1=new CardEvent();
		event_1.event_type=CardEvent.Type.GameEnd;
		
	}
}
