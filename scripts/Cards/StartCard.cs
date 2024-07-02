using Godot;
using System;
using System.Diagnostics;

public partial class StartCard : Card{

	public StartCard(){
		card_description="Start - 上划开始，下划退出";
		is_use_in_level=false;
		event_0=new CardEvent();
		event_0.event_type=CardEvent.Type.GameStart;
		event_1=new CardEvent();
		event_1.event_type=CardEvent.Type.GameEnd;
		
	}
}
