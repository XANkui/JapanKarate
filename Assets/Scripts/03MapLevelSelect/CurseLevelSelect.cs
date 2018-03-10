using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CurseLevelSelect : MonoBehaviour {

	private bool isSelected = false;
	public GameObject lockImage;

	//学习与否,0表示未学习，1表示学习了
	private int studiedOrNot = 0;

	//标记学习与否
	public Text labelText;

	//游戏场景
	private int CURSESCENE = 4;

	// Use this for initialization
	void Start () {

		//第一个关卡自动解锁
		if (transform.parent.GetChild(0).name == gameObject.name) {
			isSelected = true;            
		}

		//判断现已管是否能解锁
		UnlockNextCurse ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Unlocks the next curse.
	/// </summary>
	private void UnlockNextCurse() {
		//获取前一关卡的星星数是否大于1，大于则当前关卡解锁
		int beforeLevelNum = int.Parse(gameObject.name) - 1;
		if (PlayerPrefs.GetInt("curseLevel"+ beforeLevelNum.ToString()) > 0) {
			isSelected = true;
		}

		if (isSelected == true) {
			//解锁学习课程
			lockImage.SetActive (false);

			studiedOrNot = PlayerPrefs.GetInt ("curseLevel" + gameObject.name);
			if (studiedOrNot > 0){
				labelText.text = "已学";
			}
		}
	}

	/// <summary>
	/// Raises the course level button event.
	/// </summary>
	public void OnCourseLevelButton() {
		if (isSelected == true) {
			//进入场景设置为已学习,0表示为学习,1表示已学习
			PlayerPrefs.SetInt ("curseLevel" + gameObject.name, 1);

			//保存选择场景
			PlayerPrefs.SetString("nowLevel", "curseLevel" + gameObject.name);
			SceneManager.LoadScene(CURSESCENE);
		}  
	}
}
