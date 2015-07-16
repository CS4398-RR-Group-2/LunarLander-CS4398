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

		if (Input.GetKeyDown ("r")) {
			print ("space key was pressed");

			Application.LoadLevel(Application.loadedLevelName);

			return;
		}

		// Get Left/Right Input. This will be used to rotate the Lander.
		float rotate = Input.GetAxisRaw ("Horizontal");

		if (rotate != 0) {
			applyRotation (rotate);

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

		//float upDirection = Vector2.up;
		//landerRigidBody.AddForce(Vector3.up * thrustAcceleration * Time.deltaTime);
	}

	void applyRotation(float rotation)
	{
//		Debug.Log ("Rotation = " + transform.rotation.eulerAngles.z);

		float initialRotation = transform.rotation.eulerAngles.z;
		float finalRotation = initialRotation + (-rotation * thrustRotation);

		// This Lerp function slowly applies the rotation over time
		transform.rotation = Quaternion.Lerp ( transform.rotation, Quaternion.Euler(0,0,finalRotation), Time.deltaTime*thrustRotation);
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