using UnityEngine;
using System.Collections;

public class Scene3Camera3Script : MonoBehaviour {

	// Use this for initialization
	public float waitTime;
	private float waitTicker = 0.0f;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		waitTicker += Time.deltaTime;
		if (waitTicker > waitTime) {
			transform.Translate (new Vector3 (.05f, 0, 0));
			transform.Rotate (new Vector3 (0, -.08f, 0));
		}
	}
}
