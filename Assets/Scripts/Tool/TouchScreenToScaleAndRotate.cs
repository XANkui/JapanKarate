using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Touch screen to scale and rotate.绑定在游戏物体上，
/// 游戏物体既可以通过触摸屏幕，放大缩小，以及旋转
/// </summary>
public class TouchScreenToScaleAndRotate : MonoBehaviour {

	private Touch oldTouch1;  //上次触摸点1(手指1)  
	private Touch oldTouch2;  //上次触摸点2(手指2)  

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()  
	{  

	}  
		
	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {  

		//没有触摸  
		if ( Input.touchCount <= 0 ){  
			return;  
		}  

		//单点触摸， 水平上下旋转  
		if( 1 == Input.touchCount ){  
			Touch touch = Input.GetTouch (0);  
			Vector2 deltaPos = touch.deltaPosition;           
			transform.Rotate(Vector3.down  * deltaPos.x , Space.Self); 
			//transform.RotateAround (transform.position, Vector3.down, Vector3.down  * deltaPos.x);

			//transform.Rotate(Vector3.right * deltaPos.y , Space.World);  
		}  

		//多点触摸, 放大缩小  
		Touch newTouch1 = Input.GetTouch (0);  
		Touch newTouch2 = Input.GetTouch (1);  

		//第2点刚开始接触屏幕, 只记录，不做处理  
		if( newTouch2.phase == TouchPhase.Began ){  
			oldTouch2 = newTouch2;  
			oldTouch1 = newTouch1;  
			return;  
		}  

		//计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型  
		float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);  
		float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);  

		//两个距离之差，为正表示放大手势， 为负表示缩小手势  
		float offset = newDistance - oldDistance;  

		//放大因子， 一个像素按 0.01倍来算(100可调整)  
		float scaleFactor = offset / 100f;  
		Vector3 localScale = transform.localScale;  
		Vector3 scale = new Vector3(localScale.x + scaleFactor,  
			localScale.y + scaleFactor,   
			localScale.z + scaleFactor);  

		//最小缩放到 0.3 倍  
		if (scale.x > 0.1f && scale.y > 0.1f && scale.z > 0.1f) {  
			transform.localScale = scale;  
		}  

		//记住最新的触摸点，下次使用  
		oldTouch1 = newTouch1;  
		oldTouch2 = newTouch2;  
	}  
}
