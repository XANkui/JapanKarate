using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton.继承后无需挂载的泛型单例模板
/// </summary>
public class Singleton<T> where T : MonoBehaviour ,new(){

	//设置泛型单例
	private static volatile T instance;

	//设置一把锁，避免多线程影响
	private static object locker = new object();

	//设置单例属性
	public static T Instance {
		get { 
			if(instance == null) {

				//加锁操作，避免单例不唯一
				lock (locker) {
					if(instance == null){
						GameObject obj = new GameObject (typeof(T).Name);
						instance = obj.AddComponent <T> ();
					}
				}

			}

			return instance;
		}
	}

	/// <summary>
	/// Awake this instance.可继承修改的函数
	/// </summary>
	protected virtual void Awake() {
		instance = this as T;
	}

}
