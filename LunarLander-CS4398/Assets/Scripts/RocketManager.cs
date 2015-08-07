/* RocketManager.cs
 * 
 * This class is used to toggle on or off the rockets and to control their
 * behavior and destruction (Some items are labled as meteors but that is 
 * due to code re-use).
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/ 
using UnityEngine;
using System.Collections;

/// <summary>
/// This class is used to toggle on or off the rockets and to control their
/// behavior and destruction.  
/// </summary>
public class RocketManager : MonoBehaviour
{
	/// <summary>
	/// A Transform variable which represents the lander's position.
	/// </summary>
	public Transform spawnTarget;
	
	// The type of Game Object that will be spawned.
	/// <summary>
	/// A GameObject variable which represents the type of game object that
	/// will be spawned (i.e. Meteor, Rocket, Health/Fuel Barrel, etc.). 
	/// </summary>
	public GameObject spawnObject;
	
	/// <summary>
	/// A float variable which represents the initial velocity of a meteor
	/// </summary>
	public float initialVelocity = 3f;

	/// <summary>
	/// A float variable which represents the range of randomness an item
	/// is allowed. 
	/// </summary>
	public float randomness = 0.2f;

	/// <summary>
	/// A float which represents the total number of meteors that can exist 
	/// at once. Once that number is reached the oldest rocket is destroyed.
	/// </summary>
	public float meteorLifeTime = 10f;
	
	/// <summary>
	/// A float representing the amount of time between the spawning of
	/// rockets. 
	/// </summary>
	public float spawnTime = 1f;
	
	/// <summary>
	/// A float variable representing where on the Y-axis the rocket will be
	/// instantiated (outside of camera view). 
	/// </summary>
	private float spawnHeight = 15f;

	/// <summary>
	/// A float variable representing where on the X-axis the rocket will be 
	/// spawned (0 - 10 units to the left or right of the lander).
	/// </summary>
	private float spawnWidth = 20f;

	/// <summary>
	/// A boolean variable used to determine if the meteors are activated or
	/// disabled.
	/// </summary>
	private bool disableMeteors = true;

	/// <summary>
	/// This method is used to toggle on or off the rockets.
	/// </summary>
	void Update()
	{
		if (Input.GetKeyDown ("y")) 
		{			
			disableMeteors = !disableMeteors;	
			return;
		}
	}
	
	// Use this for initialization
	/// <summary>
	/// This method is used to spawn a new rocket. The time between
	/// the rockets being spawned is controled by spawnTime.
	/// </summary>
	void Start () 
	{	
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	/// <summary>
	/// This method is used to spawn a rocket and sets where in the 
	/// game world the rocket will be instantiated. It also controls the 
	/// rockets behavior. 
	/// </summary>
	void Spawn()
	{
		if(disableMeteors)
		{
			return;
		}
		
		// Meteors will be spawned outside the view of the player, and they will be aimed toward the
		// player with some randomness. The Y position is set above the camera's visible range,
		// and the X position will be randomly selected from either the left or right.
		float spawnY = spawnTarget.position.y + spawnHeight;
		float spawnX = Random.Range(spawnTarget.position.x - spawnWidth, spawnTarget.position.x + spawnWidth);

		// New Spawning Code
		int direction = (int)(Random.Range (0, 4));
		
		switch (direction) 
		{	
			// Spawn From Left
			case 0:
				spawnX = spawnTarget.position.x - spawnWidth;
				spawnY = Random.Range(spawnTarget.position.y - spawnWidth, spawnTarget.position.y + spawnWidth);
				break;
			
			// Spawn From Right
			case 1:
				spawnX = spawnTarget.position.x + spawnWidth;
				spawnY = Random.Range(spawnTarget.position.y - spawnWidth, spawnTarget.position.y + spawnWidth);
				break;
			
			// Spawn From Top
			case 2:
				spawnY = spawnTarget.position.x + spawnWidth;
				spawnX = Random.Range(spawnTarget.position.x - spawnWidth, spawnTarget.position.x + spawnWidth);
				break;
			
			// Spawn From Bottom
			default:
				spawnY = spawnTarget.position.x - spawnWidth;
				spawnX = Random.Range(spawnTarget.position.x - spawnWidth, spawnTarget.position.x + spawnWidth);
				break;	
		}
		
		Vector2 spawnPos = spawnTarget.position;
		spawnPos.x = spawnX;
		spawnPos.y = spawnY;
		
		// Spawn Object
		GameObject newMeteor = (GameObject) Instantiate (spawnObject, spawnPos, spawnTarget.transform.rotation);
		// Destroy the meteor after it's lifetime ends
		Destroy(newMeteor, meteorLifeTime); 
		Rigidbody2D meteorRigidBody = newMeteor.GetComponent<Rigidbody2D>();

		if(meteorRigidBody != null)
		{
			Vector2 dir = spawnTarget.position - newMeteor.transform.position;
			dir.x += Random.Range(-randomness, randomness);
			dir.y += Random.Range(-randomness, randomness);
			meteorRigidBody.velocity = new Vector3(dir.x, dir.y, 0) * initialVelocity / 8f;		
		}
	}

	/// <summary>
	/// This method is used to guide the rocket towards the lander which 
	/// is its target. 
	/// </summary>
	/// <returns>The object.</returns>
	/// <param name="startingPosition">The current position of the rocket.</param>
	/// <param name="targetPosition">The current position of the rocket's
	/// target (lander).</param>
	public static Quaternion FaceObject(Vector2 startingPosition, Vector2 targetPosition)
	{
		Vector2 direction = targetPosition - startingPosition;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		return Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
