using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.ComponentModel;
using UnityEngine.XR.iOS;

public class UIGameAR : MonoBehaviour {


	public GameARGameManager gameARGameManager;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake() {
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	/// <summary>
	/// Raises the previous button event.
	/// </summary>
	public void OnPreviousButton() {
		gameARGameManager.PlayPreviousAnimation ();
	}

	/// <summary>
	/// Raises the next button event.
	/// </summary>
	public void OnNextButton() {
		gameARGameManager.PlayNextAnimation ();
	}

	/// <summary>
	/// Raises the replay button event.
	/// </summary>
	public void OnReplayButton() {
		gameARGameManager.ReplayAnimation ();
	}

	/// <summary>
	/// Raises the relocate button event.
	/// </summary>
	public void OnRelocateButton() {

		UnityARHitTestExample.Instance.DestroyCurrentPlayer ();
	}
}
