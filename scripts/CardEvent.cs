using Godot;
using System;

public partial class CardEvent{

	public enum Type{
		GameStart,
		GameEnd,
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
}
