using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

namespace UnityStandardAssets.Characters.ThirdPerson
{
	[RequireComponent(typeof (ThirdPersonCharacter))]
	public class ModifiedGuyScriptScene4 : MonoBehaviour
	{
		enum SceneState { LookingAtBookShelf, NothingBookSelf, Bedroom, Kitchen, InKitchen, TheOven }
		private SceneState currentState = SceneState.LookingAtBookShelf;
		
		private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
		private Transform m_Cam;                  // A reference to the main camera in the scenes transform
		private Vector3 m_CamForward;             // The current forward direction of the camera
		private Vector3 m_Move;
		private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
		
		public Vector3[] wanderPoints;
		public float wanderSpeed;
		private int wanderIndex = 0;
		private float thinkingTimer = 0;
		
		public Camera mainCamera;
		private Animator anim;
		private float searchTimer = 0;
		private float idleTimer = 0;
		private float thinkingInKitchenTimer = 0;
		private float over360;
		private float sinkTimer = 0;

		private void Start()
		{
			// get the transform of the main camera
			if (Camera.main != null)
			{
				m_Cam = Camera.main.transform;
			}
			else
			{
				Debug.LogWarning(
					"Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.");
				// we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
			}
			
			// get the third person character ( this should never be null due to require component )
			m_Character = GetComponent<ThirdPersonCharacter>();
			anim = GetComponent<Animator> ();
		}
		
		
		private void Update()
		{
			if (!m_Jump)
			{
				m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}
			switch (currentState) {
				case SceneState.LookingAtBookShelf:
					thinkingTimer += Time.deltaTime;
					if(thinkingTimer > 5) {
						anim.Play ("Walking");
						mainCamera.GetComponent<MainCameraScriptScene4>().activateRot();
						MoveTo (wanderPoints[wanderIndex], wanderSpeed);
						if(transform.eulerAngles.y < 160) {
							transform.Rotate (new Vector3(0,2,0));
						}
						if(transform.position.x == -49.5) {
							currentState = SceneState.NothingBookSelf;
						}
					}
				break;
				case SceneState.NothingBookSelf:
					searchTimer += Time.deltaTime;
					if(searchTimer < 15) {
						if(!this.anim.GetCurrentAnimatorStateInfo(0).IsName("Search Low")) {
							anim.Play ("Search");
						}
						if(!this.anim.GetCurrentAnimatorStateInfo(0).IsName("Search")) {
							mainCamera.GetComponent<MainCameraScriptScene4>().OnTriggerSwitchCam2();
							anim.Play ("Search Low");
						}
					} else {
						currentState = SceneState.Bedroom;
						wanderIndex++;
						anim.Play ("Walking");
					}
				break;
				case SceneState.Bedroom:
					MoveTo(wanderPoints[wanderIndex], wanderSpeed);
					if(transform.eulerAngles.y > 70) {
						transform.Rotate(new Vector3(0, -2, 0));
					}
					if(transform.position.x >= -11.5) {
						anim.Play ("Idle");	
						//mainCamera.GetComponent<MainCameraScriptScene4>().OnTriggerSwitchCam3();
						idleTimer += Time.deltaTime;
						if(idleTimer > 4) {
							currentState = SceneState.Kitchen;
							wanderIndex++;
						}
					}
				break;
				case SceneState.Kitchen:
					if(transform.eulerAngles.y < 347) {
						transform.Rotate(new Vector3(0,-1,0));
					}
					if(transform.position.x != -14) {
						anim.Play("Walking");
					} else {
						anim.Play ("Idle");
					}
					MoveTo (wanderPoints[wanderIndex], wanderSpeed);
					thinkingInKitchenTimer += Time.deltaTime;
					if(thinkingInKitchenTimer > 10) {
						currentState = SceneState.InKitchen;
						wanderIndex++;
					}
				break;
				case SceneState.InKitchen:
					MoveTo(wanderPoints[wanderIndex], wanderSpeed);
					if(transform.eulerAngles.y > 350) {
						over360 = transform.eulerAngles.y - 360;
					}
					if(over360 < 65) { 
						over360 += .5f;
						transform.Rotate(new Vector3(0,0.5f,0));
					}
					if(transform.position.x >= 17.7) {
						anim.Play ("Idle");
						sinkTimer += Time.deltaTime;
						if(sinkTimer > 5) {
							wanderIndex++;
							currentState = SceneState.TheOven;
						}
					} else {
						anim.Play ("Walking");
					}
				break;
				case SceneState.TheOven:
					anim.Play ("Walking");
					transform.Rotate(new Vector3(0, 1, 0));
					MoveTo (wanderPoints[wanderIndex], wanderSpeed);
				break;
			}
		}
		
		
		// Fixed update is called in sync with physics
		private void FixedUpdate()
		{
			// read inputs
			float h = CrossPlatformInputManager.GetAxis("Horizontal");
			float v = CrossPlatformInputManager.GetAxis("Vertical");
			bool crouch = Input.GetKey(KeyCode.C);
			
			// calculate move direction to pass to character
			if (m_Cam != null)
			{
				// calculate camera relative direction to move:
				m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
				m_Move = v*m_CamForward + h*m_Cam.right;
			}
			else
			{
				// we use world-relative directions in the case of no main camera
				m_Move = v*Vector3.forward + h*Vector3.right;
			}
			#if !MOBILE_INPUT
			// walk speed multiplier
			if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
			#endif
			
			// pass all parameters to the character control script
			m_Character.Move(m_Move, crouch, m_Jump);
			m_Jump = false;
		}
		
		bool MoveTo(Vector3 targetPos, float speed) {
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards (transform.position, targetPos, step);
			
			return (transform.position == targetPos);
		}
	}
}
