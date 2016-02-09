using UnityEngine;
using System.Collections;

public class ModifiedGuyScriptScene5 : MonoBehaviour {

	enum SceneState {WalkingOffPorch, Thinking, Water, waitAtWater};

	private SceneState currentState = SceneState.WalkingOffPorch;

	Animator anim;
	public Vector3[] wanderPoints;
	public float wanderSpeed;
	private int wanderIndex;
	private float thinkingOnPorch = 0;
	private float pauseForABit = 0;
	private float thinkingPeriod = 0;
	private GameObject mainCamera;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		wanderIndex = 0;
		mainCamera = GameObject.Find ("Camera Manager");
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentState) {
			case SceneState.WalkingOffPorch:
				thinkingOnPorch += Time.deltaTime;
				if(thinkingOnPorch > 1) { //TODO:Change this for when he talks on porch
					pauseForABit += Time.deltaTime;
					MoveTo (wanderPoints [wanderIndex], wanderSpeed);
					if(transform.position.z < 269.8f && pauseForABit > 2.0f) {
						mainCamera.GetComponent<MainCameraScriptScene5>().OnTriggerSwitchCam2();
						currentState = SceneState.Thinking;
						wanderIndex++;
					} else {
						anim.Play ("Walking");
					}
				} else {
					//anim.Play ();
				}
				break;
			case SceneState.Thinking:
				thinkingPeriod += Time.deltaTime;
				if(thinkingPeriod < 10) {
					anim.Play ("Thinking");
				} else {
					anim.Play ("Excited");
					currentState = SceneState.Water;
				}
				break;
			case SceneState.Water:
				MoveTo(wanderPoints[wanderIndex], wanderSpeed);
				if(transform.eulerAngles.y > 210) {
					transform.Rotate(new Vector3(0,-1f,0));
				}
				anim.Play ("WalkingToWater");
				if(transform.position.x == 202.9) {
					currentState = SceneState.waitAtWater;
				}
				break;
			case SceneState.waitAtWater:
				anim.Play ("Idle");
				break;
		}
	}

	bool MoveTo(Vector3 targetPos, float speed) {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, targetPos, step);
		
		return (transform.position == targetPos);
	}
}
