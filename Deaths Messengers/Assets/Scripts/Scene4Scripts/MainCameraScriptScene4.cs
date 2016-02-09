using UnityEngine;
using System.Collections;

public class MainCameraScriptScene4 : MonoBehaviour {

	public Camera camera1;
	public Camera camera2;
	public Camera camera3;
	public Camera camera4;
	
	// Use this for initialization
	void Start () {
		camera1.enabled = true;
		camera2.enabled = false;
		camera3.enabled = false;
		camera4.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnTriggerSwitchCam2() {
		camera1.enabled = false;
		camera2.enabled = true;
		camera3.enabled = false;
		camera4.enabled = false;
	}
	
	public void OnTriggerSwitchCam3() {
		camera1.enabled = false;
		camera2.enabled = false;
		camera3.enabled = true;
		camera4.enabled = false;
	}
	
	public void OnTriggerSwitchCam4() {
		camera1.enabled = false;
		camera2.enabled = false;
		camera3.enabled = false;
		camera4.enabled = true;
	}

	public void activateRot() {
		camera1.GetComponent<Scene4Camera1Script> ().activateCamera1Rotation ();
	}
}
