/**LanderControllerScript.cs
 * 
 * 
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/ 
using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class LanderControllerScript : MonoBehaviour 
{
	/// <summary>
	/// A game object which represents the lander's thrusters igniting.
	/// </summary>
	public GameObject thrusters;

	/// <summary>
	/// A game object which represents the lander's left thruster igniting.	
	/// </summary>
	public GameObject leftThruster;

	/// <summary>
	/// A game object which represents the lander's right thruster igniting.
	/// </summary>
	public GameObject rightThruster;	

	/// <summary>
	/// An audtio source which represents the audio clip played when the any
	/// of lander's thrusters initiate.
	/// </summary>
	public AudioSource thrusterAudio;
	
	/// <summary>
	/// A variable which controls the rate at which the lander will 
	/// accelerate when the main thrusters are initiated.
	/// </summary>
	public float thrustAcceleration = 5f;

	/// <summary>
	/// A variable which controls the speed at which the lander will rotate
	/// on the Z-axis when the left or right thrusters are initiated.
	/// </summary>
	public float thrustRotation = 5f;


	/// <summary>
	/// Represents the maximum speed at which the lander may travel.  
	/// </summary>
	public float maxSpeed = 10f;

	/// <summary>
	/// The amount of fuel a lander has at the start of a level.
	/// </summary>
	public int fuelAmount = 5000; //an arbitrary amount of fuel for the lander

	/// <summary>
	/// The amount of health a lander has at the start of a level.
	/// </summary>

	public float healthAmount = 100;

	/// <summary>
	/// A boolean variable which is used to represent if the player is
	/// on the first level.
	/// </summary>
	public bool isMainMenuScreen = false;

	/// <summary>
	/// The velocity at which the lander is traveling along the X-axis.
	/// </summary>
	private float xVelocity;

	/// <summary>
	/// The velocity at which the lander is traveling along the Y-axis.
	/// </summary>
	private float yVelocity;

	/// <summary>
	/// 
	/// </summary>
	private float DAMAGE_THRESHOLD = .05f;
	private float DAMAGE_MULTIPLIER = 3f;
	private int FUEL_BURN0 = 10;
	private int FUEL_BURN1 = 1;
	private Rigidbody2D landerRigidBody;
	private SpriteRenderer landerSpriteRenderer;

	public Sprite[] landerSprites;


	/// <summary>
	/// This method is used to instatiate a lander witha rigid body. 
	/// </summary>
	void Start ()
	{
		hideThrusters ();
		landerRigidBody = GetComponent<Rigidbody2D>();
	}

	/// <summary>
	/// This function is used to update the lander every time the physics
	/// engine updates. It registers player input of "1", "2", "3", "4",
	/// "r", "Up" arrow, "Left" arrow, and "Right" arrow keys in order to 
	/// rotate the lander clockwise, counter clockwise, initiate thrust, 
	/// restart level, and level selection.
	/// </summary>
	void FixedUpdate () 
	{
		showThrusters ();
		landerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		damageHandler ();

		// Quit Game
		if (Input.GetKey("escape"))
			Application.Quit();

		// Level Select
		if (Input.GetKeyDown ("1")) 
		{			
			Application.LoadLevel("Scene01");			
			return;
		}
		else if (Input.GetKeyDown ("2")) 
		{			
			Application.LoadLevel("scene_grav_01");			
			return;
		}
		else if (Input.GetKeyDown ("3")) 
		{			
			Application.LoadLevel("scene_grav_02");			
			return;
		}
		else if (Input.GetKeyDown ("4")) 
		{			
			Application.LoadLevel("scene_grav_03");			
			return;
		}

		// Restart Level
		if (Input.GetKeyDown ("r"))
		{
			GameManager.RestartLevel();
			return;
		}

		// Lander Controls
		// Get Left/Right Input. This will be used to rotate the Lander.
		float rotate = Input.GetAxisRaw ("Horizontal");
		
		if (rotate != 0) 
		{
			// Apply Torque applies rotation to the Physics System
			if (fuelAmount > 0 && healthAmount > 0)
				applyTorque(rotate);

			// User is rotating clockwise
			if(rotate > 0)
			{
				if(fuelAmount > 0 && healthAmount > 0)
				{
					applyRotation(rotate);
					showLeftThruster();
					fuelAmount = fuelAmount - FUEL_BURN1;
				}
				else
				{
					hideLeftThruster();
				}
			}
			else
			{
				if(fuelAmount > 0 && healthAmount > 0)
				{
					applyRotation(rotate);
					showRightThruster();
					fuelAmount = fuelAmount - FUEL_BURN1;
				}
				else
				{
					hideRightThruster();
				}
			}
		} 
		else
		{
			hideLeftThruster();
			hideRightThruster();
		}
		
		// Get Up input. This will be used to accelerate the Lander.
		float thrust = Input.GetAxisRaw ("Vertical");
		if (thrust > 0f) 
		{
			if(fuelAmount > 0 && healthAmount > 0)
			{
				applyThrust(thrust);
				showThrusters();
				fuelAmount = fuelAmount - FUEL_BURN0;
			}
			else
			{
				hideThrusters();
			}
		} 
		else 
		{
			hideThrusters();
		}
	}
	
	/// <summary>
	/// Applies the thrust.
	/// </summary>
	/// <param name="thrust">Thrust.</param>
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
		startThrusterAudio();
	}
	
	void hideThrusters()
	{
		if (isMainMenuScreen)
			return;

		thrusters.SetActive(false);
		stopThrusterAudio();
	}
	
	void showLeftThruster()
	{
		leftThruster.SetActive(true);
		startThrusterAudio ();
	}
	
	void hideLeftThruster()
	{
		leftThruster.SetActive(false);
		stopThrusterAudio();
	}
	
	void showRightThruster()
	{
		rightThruster.SetActive(true);
		startThrusterAudio ();
	}
	
	void hideRightThruster()
	{
		rightThruster.SetActive(false);
		stopThrusterAudio();
	}

	//HP handler
	void OnCollisionEnter2D(Collision2D coll){


		xVelocity = Mathf.Abs (landerRigidBody.velocity.x);
		yVelocity = Mathf.Abs (landerRigidBody.velocity.y);

		if ((xVelocity > DAMAGE_THRESHOLD || yVelocity > DAMAGE_THRESHOLD) && healthAmount > 0) {
			healthAmount = healthAmount - DAMAGE_MULTIPLIER * Mathf.Pow ((xVelocity + yVelocity), 2);

			ScoreManager.SubtractScore(10);

			if (healthAmount <= 0)
				healthAmount = 0; //game loss sequence should occur
		}
	}

	/* 
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
	*/
	

	void startThrusterAudio()
	{
		// Play the sound
		if(!thrusterAudio.isPlaying && !isMainMenuScreen && thrusterAudio && thrusterAudio.isActiveAndEnabled)
			thrusterAudio.Play ();
	}

	void stopThrusterAudio()
	{
		// If any of the three thrusters is active, then stop the sound
		if(!thrusters.activeSelf && !leftThruster.activeSelf && !rightThruster.activeSelf)
			thrusterAudio.Stop();
	}

	void damageHandler()
	{
		int frame = (int)(healthAmount / (100/14));
		frame = 14 - frame;
		//Debug.Log ("Frame " + frame);

		landerSpriteRenderer.sprite = landerSprites [frame];
	}

	

}