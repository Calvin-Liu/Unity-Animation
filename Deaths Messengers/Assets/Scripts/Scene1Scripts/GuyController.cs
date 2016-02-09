using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuyController : MonoBehaviour {
	enum SceneState { Walking, hearsSomething, reaperGiantFight, runToHelp, getScythe, 
	attackGiant };

	private SceneState currentState = SceneState.Walking;

	/*
	private GameObject giantRef;
	private GiantController giantControllerRef;
	private GameObject deathRef;
	private DeathController deathControllerRef;
	*/

	public Vector3[] wanderPoints;
	public float wanderSpeed;
	public float runSpeed;
	private int wanderIndex;
	public AudioSource source;
	//public AudioClip dramaticMusic;
	Animator anim;

	private float waitTimer = 0.0f;
	private float waitTimeForRun = 0.0f;
	public float waitTime = 2.0f;
	private float waitTimeToPickUpScythe = 0.0f;
	public float waitTimeForHelp = 10.0f;
	public GameObject scene2_1;
	public GameObject scythe;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		wanderIndex = 0;
		anim = GetComponent<Animator> ();
		/*
		giantRef = GameObject.Find ("Giant");
		giantControllerRef = giantRef.GetComponent<GiantController> ();
		deathRef = GameObject.Find ("Death");
		deathControllerRef = deathRef.GetComponent<DeathController> ();
		*/
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentState) {
			case SceneState.Walking:
				if(MoveTo(wanderPoints[wanderIndex], wanderSpeed)) {
					wanderIndex++;
					//Next checkpoint
					if(wanderIndex == 1) {
						currentState = SceneState.hearsSomething;
					}
				}
				break;

			case SceneState.hearsSomething:
				//This is where he looks around because he hears the giant and death fighting
				if(MoveTo(wanderPoints[wanderIndex], wanderSpeed)) {
					if(transform.position.x <= 184.0f) {
						currentState = SceneState.reaperGiantFight;
					}
				}
				break;
				
			case SceneState.reaperGiantFight:
				//put dramatic music change here
				//must wait before running for a certain time
				waitTimer += Time.deltaTime;
				if(waitTimer >= waitTime) {
					wanderIndex++;
					currentState = SceneState.runToHelp;
				}
				break;

			case SceneState.runToHelp:
				waitTimeForRun += Time.deltaTime;
				if(waitTimeForRun >= waitTimeForHelp) {
					MoveTo (wanderPoints[wanderIndex], runSpeed);
				}
				if(transform.position.x == 136.5f) {
					stopMusic();
					scene2_1.GetComponent<Scene2Camera1Script>().playMusicForScene2_1();
					wanderIndex++;
					currentState = SceneState.getScythe;
				}
				break;
			case SceneState.getScythe:
				MoveTo (wanderPoints[wanderIndex], runSpeed);
				if(transform.position.x <= 123) {
					waitTimeToPickUpScythe += Time.deltaTime;
					if(waitTimeToPickUpScythe >= 1.0f) {
						wanderIndex++;
						currentState = SceneState.attackGiant;
					}
				}
				break;
			case SceneState.attackGiant:
				//Debug.Log("attacking giant scene");
				scythe.GetComponent<ScytheScript>().moveScytheToGiant();
				MoveTo (wanderPoints[wanderIndex], runSpeed);
				break;
		}
	}

	bool MoveTo(Vector3 targetPos, float speed) {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, targetPos, step);

		return (transform.position == targetPos);
	}

	IEnumerator waiting(float seconds) {
		yield return new WaitForSeconds(seconds);
	}

	void stopMusic() {
		source.Stop ();
	}

	void playMusic() {
		source.Play ();
	}
}
