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
		Effect
	}
	public Type event_type;
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