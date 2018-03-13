using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

/// <summary>
/// Play video on UI.在Canvas的RawImage上播放视频的方法
/// </summary>
[RequireComponent(typeof(VideoPlayer))]
public class PlayVideoOnUI : MonoBehaviour {

	//获取播放UI载体
	private RawImage rawImage;

	//获取视频播放器
	private VideoPlayer videoPlayer;

	//提示文字，再点击屏幕跳过动画
	private bool isShowTip = false;

	//监控播放1秒后看触屏跳过过场动画
	private bool isJump = false;
	public GameObject tipText;

	// Use this for initialization
	void Start () {
		//获取Image组件
		rawImage = GameObject.Find ("RawImage").GetComponent <RawImage> ();

		//获取VideoPlayer组件
		videoPlayer = this.GetComponent <VideoPlayer> ();

		//设置视频播放器的渲染模式
		videoPlayer.renderMode = VideoRenderMode.APIOnly;

		//监控播放1秒后看触屏跳过过场动画
		Invoke ("IsJumpTrue", 1);

	}
	
	// Update is called once per frame
	void Update () {

		print(videoPlayer.frameCount +":" +videoPlayer.frame);

		//播放视频
		PlayVideo ();

		//监控播放1秒后看触屏跳过过场动画
		if(isJump == true) {
			//视频播放结束监控的调用
			PlayVideoEnd();
		}

		//按下任意键跳转
		if(Input.anyKeyDown){

			//显示文字后再点击才能跳过
			if(isShowTip == true) {
				//发送消息
				SendMessage ("JumpToScene", 1);

			}

			//设置显示文字
			isShowTip = true;
			ShowTipText ();
		}
	}

	/// <summary>
	/// Plaies the video.
	/// </summary>
	private void PlayVideo(){
		//没有视频就退出
		if (videoPlayer.texture == null) 
			return;

		//在URawImage的UI渲染播放视频
		rawImage.texture = videoPlayer.texture;
	}

	/// <summary>
	/// Plaies the video end.视频播放结束，开始跳转场景
	/// </summary>
	private void PlayVideoEnd() {

		long titleFrame = (long)(videoPlayer.frameCount - 10);

		//当视频播放帧数大于（总视频帧数-10），开始发消息跳转场景
		if(videoPlayer.frame >= titleFrame){

			//发送消息
			SendMessage ("JumpToScene", 1);

			#if UNITY_EDITOR
			print("SendMessage" +videoPlayer.frameCount +":" +videoPlayer.frame);
			#endif
		}
	}

	/// <summary>
	/// Determines whether this instance is jump true.
	/// </summary>
	/// <returns><c>true</c> if this instance is jump true; otherwise, <c>false</c>.</returns>
	private void IsJumpTrue(){
		//监控播放1秒后看触屏跳过过场动画
		isJump = true;
	}

	/// <summary>
	/// Shows the tip text.
	/// </summary>
	private void ShowTipText() {
		//显示提示文字
		tipText.SetActive (true);
	}
}
