using Godot;
using System;

public enum AudioEnum
{
	/// <summary> 上划 </summary>
	PlayUp,
	/// <summary> 下划 </summary>
	PlayDown,
	/// <summary> 主菜单 </summary>
	MainMenu,
	/// <summary> 游戏中 </summary>
	Game,
	/// <summary> 倒计时 </summary>
	CountDown,
	/// <summary> 设置 </summary>
	Setting,
	/// <summary> 笑 </summary>
	LaughX1,
	/// <summary> 欢呼 </summary>
	LaughX2,
	/// <summary> 大声欢呼 </summary>
	LaughX3,
	/// <summary> 满堂喝彩 </summary>
	LaughX4,
	/// <summary> 蜂鸣器 </summary>
	Bee,
}


public partial class AudioManager : Node
{
	private static AudioManager sAudioManager;

	public static AudioManager Instance => sAudioManager;

	public override void _Ready()
	{
		sAudioManager = this;
	}

	public void PlaySound(AudioEnum audio)
	{
		
	}
}
