using Godot;
using System;

public partial class CardEvent{

	public enum Type{
		GameStart,
		GameEnd,
        AddScore,
		Effect
	}
	public Type event_type;
    public int score;
	public Action<LevelManager> action;
}
