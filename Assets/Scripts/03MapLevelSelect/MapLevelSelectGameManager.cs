using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cn.bmob.api;
using cn.bmob.io;
using cn.bmob.tools;
using NUnit.Framework;
using System.IO;

/// <summary>
/// Map level select game manager.
/// </summary>
public class MapLevelSelectGameManager : MonoBehaviour {

	//脚本单例化
	private static MapLevelSelectGameManager instance;
	//单例属性
	public static MapLevelSelectGameManager Instance {
		get {
			return instance;
		}
	}

	//设置一个静态BmobUnity，里面包含调用账号的API等信息
	private static BmobUnity Bmob;

	//定义获取Bmob数据的表的名称
	private string TABLENAME = "_User";

	//定义登录账户保存的objectId
	private string objectId;

	//获取UIMapLevelSelect脚本
	public UIMapLevelSelect uiMapLevelSelect;

	//金币排行榜列表
	private List<GameObject> coinRankList;

	//学习排行榜列表
	private List<GameObject> studyRankList;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake(){
		//单例初始化
		instance = this;
	}

	// Use this for initialization
	void Start () {
		//Bmob初始化
		BmobInit();

		//获取保存的用户数据objectId
		objectId = PlayerPrefs.GetString ("objectId");

		//获取Bmob用户数据并显示
		GetUserBmobData ();

		//初始化金币、学习进度列表
		coinRankList = new List<GameObject>();
		studyRankList = new List<GameObject>();
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
	/// Bmob game object.
	/// </summary>
	public class BmobGameObject : BmobTable {
		//用户名
		public string username;

		//金币数
		public int coin;

		//已经学了的课程数量
		public int studiedCourse;

		//总的课程数量
		public int totalCourse;

		/// <summary>
		/// 把服务端返回的数据转化为本地对象值
		/// </summary>
		/// <param name="input"></param>
		public override void readFields (BmobInput input)
		{
			//继承基类的代码
			base.readFields (input);

			//添加读取自己添加定义的数据的接口
			username = input.Get<string> ("username");
			coin = input.Get<int> ("Coin");
			studiedCourse = input.Get<int> ("StudiedCourse");
			totalCourse = input.Get<int> ("TotalCourse");
		}

		/// <summary>
		/// 把本地对象写入到output，后续序列化提交到服务器
		/// </summary>
		/// <param name="output"></param>
		/// <param name="all">用于区分请求/打印输出序列化</param>
		public override void write (BmobOutput output, bool all)
		{
			base.write (output, all);
		}
	}

	/// <summary>
	/// Gets the user bmob data.
	/// </summary>
	public void GetUserBmobData() {

		Bmob.Get <BmobGameObject>(TABLENAME, objectId,
			(response, exception)=>{
				if(exception != null){
					print("数据获取失败：" + exception);

					return;
				}

				#if UNITY_EDITOR
				print("数据获取成功.");
				#endif

				/* 获取数据显示在UI上 
				 * 1、获取用户名数据显示
				 * 2、获取金币数据显示
				 * 3、获取已经学习的课程数和总的课程数	*/				 			 
				uiMapLevelSelect.userNameText.text = response.username;
				uiMapLevelSelect.coinText.text = "金币：" + response.coin;
				uiMapLevelSelect.studyText.text = "学习进度：" + response.studiedCourse +"/"+ response.totalCourse;

			}
		);
	}

	/// <summary>
	/// Coins the rank data.
	/// </summary>
	public void CoinRankData() {

		/* 设置获取表的属性
		 * 1、获取金币从大到小排行
		 * 2、获取前10个数据 
		*/
		BmobQuery queryData =  new BmobQuery();
		queryData.OrderByDescending ("Coin");
		queryData.Limit (10);

		//从Bmob数据库获取表的内容
		Bmob.Find <BmobGameObject>(TABLENAME, queryData,
			(response, exception)=>{
				if(exception != null){
					print ("数据列表获取失败：" + exception);

					return;
				}

				print ("获取数据列表成功。");

				/* 生成排行榜之前的处理
				 * 1、删掉之前的预制体
				 * 2、清空列表
				*/
				for(int i = 0; i < coinRankList.Count; i++){
					Destroy (coinRankList[i]);
				}
				coinRankList.Clear ();

				//获取返回的津滨排行数据
				List<BmobGameObject> list = response.results;
				foreach(var data in list){
					/* 金币排行榜数据
					 * 1、用户名
					 * 2、金币数
					 * 3、设置生成的属性
					 * 4、把生成的预制体添加到列表
					*/
					GameObject coinRankData = Instantiate (uiMapLevelSelect.coinListDataPrefab);
					coinRankData.transform.parent = uiMapLevelSelect.coinListDataParent;
					coinRankData.transform.localRotation = uiMapLevelSelect.coinListDataPrefab.transform.localRotation;
					coinRankData.transform.localScale = Vector3.one;
					coinRankData.GetComponent <RankDataAttr>().userNameText.text = data.username;
					coinRankData.GetComponent <RankDataAttr>().dataText.text = data.coin.ToString ();

					coinRankList.Add (coinRankData);
				}

			}
		);
	}

	/// <summary>
	/// Studies the rank data.
	/// </summary>
	public void StudyRankData(){
		/* 设置获取表的属性
		 * 1、获取学习进度从大到小排行
		 * 2、获取前10个数据 
		*/
		BmobQuery queryData =  new BmobQuery();
		queryData.OrderByDescending ("StudiedCourse");
		queryData.Limit (10);

		//从Bmob数据库获取表的内容
		Bmob.Find <BmobGameObject>(TABLENAME, queryData,
			(response, exception)=>{
				if(exception != null){
					print ("数据列表获取失败：" + exception);

					return;
				}

				print ("获取数据列表成功。");

				/* 生成排行榜之前的处理
				 * 1、删掉之前的预制体
				 * 2、清空列表
				*/
				for(int i = 0; i < studyRankList.Count; i++){
					Destroy (studyRankList[i]);
				}
				studyRankList.Clear ();

				//获取返回的津滨排行数据
				List<BmobGameObject> list = response.results;
				foreach(var data in list){
					/* 金币排行榜数据
					 * 1、用户名
					 * 2、学习进度
					 * 3、设置生成的属性
					 * 4、并把生成的预制体添加到列表
					*/
					GameObject studyRankData = Instantiate (uiMapLevelSelect.studyListDataPrefab);
					studyRankData.transform.parent = uiMapLevelSelect.studyListDataParent;
					studyRankData.transform.localRotation = uiMapLevelSelect.studyListDataPrefab.transform.localRotation;
					studyRankData.transform.localScale = Vector3.one;
					studyRankData.GetComponent <RankDataAttr>().userNameText.text = data.username;
					studyRankData.GetComponent <RankDataAttr>().dataText.text = data.studiedCourse +"/"+data.totalCourse;

					studyRankList.Add (studyRankData);
				}

			}
		);
	}

	/// <summary>
	/// Shows the backpack panel.
	/// </summary>
	public void ShowBackpackPanel(){
		/* 显示背包面板
		 * 1、隐藏商城和排行榜界面
		 * 2、显示背包界面
		*/
		uiMapLevelSelect.mallPanel.SetActive (false);
		uiMapLevelSelect.rankPanel.SetActive (false);
		uiMapLevelSelect.backpackPanel.SetActive (true);
	}

	/// <summary>
	/// Shows the mall panel.
	/// </summary>
	public void ShowMallPanel(){
		/* 显示商城面板
		 * 1、隐藏背包和排行榜界面
		 * 2、显示商城界面
		*/
		uiMapLevelSelect.backpackPanel.SetActive (false);
		uiMapLevelSelect.rankPanel.SetActive (false);
		uiMapLevelSelect.mallPanel.SetActive (true);
	}

	/// <summary>
	/// Shows the rank panel.
	/// </summary>
	public void ShowRankPanel(){
		/* 显示商城面板
		 * 1、隐藏背包和商城界面
		 * 2、显示排行榜界面
		*/
		uiMapLevelSelect.backpackPanel.SetActive (false);
		uiMapLevelSelect.mallPanel.SetActive (false);
		uiMapLevelSelect.rankPanel.SetActive (true);
	}

	/// <summary>
	/// Hides the backpack mall rank panel.
	/// </summary>
	public void HideBackpackMallRankPanel() {
		/* 隐藏界面
		 * 1、隐藏背包界面
		 * 2、隐藏商城界面
		 * 3、隐藏排行榜界面
		*/
		uiMapLevelSelect.backpackPanel.SetActive (false);
		uiMapLevelSelect.mallPanel.SetActive (false);
		uiMapLevelSelect.rankPanel.SetActive (false);
	}

	/// <summary>
	/// Shows the rank menu image.
	/// </summary>
	public void ShowRankMenuImage() {
		/* 显示排行榜菜单
		 * 1、隐藏金币排行榜和学习进度排行榜
		 * 2、显示排行榜菜单
		*/
		uiMapLevelSelect.rankCoinListImage.SetActive (false);
		uiMapLevelSelect.rankStudyListImage.SetActive (false);
		uiMapLevelSelect.rankMenuImage.SetActive (true);
	}

	/// <summary>
	/// Shows the rank coin list image.
	/// </summary>
	public void ShowRankCoinListImage() {
		/* 显示金币排行榜
		 * 1、隐藏排行榜菜单和学习进度排行榜
		 * 2、显示金币排行榜
		*/
		uiMapLevelSelect.rankMenuImage.SetActive (false);
		uiMapLevelSelect.rankStudyListImage.SetActive (false);
		uiMapLevelSelect.rankCoinListImage.SetActive (true);

		//获取金币排行榜数据
		CoinRankData ();
	}

	/// <summary>
	/// Shows the rank study list image.
	/// </summary>
	public void ShowRankStudyListImage() {
		/* 显示学习排行榜
		 * 1、隐藏排行榜菜单和金币进度排行榜
		 * 2、显示学习排行榜
		*/
		uiMapLevelSelect.rankMenuImage.SetActive (false);
		uiMapLevelSelect.rankCoinListImage.SetActive (false);
		uiMapLevelSelect.rankStudyListImage.SetActive (true);

		//获取学习排行榜数据
		StudyRankData ();
	}



}
