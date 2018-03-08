using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoginAndRigister : MonoBehaviour {

	//获得UserRigisterAndLogin脚本
	public UserRigisterAndLogin userLoginAndRigister;

	//获取登录和注册Panel
	public GameObject loginPanel;
	public GameObject rigisterPanel;

	//获取登录界面的用户名，密码和提示文本
	public InputField loginUserNameText;
	public InputField loginPasswordText;
	public Text loginTipText;

	//获取注册界面的用户名，密码和提示文本
	public InputField rigisterUserNameText;
	public InputField rigisterFirstPasswordText;
	public InputField rigisterSecondPasswordText;
	public Text rigisterTipText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Raises the login account button event.
	/// </summary>
	public void OnLoginAccountButton() {
		userLoginAndRigister.LoginAccount ();
	}

	/// <summary>
	/// Raises the rigister account button event.
	/// </summary>
	public void OnRigisterAccountButton() {
		userLoginAndRigister.RigisterAccount ();
	}

	/// <summary>
	/// Raises the jump rigister user interface button event.
	/// </summary>
	public void OnJumpRigisterUIButton() {
		//清空之前的注册信息
		rigisterUserNameText.text = "";
		rigisterFirstPasswordText.text = "";
		rigisterSecondPasswordText.text = "";

		loginPanel.SetActive (false);
		rigisterTipText.text = "欢迎观临";
		rigisterPanel.SetActive (true);
	}

	/// <summary>
	/// Raises the jump login user interface button event.
	/// </summary>
	public void OnJumpLoginUIButton() {
		//刷新记载注册的用户名和密码
		LoginDeflautUsernameAndPassword();

		rigisterPanel.SetActive (false);
		loginTipText.text = "欢迎光临";
		loginPanel.SetActive (true);			
	}

	/// <summary>
	/// Logins the deflaut username and password.
	/// </summary>
	public void LoginDeflautUsernameAndPassword () {
		//加载默认的用户名
		loginUserNameText.text = PlayerPrefs.GetString ("username", "");

		//加载默认的密码
		loginPasswordText.text = PlayerPrefs.GetString ("password", "");
	}
}
