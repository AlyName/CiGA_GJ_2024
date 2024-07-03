using Godot;
using System;
using System.Diagnostics;

public partial class ContinueCard : Card{

	public ContinueCard(){
		card_type_s="Continue";
		card_description="Continue - 上划继续，下划退出";
		is_use_in_level=false;
		event_0=new CardEvent();
		event_0.event_type=CardEvent.Type.Continue;
		event_1=new CardEvent();
		event_1.event_type=CardEvent.Type.GameEnd;
		
	}
}
