using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

public class MapSelect : MonoBehaviour {

	public int unlockCurseNum = 0;
	private bool isSelected = false;
	public GameObject lockPanel;
	public GameObject unlockPanel;

	public GameObject mapPanel;
	public GameObject levelPanel;

	public Text curseText;

	public int startCurrentLevelCourse = 1;
	public int endCurrentLevelCourse = 10;

	//Button事件绑定的参数
	public GameObject bottomBgImage;
	private Button bottomBgBackButtonOnMap;
	private Button bottomBgBackButtonOnCurseLevel;

	// Use this for initialization
	void Start () {

		//清除保存的数据，发布前清理一次注释掉即可，
		//PlayerPrefs.DeleteAll();

		/*
		 * 获取Button组件
		 * 添加点击事件
		*/
		bottomBgBackButtonOnMap = bottomBgImage.transform.Find ("BackButtonOnMap").GetComponent <Button> ();
		bottomBgBackButtonOnMap.onClick.AddListener (OnBottomBgBackButtonLoad2Scene);
		bottomBgBackButtonOnCurseLevel = bottomBgImage.transform.Find ("BackButtonOnCurseLevel").GetComponent <Button> ();
		bottomBgBackButtonOnCurseLevel.onClick.AddListener (OnBottomBgBackButtonShowMap);

		//通过地图解锁条件，判断是否能解锁地图
		UnlockMapCondition ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Unlocks the map condition.
	/// </summary>
	public void UnlockMapCondition(){
		if(PlayerPrefs.GetInt ("studiedCurses", 0) >= unlockCurseNum){
			isSelected = true;
		}

		if (isSelected == true){
			unlockPanel.SetActive (true);
			lockPanel.SetActive (false);

			int studiedCourse = 0;
			for(int i = startCurrentLevelCourse; i <= endCurrentLevelCourse; i++){
				studiedCourse += PlayerPrefs.GetInt ("curseLevel" + i.ToString (), 0);
			}

			int currentLevelCurseTotal = endCurrentLevelCourse - startCurrentLevelCourse + 1;
			curseText.text = studiedCourse.ToString () + "/" + currentLevelCurseTotal.ToString ();
		}
	}

	/// <summary>
	/// Maps the can selected.
	/// </summary>
	public void MapCanSelected(){
		if (isSelected == true) {
			mapPanel.SetActive (false);
			levelPanel.SetActive (true);

			//隐藏、显示按钮
			bottomBgBackButtonOnMap.gameObject.SetActive (false);
			bottomBgBackButtonOnCurseLevel.gameObject.SetActive (true);
		}
	}

	/// <summary>
	/// Backs to map from level.
	/// </summary>
	public void BackToMapFromLevel(){
		if (isSelected == true) {
			mapPanel.SetActive (true);
			levelPanel.SetActive (false);

			//隐藏、显示按钮
			bottomBgBackButtonOnMap.gameObject.SetActive (true );
			bottomBgBackButtonOnCurseLevel.gameObject.SetActive (false);
		}
	}

	/// <summary>
	/// Raises the bottom background back button load2 scene event.
	/// </summary>
	public void OnBottomBgBackButtonLoad2Scene(){
		SceneManager.LoadScene (2);
	}

	/// <summary>
	/// Raises the bottom background back button show map event.
	/// </summary>
	public void OnBottomBgBackButtonShowMap(){
		BackToMapFromLevel ();
	}
		
}
