using Godot;
using System;

public partial class NumTween2 : Label
{
	[Export] public int num = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var tween = GetTree().CreateTween();
		var label = (NumTween2)Duplicate();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GD.Print(num.ToString());
		Text = num.ToString();
	}
	
	private void OnTweenCompleted(object obj, NodePath key)
	{
		num = 0;
	}
}
