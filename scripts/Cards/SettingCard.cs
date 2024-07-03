using Godot;
using System;
using System.Diagnostics;

public partial class SettingCard : Card{

	public SettingCard(){
		card_type_s="Setting";
		card_description="Setting - 上划设置，下划帮助，但没有帮助，所以是退出";
		is_use_in_level=false;
		event_0=new CardEvent();
		event_0.event_type=CardEvent.Type.Settings;
		event_1=new CardEvent();
		event_1.event_type=CardEvent.Type.GameEnd;
		
	}
}
