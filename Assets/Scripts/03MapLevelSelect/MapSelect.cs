using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


	// Use this for initialization
	void Start () {

		//清除保存的数据，发布前清理一次注释掉即可，
		//PlayerPrefs.DeleteAll();

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
			for(int i = startCurrentLevelCourse; i < endCurrentLevelCourse; i++){
				studiedCourse += PlayerPrefs.GetInt ("courseLevel" + i.ToString (), 0);
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
		}
	}

	/// <summary>
	/// Backs to map from level.
	/// </summary>
	public void BackToMapFromLevel(){
		if (isSelected == true) {
			mapPanel.SetActive (true);
			levelPanel.SetActive (false);
		}
	}
}
