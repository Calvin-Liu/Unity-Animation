using UnityEngine;
using System.Collections;

public class Scene4Camera1Script : MonoBehaviour {

	private bool startRotation = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.eulerAngles.y > 232 && startRotation) {
			transform.Rotate (0, -.5f, 0);
		}
	}

	public void activateCamera1Rotation() {
		startRotation = true;
	}
}
