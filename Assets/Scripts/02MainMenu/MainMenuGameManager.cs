using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main menu game manager.
/// </summary>
public class MainMenuGameManager : MonoBehaviour {

	//单例化脚本
	private static MainMenuGameManager instance;
	public static MainMenuGameManager Instance {
		get {
			return instance;
		}
	}

	//获取主菜单脚本 
	private UIMainMenu uiMainMenu;
	public UIMainMenu UiMainMenu {
		get {
			return uiMainMenu;
		}
	}

	//异步加载操作符
	private AsyncOperation asyncOp;

	//当前价在进度
	private int currentProgress;

	private void Awake() {
		//单例初始化
		instance = this;
	}

	// Use this for initialization
	void Start () {
		//获取UIMainMenu组件
		uiMainMenu = this.GetComponent <UIMainMenu> ();
	}
	
	// Update is called once per frame
	void Update () {

		//异步加载未开始，则返回
		if(asyncOp == null){
			return;
		}

		//设置进度值
		int progressValue = 0;

		//进度值赋值
		if (asyncOp.progress < 0.9f) {
			progressValue = (int)(asyncOp.progress * 100);
		} else {
			progressValue = 100;
		}

		//当前值增加
		if (currentProgress < progressValue) {
			currentProgress++;
		}

		//显示在UI界面的当前进度
		uiMainMenu.loadProgreeText.text = currentProgress + " %";
		uiMainMenu.loadSlider.value = currentProgress / 100f;

		//进度完成
		if (currentProgress == 100) {

			//加载场景
			asyncOp.allowSceneActivation = true;
		}
	}

	/// <summary>
	/// Loads the game.
	/// </summary>
	/// <param name="sceneIndex">Scene index.</param>
	public void LoadGame(int sceneIndex) {
		//隐藏主界面
		uiMainMenu.BgImage.SetActive (false);

		//显示加载界面
		uiMainMenu.GameLoadBgImage.SetActive (true);

		//异步加载场景
		StartCoroutine (LoadAsync (sceneIndex));
	}

	/// <summary>
	/// Loads the async.
	/// </summary>
	/// <returns>The async.</returns>
	IEnumerator LoadAsync(int sceneIndex){
		
		//异步跳转到game场景
		asyncOp = SceneManager.LoadSceneAsync (sceneIndex);

		//当game场景加载到90%时，不让它直接跳转到game场景
		asyncOp.allowSceneActivation = false;

		yield return asyncOp;
	}

	/// <summary>
	/// Shows the help panel and hide main menu.
	/// </summary>
	public void ShowHelpPanelAndHideMainMenu() {
		//隐藏主菜单
		uiMainMenu.BgImage.SetActive (false);
		//显示帮助菜单
		uiMainMenu.HelpBgImage.SetActive (true);
	}

	/// <summary>
	/// Hides the help panel and show main menu.
	/// </summary>
	public void HideHelpPanelAndShowMainMenu() {
		//显示主菜单
		uiMainMenu.BgImage.SetActive (true);
		//隐藏帮助菜单
		uiMainMenu.HelpBgImage.SetActive (false);
	}

	/// <summary>
	/// Previouses the image.
	/// </summary>
	public void PreviousImage(){

		//图片索引减掉一
		uiMainMenu.imageIndex --;

		//避免越界，小于0，则立即跳到最后一张
		if(uiMainMenu.imageIndex < 0){
			//赋值最后一张
			uiMainMenu.imageIndex = uiMainMenu.ImageSprites.Length;
		}

		//把图片显示在图上
		uiMainMenu.operationImage.sprite = uiMainMenu.ImageSprites[uiMainMenu.imageIndex];
	}

	/// <summary>
	/// Nexts the image.
	/// </summary>
	public void NextImage(){

		//图片索引减掉一
		uiMainMenu.imageIndex ++;

		//避免越界，小于0，则立即跳到最后一张
		if(uiMainMenu.imageIndex > uiMainMenu.ImageSprites.Length){
			//赋值最后一张
			uiMainMenu.imageIndex = 0;
		}

		//把图片显示在图上
		uiMainMenu.operationImage.sprite = uiMainMenu.ImageSprites[uiMainMenu.imageIndex];
	}


}
