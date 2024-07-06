using Godot;
using System;
using System.Runtime.InteropServices;

public partial class FloatTween : Label
{
	[Export] public Vector2 oriPos;
	[Export] public Vector2 tarPos;
	[Export] public float tarScale;
	[Export] public float tweenTime;

	private Tween mTween;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayFloatTween(new Vector2(500, 500), new Vector2(500, 300), 1.3f, 1f);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void PlayFloatTween(Vector2 oriPos, Vector2 tarPos, float tarScale, float tweenTime)
	{
		this.oriPos = oriPos;
		this.tarPos = tarPos;
		this.tarScale = tarScale;
		this.tweenTime = tweenTime;
		
		ExecuteFloatTween();
	}

	private void ExecuteFloatTween()
	{
		mTween?.Kill();
		
		Scale = Vector2.One;
		Position = oriPos;

		mTween = GetTree().CreateTween().SetParallel();
		mTween.TweenProperty(this, "position", tarPos, tweenTime)
			.SetTrans(Tween.TransitionType.Quint)
			.SetEase(Tween.EaseType.Out);
		mTween.TweenProperty(this, "modulate", new Color(Colors.White, 0), tweenTime);
		mTween.TweenProperty(this, "scale", Vector2.One * tarScale, tweenTime);
	}
}
