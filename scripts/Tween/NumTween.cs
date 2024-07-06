using Godot;
using System;

public partial class NumTween : RichTextLabel
{
	[Export] public int numStart = 0;
	[Export] public int numEnd = 999;
	[Export] public float tweenTime = 1f;
	[Export] public bool shake = false;
	[Export] public float targetScale = 2f;
	private Tween mTween;

	private float mSize2TarTime = .3f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		FitContent = false;
		
		//下面是示例
		
		//NumTweenScroll(0,9999, 3f).SetShake();
		
		//NumTweenSize(0, 9999, 1.5f, .5f).SetShake().SetFire();
	}

	/// <summary>
	/// 数字滚动
	/// </summary>
	/// <param name="numStart">开始数字</param>
	/// <param name="numEnd">结束数字</param>
	/// <param name="tweenTime">动画时间</param>
	/// <returns></returns>
	public NumTween NumTweenScroll(int numStart, int numEnd, float tweenTime)
	{
		this.numStart = numStart;
		this.numEnd = numEnd;
		this.tweenTime = tweenTime;
		
		shake = false;
		
		ExecuteScrollTween();
		
		//tween.Connect("tween_completed", new Callable(this, nameof(OnTweenCompleted)));

		return this;
	}

	/// <summary>
	/// 数字突然变大变小
	/// </summary>
	/// <param name="numStart">开始数字</param>
	/// <param name="numEnd">结束数字</param>
	/// <param name="targetScale">目标Scale</param>
	/// <param name="tweenTime">动画时间</param>
	/// <param name="size2TarTime">变到目标Scale占动画时间的比例， 1-size2TarTime就是恢复原始大小的时间</param>
	/// <returns></returns>
	public NumTween NumTweenSize(int numStart, int numEnd, float targetScale, float tweenTime, float size2TarTime = .3f)
	{
		this.numStart = numStart;
		this.numEnd = numEnd;
		this.tweenTime = tweenTime;
		this.targetScale = targetScale;

		mSize2TarTime = size2TarTime;
		
		shake = false;
		
		ExecuteScaleTween();

		return this;
	}
	
	/// <summary>
	/// 设置抖动
	/// </summary>
	public void SetShake()
	{
		shake = true;
	}
	
	/// <summary>
	/// 设置火焰效果
	/// </summary>
	public void SetFire()
	{
		
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (numStart < numEnd)
			Text = numStart.ToString();
		else if(shake)
			Text = $"[shake rate = 30 level = 10]{numStart}";
	}
	
	private void OnTweenCompleted(object obj, NodePath key)
	{
		
	}


	private void ExecuteScrollTween()
	{
		mTween?.Kill();
		
		mTween = GetTree().CreateTween();
		mTween.TweenProperty(this, "numStart", numEnd, tweenTime)
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out);
	}

	private void ExecuteScaleTween()
	{
		mTween?.Kill();
		
		Scale = Vector2.One;
		
		mTween = GetTree().CreateTween();
		mTween.TweenProperty(this, "scale", targetScale * Vector2.One, tweenTime * mSize2TarTime)
			.SetTrans(Tween.TransitionType.Sine)
			.SetEase(Tween.EaseType.Out);

		mTween.TweenProperty(this, "numStart", numEnd, tweenTime * 0f);

		mTween.TweenProperty(this, "scale", Vector2.One, tweenTime * (1 - mSize2TarTime));
	}
}
