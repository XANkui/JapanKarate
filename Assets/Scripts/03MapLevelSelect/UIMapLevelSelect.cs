using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Policy;

/// <summary>
/// User interface map level select.
/// </summary>
public class UIMapLevelSelect : MonoBehaviour {

	public Text userNameText;

	public Text studyText;

	public Text coinText;

	public GameObject coinListDataPrefab;

	public Transform coinListDataParent;

	public GameObject studyListDataPrefab;

	public Transform studyListDataParent;

	public GameObject backpackPanel;
	[HideInInspector]
	public bool isShowBackpackpanel = false;

	public GameObject mallPanel;
	[HideInInspector]
	public bool isShowMallpanel = false;

	public GameObject rankPanel;
	public GameObject rankMenuImage;
	public GameObject rankCoinListImage;
	public GameObject rankStudyListImage;
	[HideInInspector]
	public bool isShowRankpanel = false;


	// Use this for initialization
	void Start () {
		
		isShowBackpackpanel = false;

		isShowMallpanel = false;

		isShowRankpanel = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	/// <summary>
	/// Raises the backpack button event.
	/// </summary>
	public void OnBackpackButton() {

		isShowMallpanel = false;
		isShowRankpanel = false;

		isShowBackpackpanel = !isShowBackpackpanel;

		//显示背包界面
		if (isShowBackpackpanel == true) {
			MapLevelSelectGameManager.Instance.ShowBackpackPanel ();
		} 
	}

	/// <summary>
	/// Raises the mall button event.
	/// </summary>
	public void OnMallButton() {

		isShowBackpackpanel = false;
		isShowRankpanel = false;

		isShowMallpanel = !isShowMallpanel;

		//显示商城界面
		if(isShowMallpanel == true){
			MapLevelSelectGameManager.Instance.ShowMallPanel ();
		}else {
			MapLevelSelectGameManager.Instance.HideBackpackMallRankPanel ();
		}
	}

	/// <summary>
	/// Raises the rank button event.
	/// </summary>
	public void OnRankButton() {
		
		isShowBackpackpanel = false;
		isShowMallpanel = false;

		isShowRankpanel = !isShowRankpanel;

		//显示排行榜主菜单
		MapLevelSelectGameManager.Instance.ShowRankMenuImage ();

		//显示排行榜界面
		if(isShowRankpanel == true){
			MapLevelSelectGameManager.Instance.ShowRankPanel ();
		}else {
			MapLevelSelectGameManager.Instance.HideBackpackMallRankPanel ();
		}

	}

	/// <summary>
	/// Raises the coin rank button event.
	/// </summary>
	public void OnCoinRankButton(){
		MapLevelSelectGameManager.Instance.ShowRankCoinListImage ();
	}
		
	/// <summary>
	/// Raises the study rank button event.
	/// </summary>
	public void OnStudyRankButton(){
		MapLevelSelectGameManager.Instance.ShowRankStudyListImage ();
	}
}
