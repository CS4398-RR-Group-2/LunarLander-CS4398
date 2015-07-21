﻿using UnityEngine;
using System.Collections;



public class LanderControllerScript : MonoBehaviour {
	
	
	public GameObject thrusters;
	public GameObject leftThruster;
	public GameObject rightThruster;
	public GameObject fuelGauges;

	public float thrustAcceleration = 5f;
	public float thrustRotation = 5f;
	public float maxSpeed = 10f;
	public int fuelAmount = 5000; //an arbitrary amount of fuel for the lander
	public float healthAmount = 100;

	private float xVelocity;
	private float yVelocity;
	private float DAMAGE_THRESHOLD = .7f;
	private float DAMAGE_MULTIPLIER = 3f;
	private int FUEL_BURN0 = 10;
	private int FUEL_BURN1 = 1;
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
				if(fuelAmount <= 0)
					hideLeftThruster();
				else
				{
					applyRotation(rotate);
					showLeftThruster();
					fuelAmount = fuelAmount - FUEL_BURN1;
				}
			}
			else
			{
				if(fuelAmount <= 0)
					hideRightThruster();
				else
				{
					applyRotation(rotate);
					showRightThruster();
					fuelAmount = fuelAmount - FUEL_BURN1;
				}
			}
			
		} else {
			hideLeftThruster();
			hideRightThruster();
		}
		
		// Get Up/Down Input. This will be used to rotate the Lander.
		float thrust = Input.GetAxisRaw ("Vertical");
		if (thrust > 0f) {
			if(fuelAmount <= 0)
				hideThrusters();
			else
			{
				applyThrust(thrust);
				showThrusters();
				fuelAmount = fuelAmount - FUEL_BURN0;
			}
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

	void OnCollisionEnter2D(Collision2D coll){
		xVelocity = Mathf.Abs (landerRigidBody.velocity.x);
		yVelocity = Mathf.Abs (landerRigidBody.velocity.y);

		if ((xVelocity > DAMAGE_THRESHOLD || yVelocity > DAMAGE_THRESHOLD) && healthAmount > 0) {
			healthAmount = healthAmount - DAMAGE_MULTIPLIER * (xVelocity + yVelocity);
			if (healthAmount <= 0)
				healthAmount = 0; //game loss sequence should occur
		}
	}


	void OnCollisionStay2D(Collision2D coll)
	{
		if(coll.gameObject.tag == "winningArea") {

			if(landerRigidBody.velocity == new Vector2(0,0)){
				Debug.Log ("Win");
			}
			else
				Debug.Log ("Not Landed");
		}
	}

	



	
	
}