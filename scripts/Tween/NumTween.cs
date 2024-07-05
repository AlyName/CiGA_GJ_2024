using Godot;
using System;

public partial class NumTween : Label
{
	[Export] public int num = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var tween = GetTree().CreateTween();
		tween.TweenProperty(this, "num", 99999, 2f)
			.SetTrans(Tween.TransitionType.Quint)
			.SetEase(Tween.EaseType.Out);

		tween.Connect("tween_completed", new Callable(this, nameof(OnTweenCompleted)));
		
		tween.SetLoops();
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
