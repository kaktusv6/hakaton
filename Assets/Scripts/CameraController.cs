using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		VuforiaARController vuforia = VuforiaARController.Instance;
		vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);
//		vuforia.RegisterOnPauseCallback(OnPaused);
	}
	
	private void OnVuforiaStarted()
	{
		CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		
	}
 
	private void OnPaused(bool paused)
	{
		if (!paused) // resumed
		{
			// Set again autofocus mode when app is resumed
			CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void RegisterTrack()
	{
		
	}
	
	private void OnGUI()
	{
//		String textPosition = "X: " + transform.position.x + " Y: " + transform.position.y + " Z: " + transform.position.z;
//		GUI.Box(new Rect(0 ,0, 250, 20), textPosition);
	}
}
