using UnityEngine;
using System.Collections;

public class MainCameraScript : MonoBehaviour {
	public Camera camera1;
	public Camera camera2;
	public Camera camera3;
	public Camera camera4;
	public GameObject guyRef;
	public GameObject scytheRef;

	private float waitTimer = 0.0f;
	private float waitTimerForScythe = 1.0f;
	private bool change = false;

	// Use this for initialization
	void Start () {
		camera1.enabled = false;
		camera2.enabled = false;
		camera3.enabled = false;
		camera4.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y > 7) 
			transform.Translate (Vector3.down * Time.deltaTime * 4);
		else {
			OnTriggerSwitchCam1();
		}
		if (change) {
			OnTriggerSwitchCam4();
		} 
		if (guyRef.transform.position.x < 146.5f) {
			//Debug.Log ("Switching Cam 2");
			change = false;
			OnTriggerSwitchCam2();
		} 
		if (scytheRef.GetComponent<ScytheScript> ().scytheIsUp ()) {;
			waitTimer += Time.deltaTime;
			if(waitTimer >= waitTimerForScythe) {
				OnTriggerSwitchCam3();
				//Debug.Log ("Switching Cam 3");
			}
		}
	}

	public void OnTriggerSwitchCam1() {
		camera1.enabled = true;
		camera2.enabled = false;
		camera3.enabled = false;
		camera4.enabled = false;
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

	public void SwitchToCam4() {
		change = true;
	}

	public bool startMovingScytheCamera() {
		return change;
	}


	IEnumerator waiting(float seconds) {
		yield return new WaitForSeconds(seconds);
	}
}
