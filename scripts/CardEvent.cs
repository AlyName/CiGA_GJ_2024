using Godot;
using System;

public partial class CardEvent{

	public enum Type{
		GameStart,
        AddScore
	}
	public Type event_type;
    public int score;
}
