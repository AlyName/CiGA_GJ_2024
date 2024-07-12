using Godot;
using System;

public partial class CardEvent{

	public enum Type{
		GameStart,
		GameEnd,
		GetIntoNextLevel,
		GameOver,
		Settings,
		Continue,
		NextLevel,
        AddScore,
		Effect,
		SetFullScreen,
		SetWindow,
		SetMusic,
		SetSilence,
		SetHelp
	}
	public Type event_type;
	public bool is_instant_effect=false;
    public int score,card_level,card_type;
	public string card_type_s;
	public Action<LevelManager,int> action;

	public Vector2 pos;

	public CardEvent(){

	}
	public CardEvent(Type type){
		event_type = type;
	}
}
