using UnityEngine;
using System.Collections;

public class GuyCameraScript : MonoBehaviour {
	enum SceneState { Walking, hearsSomething, reaperGiantFight, runToHelp };
	private SceneState currentState = SceneState.Walking;
	private float waitTime = 3.0f;
	private float waitTimeForFight = 5.0f;
	private float waitTimerHearsSomething = 0.0f;
	private float waitTimeCameraFight = 0.0f;
	private float waitTimeWatchFight = 0.0f;
	public GameObject transformToLookAt;
	public GameObject mainCamera;

	public GameObject target = null;
	public int camRotateSpeed = 15;
	public bool orbitY = false;
	private float totalAngle = 0;

	

	void Start () {
		//StartCoroutine (waitFor3Seconds());
	}
	
	// Update is called once per frame
	void Update () {
		switch (currentState) {
			case SceneState.Walking:
				if (target != null) {
					if(orbitY) {
						if(totalAngle < 110) {
							transform.RotateAround(target.transform.position, Vector3.up, Time.deltaTime * camRotateSpeed);
							totalAngle += 10 * Time.deltaTime;
						} else {
							currentState = SceneState.hearsSomething;
						}
					}
				}
				break;
			case SceneState.hearsSomething:
				waitTimerHearsSomething += Time.deltaTime;
				if(waitTimerHearsSomething >= waitTime) {
					//TODO: zoom into facial expression if can
					currentState = SceneState.reaperGiantFight;
				}
				break;
			case SceneState.reaperGiantFight:
				transform.LookAt(transformToLookAt.transform);
				//Debug.Log(transformToLookAt.transform);
				//transform.LookAt (new Vector3(0,-.3f,-1));
				//TODO: Zoom in on the fight
				waitTimeCameraFight += Time.deltaTime;
				if(waitTimeCameraFight <= waitTime) {
					transform.Translate(Vector3.forward * Time.deltaTime * 14);
				} else {
					currentState = SceneState.runToHelp;
				}
				break;
			case SceneState.runToHelp:
				//waitFor3Seconds();
				waitTimeWatchFight += Time.deltaTime;
				if(waitTimeWatchFight >= waitTimeForFight) {
					if(transform.position.x < 150.0) {
						transform.Translate(new Vector3(0, 0, -1));
						//TODO: stablize the camera, it keeps bouncing back and forth
					} else {
						//Switch to zoom into scythe
						mainCamera.GetComponent<MainCameraScript>().SwitchToCam4();
					}
				}
				break;
		}
	}

	//IEnumerator waitFor3Seconds() {
		//yield return new WaitForSeconds(5);
	//}

}
