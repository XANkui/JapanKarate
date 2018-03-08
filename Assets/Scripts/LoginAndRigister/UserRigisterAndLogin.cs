using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cn.bmob.api;
using cn.bmob.io;
using cn.bmob.tools;

public class UserRigisterAndLogin : MonoBehaviour {

	//设置一个静态BmobUnity，里面包含调用账号的API等信息
	private static BmobUnity Bmob;

	//获取UILoginAndGister的脚本
	public UILoginAndRigister uiLoginAndRigister;

	// Use this for initialization
	void Start () {
		//Bmob初始化
		BmobInit();

		//加载默认的用户名和密码
		uiLoginAndRigister.LoginDeflautUsernameAndPassword ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// 初始化Bmob信息
	/// </summary>
	private void BmobInit() {
		BmobDebug.Register (print);
		BmobDebug.level = BmobDebug.Level.TRACE;
		Bmob = gameObject.GetComponent<BmobUnity> ();

	}

	/// <summary>
	/// Login and rigister bmob user.继承Bmob自带的BmobUser
	/// </summary>
	public class LoginAndRigisterBmobUser : BmobUser{
		
		public override void readFields (BmobInput input)
		{
			base.readFields (input);
		}

		public override void write (BmobOutput output, bool all)
		{
			base.write (output, all);
		}
	}

	/// <summary>
	/// Logins the account.
	/// </summary>
	public void LoginAccount() {

		//没有输入用户名的提示
		if (uiLoginAndRigister.loginUserNameText.text == ""){
			uiLoginAndRigister.loginTipText.text = "请输入用户名";
			return;
		}

		//没有输入密码的提示
		if (uiLoginAndRigister.loginPasswordText.text == ""){
			uiLoginAndRigister.loginTipText.text = "请输入密码";
			return;
		}

		//连接远程的Bmob数据库匹配登录
		Bmob.Login <LoginAndRigisterBmobUser>(uiLoginAndRigister.loginUserNameText.text,
			uiLoginAndRigister.loginPasswordText.text, 
			(response, exception)=>{
				if (exception != null){
					print ("登陆失败"+exception);
					uiLoginAndRigister.loginTipText.text = "用户名或密码错误";
					return;
				}

				#if  UNITY_EDITOR
				print("登录成功");
				#endif

				//保存用户登录的数据objectId，供后期游戏界面加载用户信息使用
				PlayerPrefs.SetString ("objectId", response.objectId);

				//提示登陆成功
				uiLoginAndRigister.loginTipText.text = "登陆成功, 数据加载中...";

				//加载场景，后期可异步加载场景
				UnityEngine.SceneManagement.SceneManager.LoadScene (1);
			}
		);

	}

	/// <summary>
	/// Rigisters the account.
	/// </summary>
	public void RigisterAccount(){

		//没有输入用户名的提示
		if(uiLoginAndRigister.rigisterUserNameText.text == "") {
			uiLoginAndRigister.rigisterTipText.text = "请输入用户名";
			return;
		}

		//没有输入密码的提示
		if(uiLoginAndRigister.rigisterFirstPasswordText.text == "") {
			uiLoginAndRigister.rigisterTipText.text = "请输入密码";
			return;
		}

		//没有输入确认密码的提示
		if(uiLoginAndRigister.rigisterSecondPasswordText.text == "") {
			uiLoginAndRigister.rigisterTipText.text = "请确认密码";
			return;
		}

		//两次密码输入不一致的提示
		if(uiLoginAndRigister.rigisterFirstPasswordText.text != 
			uiLoginAndRigister.rigisterSecondPasswordText.text) {
			uiLoginAndRigister.rigisterTipText.text = "两次输入的密码不一致";
			return;
		}

		//新建一个账户，填入用户名和密码
		LoginAndRigisterBmobUser user = new LoginAndRigisterBmobUser ();
		user.username = uiLoginAndRigister.rigisterUserNameText.text;
		user.password = uiLoginAndRigister.rigisterFirstPasswordText.text;
		//连接远程Bmob数据库把新建用户保存
		Bmob.Signup <LoginAndRigisterBmobUser>(user, 
			(response, exception)=>{
				if (exception != null){
					print ("注册失败"+exception);
					uiLoginAndRigister.rigisterTipText.text = "注册失败，请稍后重试";
					return;
				}

				#if  UNITY_EDITOR
				print("注册成功");
				#endif

				//提示注册成功
				uiLoginAndRigister.rigisterTipText.text = "注册成功";

				//保存注册成功的用命名和密码
				PlayerPrefs.SetString ("username", user.username);
				PlayerPrefs.SetString ("password", user.password);
			}
		);
	}

}
