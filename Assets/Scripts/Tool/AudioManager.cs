using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Audio manager.音乐音效的简单管理器
/// </summary>
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

	//设置单例变量
	private static AudioManager instance;

	//设置单例属性
	public static AudioManager Instance{
		get{ 
			return instance;
		}
	}

	/// <summary>
	/// 将声音放入字典中，方便管理
	/// </summary>
	private Dictionary<string, AudioClip> _soundDictionary;

	//背景音乐和音效的音频源
	private AudioSource [] audioSources;
	private AudioSource bgAudioSource;
	private AudioSource audioSourceEffect;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake () {
		//单例初始化
		instance = this;
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		//加载资源存的所有音频资源
		LoadAudio ();

		//获取音频源
		audioSources = this.GetComponents <AudioSource> ();
		bgAudioSource = audioSources [0];
		audioSourceEffect = audioSources [1];
	}

	/// <summary>
	/// Loads the audio.
	/// </summary>
	private void LoadAudio(){

		//初始化字典
		_soundDictionary = new Dictionary<string, AudioClip> ();

		//本地加载 
		AudioClip [] audioArray = Resources.LoadAll<AudioClip> ("Audio");

		//存放到字典
		foreach (AudioClip item in audioArray) 
		{
			_soundDictionary.Add(item.name,item);
		}
	}

	/// <summary>
	/// Plaies the background audio.播放背景音乐
	/// </summary>
	/// <param name="audioName">Audio name.</param>
	public void PlayBGAudio(string audioName)
	{
		if (_soundDictionary.ContainsKey(audioName)) 
		{
			bgAudioSource.clip=_soundDictionary[audioName];
			bgAudioSource.Play();
		}
	}

	/// <summary>
	/// Plaies the audio effect.播放音效
	/// </summary>
	/// <param name="audioEffectName">Audio effect name.</param>
	public void PlayAudioEffect(string audioEffectName)
	{
		if (_soundDictionary.ContainsKey(audioEffectName)) 
		{
			audioSourceEffect.clip=_soundDictionary[audioEffectName];
			audioSourceEffect.Play();   
		}
	}
}

