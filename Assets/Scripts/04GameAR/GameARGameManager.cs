using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class GameARGameManager : MonoBehaviour {

	public string[] arrayAnimations;

	private int animationIndex = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Plaies the previous animation.
	/// </summary>
	public void PlayPreviousAnimation(){

		//如果有播放动画，则停止播放
		if (UnityARHitTestExample.Instance.anim.isPlaying == true) {
			UnityARHitTestExample.Instance.anim.Stop ();
		}

		//动画索引自减
		animationIndex--;

		//防止越界
		if(animationIndex < 0){
			animationIndex = arrayAnimations.Length - 1;
		}

		//播放动画
		UnityARHitTestExample.Instance.anim.Play (arrayAnimations[animationIndex]);
	}

	/// <summary>
	/// Plaies the next animation.
	/// </summary>
	public void PlayNextAnimation(){

		//如果有播放动画，则停止播放
		if (UnityARHitTestExample.Instance.anim.isPlaying == true) {
			UnityARHitTestExample.Instance.anim.Stop ();
		}

		//动画索引自减
		animationIndex++;	

		//防止越界
		if(animationIndex >= arrayAnimations.Length ){
			animationIndex = 0;
		}

		//播放动画
		UnityARHitTestExample.Instance.anim.Play (arrayAnimations[animationIndex]);
	}

	/// <summary>
	/// Replaies the animation.
	/// </summary>
	public void ReplayAnimation(){
		//如果有播放动画，则停止播放
		if (UnityARHitTestExample.Instance.anim.isPlaying == true) {
			UnityARHitTestExample.Instance.anim.Stop ();
		}

		//播放动画
		UnityARHitTestExample.Instance.anim.Play (arrayAnimations[animationIndex]);
	}
}
