using UnityEngine;
using System.Collections;

public class ScytheScript : MonoBehaviour {

	// Use this for initialization
	public GameObject guy;
	bool transformScythe;
	public Vector3[] wanderPoints;
	public float runSpeed;
	private int wanderIndex;
	bool moveScythe = false;

	void Start () {
		transformScythe = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (transformScythe) {
			if(!moveScythe) {
				transform.position = new Vector3(123.6f, 3.9f, 417.7f);
			}
			transform.localEulerAngles = new Vector3(0, 180, 300);
			if(moveScythe) {
				MoveTo(wanderPoints[0], runSpeed);
			}
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject == guy) {
			scythePickedUp();
		}
	}

	void scythePickedUp() {
		transformScythe = true;
	}

	public void moveScytheToGiant() {
		moveScythe = true;
	}

	public bool scytheIsUp() {
		return transformScythe;
	}

	bool MoveTo(Vector3 targetPos, float speed) {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, targetPos, step);
		
		return (transform.position == targetPos);
	}
}
