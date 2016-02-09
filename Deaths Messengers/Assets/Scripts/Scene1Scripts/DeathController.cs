using UnityEngine;
using System.Collections;

public class DeathController : MonoBehaviour {

	// Use this for initialization
	private Animator anim;
	private GameObject guy;

	void Start () {
		anim = GetComponent<Animator> ();
		guy = GameObject.Find ("Guy");
	}
	
	// Update is called once per frame
	void Update () {
		if (guy.transform.position.x <= 108) {
			anim.Play ("Idle");
		}
	}
}
