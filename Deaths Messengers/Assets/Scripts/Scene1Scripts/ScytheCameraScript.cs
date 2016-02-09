using UnityEngine;
using System.Collections;

public class ScytheCameraScript : MonoBehaviour {

	// Use this for initialization
	private GameObject mainCamera;

	void Start () {
		mainCamera = GameObject.Find ("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
		if (mainCamera.GetComponent<MainCameraScript> ().startMovingScytheCamera ()
			&& transform.position.y > 4) {
			transform.Translate (new Vector3 (0, 0, 1));
		}
	}
}
