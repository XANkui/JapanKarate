using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Fade jump to scene.渐变的方式 转场Scene
/// </summary>
public class FadeJumpToScene : MonoBehaviour {

	//渐变的原始图，黑化的话，黑色图片即可，根据需要其他图也行
	public Texture FadeTexture;

	//设置专场速度
	public float FadeSpeed = 1.5f;

	//跳转的场景编号
	private int jumpToSceneIndex;

	//转场所需要的一些参数
	private bool isStartJumpingToScene = false;
	private bool isEndJumpingToScene = false;

	//通过Alpha值的变化实现渐变
	private float onGUIColorAlpha;

	// Use this for initialization
	void Start () {

		//获取OnGUI渲染上的Alpha值，并设置初始值为0
		onGUIColorAlpha = GUI.color.a;
		onGUIColorAlpha = 0.0f;

		//设置过场不销毁该挂载的物体
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {

		//监控开始渐黑转场为真，开始转场
		if(isStartJumpingToScene == true){

			//调用开始转场
			StartJumpingToScene ();
		}

		//监控开始渐白转场为真，开始转场
		if(isEndJumpingToScene == true){

			//调用开始转场
			EndJumpingToScene ();
		}
			
	}

	/// <summary>
	/// Jumps to scene.开始转场
	/// </summary>
	private void JumpToScene(int sceneIndex) {
		//黑化转场开始
		isStartJumpingToScene = true;

		//白化转场不开始 
		isEndJumpingToScene = false;

		//跳转场景
		jumpToSceneIndex = sceneIndex;

	}

	/// <summary>
	/// Fades to black.退场渐渐黑屏效果
	/// </summary>
	private void FadeToBlack() {

		//使用差值的方法改变Alpha值
		onGUIColorAlpha = Mathf.Lerp (onGUIColorAlpha, 1.0f, FadeSpeed * Time .deltaTime);
	}

	/// <summary>
	/// Fades to white.进场渐渐白屏效果
	/// </summary>
	private void FadeToWhite() {

		//使用差值的方法改变Alpha值
		onGUIColorAlpha = Mathf.Lerp (onGUIColorAlpha, 0.0f, FadeSpeed * Time .deltaTime);
	}

	/// <summary>
	/// Starts the jump to scene.开始渐黑跳转场景
	/// </summary>
	private void StartJumpingToScene() {
		//改变Alpha屏幕渐黑
		FadeToBlack ();

		//当Alpha接近1。0f时，进行设置
		if(onGUIColorAlpha >= 0.95f){
			
			//设置Alpha值为1
			onGUIColorAlpha = 1.0f;

			//黑化转场结束
			isStartJumpingToScene = false;

			//白化转场开始
			isEndJumpingToScene = true;

			//并跳转场景
			SceneManager.LoadScene (jumpToSceneIndex);
		}
	}

	/// <summary>
	/// Ends the jumping to scene.开始白化显示场景
	/// </summary>
	private void EndJumpingToScene() {

		//改变Alpha值屏幕渐渐变白
		FadeToWhite ();

		if(onGUIColorAlpha <= 0.05f) {
			
			//设置Alpha值为0
			onGUIColorAlpha = 0.0f;

			//白化转场开始
			isEndJumpingToScene = false;

			//转场结束，销毁自身
			Destroy (this.gameObject);
		}
	}

	/// <summary>
	/// Raises the GU event.
	/// </summary>
	void OnGUI(){
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b,
			Mathf.Clamp01 (onGUIColorAlpha));

		GUI.DrawTexture (new Rect(0, 0, Screen.width, Screen.height), FadeTexture);
	}
}
