using UnityEngine;
using System.Collections;

public class Scene2Camera1Script : MonoBehaviour {

	AudioSource audioSor;
	// Use this for initialization
	void Start () {
		audioSor = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void playMusicForScene2_1 () {
		audioSor.Play ();
	}
}
