using UnityEngine;
using System.Collections;

public class MainCameraScriptScene5 : MonoBehaviour {

	public Camera camera1;
	public Camera camera2;

	// Use this for initialization
	void Start () {
		camera1.enabled = true;
		camera2.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerSwitchCam2() {
		camera1.enabled = false;
		camera2.enabled = true;
	}
}
