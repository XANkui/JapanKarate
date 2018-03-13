using System;
using System.Collections.Generic;

namespace UnityEngine.XR.iOS
{


	public class UnityARHitTestExample : MonoBehaviour
	{
		//脚本单例化
		private static UnityARHitTestExample instance;
		public static UnityARHitTestExample Instance {
			get {
				return instance;
			}
		}
		
		public Transform m_HitTransform;
		public GameObject playerPrefab;
		public GameObject buttonPanel;
		public GameObject tipPanel;

		public PointCloudParticleExample particleExample;

		private bool isHit = true;
		[HideInInspector]
		public GameObject go = null;
		[HideInInspector]
		public Animation anim;

        bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes)
        {
            List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
            if (hitResults.Count > 0) {
                foreach (var hitResult in hitResults) {
                    Debug.Log ("Got hit!");
                    m_HitTransform.position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
                    m_HitTransform.rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
                    Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", m_HitTransform.position.x, m_HitTransform.position.y, m_HitTransform.position.z));
                    return true;
                }
            }
            return false;
        }

		/// <summary>
		/// Awake this instance.
		/// </summary>
		void Awake() {
			//单例初始化
			instance = this;
		}

		/// <summary>
		/// Start this instance.
		/// </summary>
		void Start() {
			
		}
		
		// Update is called once per frame
		void Update () {
			if (Input.touchCount > 0 )
			{
				var touch = Input.GetTouch(0);
				if (touch.phase == TouchPhase.Began && isHit == true )					
				{
					//放置结束后，这只放置参数为假
					isHit = false;

					//放置好后，操作UI显示
					buttonPanel.SetActive (true);

					//屏幕提示GameObject
					tipPanel.SetActive (false);

					//扫描停止
					PointCloudParticleExample.Instance.StopScan ();


					var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
					ARPoint point = new ARPoint {
						x = screenPosition.x,
						y = screenPosition.y
					};

                    // prioritize reults types
                    ARHitTestResultType[] resultTypes = {
                        ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
                        // if you want to use infinite planes use this:
                        //ARHitTestResultType.ARHitTestResultTypeExistingPlane,
                        ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
                        ARHitTestResultType.ARHitTestResultTypeFeaturePoint
                    }; 
					
                    foreach (ARHitTestResultType resultType in resultTypes)
                    {
                        if (HitTestWithResultType (point, resultType))
                        {
							//生成角色
							go = Instantiate (playerPrefab, m_HitTransform.position, Quaternion.identity);
							anim = go.GetComponent <Animation> ();
							break;
                        }
                    }
				}
			}
		}

		/// <summary>
		/// Destroies the current player.销毁之前放置的角色
		/// </summary>
		public void DestroyCurrentPlayer() {
			
			RelocatedHit ();
			Destroy (go);
		}
			
		/// <summary>
		/// Relocateds the hit.重新放置角色
		/// </summary>
		private void RelocatedHit() {

			//设置放置参数为真
			isHit = true;

			//操作UI隐藏
			buttonPanel.SetActive (false);

			//屏幕提示
			tipPanel.SetActive (true);

			//扫描激活
			PointCloudParticleExample.Instance.StartScan ();
		}

	}
	
}

