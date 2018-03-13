using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Load curse level.
/// </summary>
public class LoadCurseLevel : MonoBehaviour {

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()  {

		//确定资源加载的路径
		string path = "Prefabs/CurseLevel/" + PlayerPrefs.GetString ("nowLevel");

		//生成对应场景
		Instantiate (Resources.Load (path));
	}

	/// <summary>
	/// Loads the scene.
	/// </summary>
	/// <param name="sceneIndex">Scene index.</param>
	public void LoadScene(int sceneIndex) {
		
		SceneManager.LoadScene (sceneIndex);
	}

}
