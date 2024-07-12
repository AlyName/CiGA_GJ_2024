using Godot;
using System;
using System.Collections.Generic;

public enum AudioEnum
{
	None,
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

	[Export()]public AudioStream playUp;
	[Export()]public AudioStream playDown;
	[Export()]public AudioStream mainMenu;
	[Export()]public AudioStream game;
	[Export()]public AudioStream countDown;
	[Export()]public AudioStream setting;
	[Export()]public AudioStream laughX1;
	[Export()]public AudioStream laughX2;
	[Export()]public AudioStream laughX3;
	[Export()]public AudioStream laughX4;
	[Export()]public AudioStream bee;

	[Export()]public AudioStreamPlayer audioStreamPlayer;

	private AudioEnum mBackAudio;

	private Dictionary<AudioEnum, AudioStream> mAudioDic = new Dictionary<AudioEnum, AudioStream>();

	private List<AudioStreamPlayer> mPlayingStreamPlayers = new List<AudioStreamPlayer>();

	public override void _Ready()
	{
		sAudioManager = this;

		mAudioDic.Add(AudioEnum.PlayUp, playUp);
		mAudioDic.Add(AudioEnum.PlayDown, playDown);
		mAudioDic.Add(AudioEnum.MainMenu, mainMenu);
		mAudioDic.Add(AudioEnum.Game, game);
		mAudioDic.Add(AudioEnum.CountDown, countDown);
		mAudioDic.Add(AudioEnum.Setting, setting);
		mAudioDic.Add(AudioEnum.LaughX1, laughX1);
		mAudioDic.Add(AudioEnum.LaughX2, laughX2);
		mAudioDic.Add(AudioEnum.LaughX3, laughX3);
		mAudioDic.Add(AudioEnum.LaughX4, laughX4);
		mAudioDic.Add(AudioEnum.Bee, bee);

		PlaySound(AudioEnum.PlayUp);
	}

	public void PlaySound(AudioEnum audio)
	{
		if (audio is AudioEnum.MainMenu or AudioEnum.Game)
		{
			audioStreamPlayer.Stream = mAudioDic[audio];
			audioStreamPlayer.Play();
			mBackAudio = audio;
		}
		else
		{
			var player = new AudioStreamPlayer();
			player.Stream = mAudioDic[audio];
			
			mPlayingStreamPlayers.Add(player);
			AddChild(player);
			player.Play();
		}
		
		
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		
		if(mBackAudio is not AudioEnum.None && !audioStreamPlayer.Playing)
			audioStreamPlayer.Play();
		List<AudioStreamPlayer> delete_list=new List<AudioStreamPlayer>();
		foreach (var player in mPlayingStreamPlayers)
		{
			if(player.Playing)
				continue;
			RemoveChild(player);
			player.QueueFree();
			delete_list.Add(player);
		}


		foreach (var player in delete_list){
			mPlayingStreamPlayers.Remove(player);
		}
	}

	public void set_silence(){
		audioStreamPlayer.VolumeDb=-100;
	}

	public void set_music(){
		audioStreamPlayer.VolumeDb=0;
	}
}
