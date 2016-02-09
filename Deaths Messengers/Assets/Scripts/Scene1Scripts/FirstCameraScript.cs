using UnityEngine;
using System.Collections;

public class FirstCameraScript : MonoBehaviour {

	public GameObject mainCamera; 
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.y > 10) 
			transform.Translate (Vector3.down * Time.deltaTime * 3);
		else {
			mainCamera.GetComponent<MainCameraScript>().OnTriggerSwitchCam1();
		}
	}
}
