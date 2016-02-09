using UnityEngine;
using System.Collections;

public class Scene3Camera2Script : MonoBehaviour {
	
	// Use this for initialization
	public float waitTime;
	private float waitTicker = 0.0f;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		waitTicker += Time.deltaTime;
		if (waitTime < waitTicker) {
			transform.Translate (new Vector3 (-.05f, .05f, 0));
			transform.Rotate (new Vector3 (.1f, .1f, 0));
		}
	}
}
