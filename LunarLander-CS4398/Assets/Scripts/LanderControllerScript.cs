/**LanderControllerScript.cs
 * 
 * This class defines the controls and behavior of the lander. It hides and displays 
 * the lander's thrusters when initiated and plays audio. Also, health and fuel are 
 * set and their depletion is controlled. A damaged lander sprite is displayed 
 * when the lander takes damage. The lander itself is instantiated. 
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/ 
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class defines the controls and behavior of the lander. It hides and displays 
/// the lander's thrusters when initiated and plays audio. Also, health and fuel are 
/// set and their depletion is controlled. A damaged lander sprite is displayed 
/// when the lander takes damage. The lander itself is instantiated. 
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
	/// The total amount of damage a lander can take before becoming inoperable.
	/// </summary>
	private float DAMAGE_THRESHOLD = .05f;

	/// <summary>
	/// A float used to multiply the damage taken by the lander when it collides
	/// against another object. 
	/// </summary>
	private float DAMAGE_MULTIPLIER = 3f;

	/// <summary>
	/// The amount of fuel used when a burst from the lander's thrusters is initiated.
	/// </summary>
	private int FUEL_BURN0 = 10;

	/// <summary>
	/// The amount of fuel used when a burst from the lander's left or right thrusters
	/// are initiated. 
	/// </summary>
	private int FUEL_BURN1 = 1;

	/// <summary>
	/// A Rigidbody2D variable which represents a the body of a lander. 
	/// </summary>
	private Rigidbody2D landerRigidBody;

	/// <summary>
	/// A SpriteRenderer variable which represents a image of a lander to be displayed. 
	/// </summary>
	private SpriteRenderer landerSpriteRenderer;
	private bool thrustersOn;

	/// <summary>
	/// An array containing Sprites which represent images of a lander to be displayed.
	/// </summary>
	public Sprite[] landerSprites;

	/// <summary>
	/// Constructor method used for unit testing
	/// </summary>
	public LanderControllerScript(int a, float b)
	{
		fuelAmount = a;
		healthAmount = b;
	}
	
	LanderControllerScript(){
	}


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
					depleteFuel(FUEL_BURN1);
					//fuelAmount = fuelAmount - FUEL_BURN1;
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
					depleteFuel(FUEL_BURN1);
					//fuelAmount = fuelAmount - FUEL_BURN1;
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
				depleteFuel(FUEL_BURN0);
				//fuelAmount = fuelAmount - FUEL_BURN0;
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
	}

	/// <summary>
	/// Applies rotation to the lander (clockwise counter clockwise).
	/// </summary>
	/// <param name="rotation">Rotation.</param>
	void applyRotation(float rotation)
	{
		float initialRotation = transform.rotation.eulerAngles.z;
		float finalRotation = initialRotation + (-rotation * thrustRotation);
		transform.rotation = Quaternion.Lerp ( transform.rotation, Quaternion.Euler(0,0,finalRotation), Time.deltaTime*thrustRotation);
	}

	/// <summary>
	/// Applies the torque to the lander.
	/// </summary>
	/// <param name="rotation">A float vairable which represents the angle or rotation of the lander.</param>
	void applyTorque(float rotation)
	{
		landerRigidBody.AddTorque(-rotation * thrustRotation/10);
	}

	/// <summary>
	/// This method displays the lander thrusters when thrust is initiated.
	/// </summary>
	void showThrusters()
	{
		thrusters.SetActive(true);
		startThrusterAudio();
		thrustersOn = true;
	}

	/// <summary>
	/// This method hides the lander thrusters.
	/// </summary>
	void hideThrusters()
	{
		if (isMainMenuScreen)
			return;

		thrusters.SetActive(false);
		stopThrusterAudio();
		thrustersOn = false;
	}

	/// <summary>
	/// This method displays the left lander thruster when rotating left. 
	/// </summary>
	void showLeftThruster()
	{
		leftThruster.SetActive(true);
		startThrusterAudio ();
		thrustersOn = true;
	}

	/// <summary>
	/// This method hides the left lander thruster. 
	/// </summary>
	void hideLeftThruster()
	{
		leftThruster.SetActive(false);
		stopThrusterAudio();
		thrustersOn = true;
	}

	/// <summary>
	/// This method displays the right lander thruster when rotating right.
	/// </summary>
	void showRightThruster()
	{
		rightThruster.SetActive(true);
		startThrusterAudio ();
		thrustersOn = true;
	}

	/// <summary>
	/// This method hides the right lander thruster. 
	/// </summary>
	void hideRightThruster()
	{
		rightThruster.SetActive(false);
		stopThrusterAudio();
		thrustersOn = false;
	}

	//HP handler
	/// <summary>
	/// Controls the amount of damage taken by the lander when an impact occurs
	/// against another object. 
	/// </summary>
	/// <param name="coll">A collision2D variable which represents that a collision
	/// has occured with the lander and another object.</param>
	void OnCollisionEnter2D(Collision2D coll)
	{
		xVelocity = Mathf.Abs (landerRigidBody.velocity.x);
		yVelocity = Mathf.Abs (landerRigidBody.velocity.y);

<<<<<<< HEAD
		if ((xVelocity > DAMAGE_THRESHOLD || yVelocity > DAMAGE_THRESHOLD) && healthAmount > 0) 
		{
			healthAmount = healthAmount - DAMAGE_MULTIPLIER * Mathf.Pow ((xVelocity + yVelocity), 2);
=======
		if ((xVelocity > DAMAGE_THRESHOLD || yVelocity > DAMAGE_THRESHOLD) && healthAmount > 0) {
			depleteHealth(xVelocity,yVelocity,DAMAGE_MULTIPLIER);
			//healthAmount = healthAmount - DAMAGE_MULTIPLIER * Mathf.Pow ((xVelocity + yVelocity), 2);

>>>>>>> origin/master
			ScoreManager.SubtractScore(10);

			if (healthAmount <= 0)
			{
				healthAmount = 0; 
				//game loss sequence should occur
			}
		}
	}

	/// <summary>
	/// Begins to play the audio clip for lander thrusters.
	/// </summary>
	void startThrusterAudio()
	{
		if(!thrusterAudio.isPlaying && !isMainMenuScreen && thrusterAudio && thrusterAudio.isActiveAndEnabled)
			thrusterAudio.Play ();
	}

	/// <summary>
	/// Stops playing the audio clip for the lander thrusters. 
	/// </summary>
	void stopThrusterAudio()
	{
		if(!thrusters.activeSelf && !leftThruster.activeSelf && !rightThruster.activeSelf)
			thrusterAudio.Stop();
	}

	/// <summary>
	/// Changes the sprite rendered for the lander depending on the amount of HP 
	/// the lander has. The less HP the more damaged the lander sprite looks.
	/// </summary>
	void damageHandler()
	{
		int frame = (int)(healthAmount / (100/14));
		frame = 14 - frame;
		landerSpriteRenderer.sprite = landerSprites [frame];
	}
<<<<<<< HEAD
=======

	/// <summary>
	/// Used to deplete fuel based on amount used, used for testing.
	/// </summary>
	public void depleteFuel(int amount)
	{
		fuelAmount = fuelAmount - amount;
	}
	
	/// <summary>
	/// Used to deplete heath based on velocity and dmgMultipler, used for testing.
	/// </summary>
	public void depleteHealth(float xVelocity, float yVelocity, float dmgMultiplier){
		healthAmount = healthAmount - dmgMultiplier * Mathf.Pow ((xVelocity + yVelocity), 2);
	}
	
	/// <summary>
	/// Used for testing, to show whether or not the lander's thrusters are active.
	/// </summary>
	public bool getThrustersStatus()
	{
		return thrustersOn;
	}

	

>>>>>>> origin/master
}