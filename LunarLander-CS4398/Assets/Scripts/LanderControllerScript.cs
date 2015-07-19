using UnityEngine;
using System.Collections;



public class LanderControllerScript : MonoBehaviour {


	public GameObject thrusters;
	public GameObject leftThruster;
	public GameObject rightThruster;

	public float thrustAcceleration = 5f;
	public float thrustRotation = 5f;
	public float maxSpeed = 10f;

	private Rigidbody2D landerRigidBody;





	// Use this for initialization
	void Start () {
		hideThrusters ();
		landerRigidBody = GetComponent<Rigidbody2D>();
	}


	// This function is called every time the physics engine updates. Because the Lander
	// uses physics, FixedUpdate() should be called instead of Update()
	void FixedUpdate () {

		// Level Select
		if (Input.GetKeyDown ("1")) {			
			GameManager.LoadLevel(0);			
			return;
		}
		else if (Input.GetKeyDown ("2")) {			
			GameManager.LoadLevel(1);			
			return;
		}
		else if (Input.GetKeyDown ("3")) {			
			GameManager.LoadLevel(2);			
			return;
		}

		// Restart Level
		if (Input.GetKeyDown ("r")) {
			GameManager.RestartLevel();
			return;
		}





		// Lander Controls

		// Get Left/Right Input. This will be used to rotate the Lander.
		float rotate = Input.GetAxisRaw ("Horizontal");

		if (rotate != 0) {

			//applyRotation (rotate);
			// Apply Torque applies rotation to the Physics System
			applyTorque(rotate);

			// User is rotating clockwise
			if(rotate > 0)
			{
				showLeftThruster();
			}
			else
			{
				showRightThruster();
			}

		} else {
			hideLeftThruster();
			hideRightThruster();
		}

		// Get Up/Down Input. This will be used to rotate the Lander.
		float thrust = Input.GetAxisRaw ("Vertical");
		if (thrust > 0f) {
			applyThrust(thrust);
			showThrusters();
		} 
		else {
			hideThrusters();
		}

	}




	void applyThrust(float thrust)
	{
		landerRigidBody.AddForce(transform.up * thrustAcceleration);
	}

	void applyRotation(float rotation)
	{
		float initialRotation = transform.rotation.eulerAngles.z;
		float finalRotation = initialRotation + (-rotation * thrustRotation);

		// This Lerp function slowly applies the rotation over time
		transform.rotation = Quaternion.Lerp ( transform.rotation, Quaternion.Euler(0,0,finalRotation), Time.deltaTime*thrustRotation);
	}

	void applyTorque(float rotation)
	{
		landerRigidBody.AddTorque(-rotation * thrustRotation/10);
	}


	// These two functions show and hide the Thrusters GameObject in the Lander

	void showThrusters()
	{
		thrusters.SetActive(true);
	}
	
	void hideThrusters()
	{
		thrusters.SetActive(false);
	}
	
	void showLeftThruster()
	{
		leftThruster.SetActive(true);
	}
	
	void hideLeftThruster()
	{
		leftThruster.SetActive(false);
	}
	
	void showRightThruster()
	{
		rightThruster.SetActive(true);
	}
	
	void hideRightThruster()
	{
		rightThruster.SetActive(false);
	}


}