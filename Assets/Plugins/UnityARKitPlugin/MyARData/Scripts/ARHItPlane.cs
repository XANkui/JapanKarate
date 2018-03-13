using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.iOS;
using UnityEngine.UI;

public class ARHItPlane : MonoBehaviour {

	public Transform m_HitTransform;
	public GameObject hitSetObject;
	public GameObject hitSetUI;
	public GameObject tipText;
	public GameObject MainUI;
	public PointCloudParticleExample particleExample;

	private bool isHit = true;
	private GameObject HitSetOb;
	private Button RelocateBtn;
	private Button SureBtn;

	void Awake() {

		GameObject.FindGameObjectWithTag ("RelocateBtn").GetComponent <Button> ().onClick.AddListener (HitReset);
		GameObject.FindGameObjectWithTag ("SureBtn").GetComponent <Button> ().onClick.AddListener (HitSure);

	}

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

	// Update is called once per frame
	void Update () {
		
		if (Input.touchCount > 0 )
		{
			var touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began && isHit )					
			{
				isHit = false;
				tipText.SetActive (false);
				hitSetUI.SetActive (true);
				//particleExample.gameObject.SetActive(false);
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
						HitSetOb = Instantiate (hitSetObject, m_HitTransform.position, Quaternion.identity);

						break;
					}
				}
			}
		}
	}

	public void HitReset() {

		Debug.Log ("HitReset");

		HitSetInit ();
		Destroy (HitSetOb);

	}

	private void HitSetInit() {

		isHit = true;
		hitSetUI.SetActive (false);
		tipText.SetActive (true);
		PointCloudParticleExample.Instance.StartScan ();
	}

	private void HitSure() {

		Debug.Log ("HitSure");

		hitSetUI.SetActive (false);
		MainUI.SetActive (true);
	}
}

