using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// User interface main menu.
/// </summary>
public class UIMainMenu : MonoBehaviour {

	//该脚本单例化
	private UIMainMenu instance;

	//设置单例脚本个getter属性
	public UIMainMenu Instance {
		get {
			return instance;
		}
	}

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake () {
		//单例初始化
		instance = this;
	}

	//主界面
	public GameObject BgImage;

	//帮助界面
	public GameObject HelpBgImage;

	//游戏加载界面
	public GameObject GameLoadBgImage;

	//操作说明图
	public Image operationImage;

	//操作说明精灵图集
	public Sprite[] ImageSprites;

	//游戏加载界面的进度条
	public Slider loadSlider;

	//游戏进度文字显示
	public Text loadProgreeText;

	//图片索引
	[HideInInspector]
	public int imageIndex = 0;

	/// <summary>
	/// Raises the play game button event.
	/// </summary>
	public void OnPlayGameButton(int sceneIndex) {
		//调用Gamemanager逻辑加载场景
		MainMenuGameManager.Instance.LoadGame (sceneIndex);
	}

	/// <summary>
	/// Raises the help button event.
	/// </summary>
	public void OnHelpButton() {
		//调用Gamemanager逻辑显示操作说明界面
		MainMenuGameManager.Instance.ShowHelpPanelAndHideMainMenu ();
	}

	/// <summary>
	/// Raises the previous button event.
	/// </summary>
	public void OnPreviousButton() {
		//调用Gamemanager逻辑显示前一张操作说明
		MainMenuGameManager.Instance.PreviousImage ();
	}

	/// <summary>
	/// Raises the next button event.
	/// </summary>
	public void OnNextButton() {
		//调用Gamemanager逻辑显示后一张操作说明
		MainMenuGameManager.Instance.NextImage ();
	}

	/// <summary>
	/// Raises the close button event.
	/// </summary>
	public void OnCloseButton() {
		//调用Gamemanager逻辑显示后一张操作说明
		MainMenuGameManager.Instance.HideHelpPanelAndShowMainMenu ();
	}
}
