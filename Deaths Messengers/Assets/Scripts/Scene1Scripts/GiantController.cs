using UnityEngine;
using System.Collections;

public class GiantController : MonoBehaviour {
	
	enum SceneState { GiantFighting, GiantRunning };
	private SceneState currentState = SceneState.GiantFighting;

	public GameObject guy;
	public Vector3[] wanderPoints;
	public float wanderSpeed;
	private int wanderIndex;
	private Animator anim;
	private float hitTimer = 0;
	// Use this for initialization
	void Start () {
		wanderIndex = 0;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentState) {
		case SceneState.GiantFighting: 
			break;
		case SceneState.GiantRunning:
			anim.Play ("Hit");
			hitTimer += Time.deltaTime;
			if (hitTimer > 5.0f) {
				if (transform.eulerAngles.y >= 20) {
					transform.Rotate (new Vector3 (0, -5, 0) * 15 * Time.deltaTime);
				} else {
					anim.Play ("Running");
					MoveTo (wanderPoints [wanderIndex], wanderSpeed);
				}
			}
			break;
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject == guy) {
			currentState = SceneState.GiantRunning;
		}
	}

	bool MoveTo(Vector3 targetPos, float speed) {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, targetPos, step);
		
		return (transform.position == targetPos);
	}


}
