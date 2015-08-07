/* MeteorManager.cs
 * 
 * This class is used to manage meteors and rockets. It can start or stop 
 * incoming meteors and rockets and controls their behavior. 
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/ 
using UnityEngine;
using System.Collections;

/// <summary>
/// This class is used to manage meteors and rockets. It can start or stop 
/// incoming meteors and rockets and controls their behavior. 
/// </summary>
public class MeteorManager : MonoBehaviour 
{
	/// <summary>
	/// A Transform variable which represents the lander's position.
	/// </summary>
	public Transform spawnTarget;

	/// <summary>
	/// A GameObject which represents the type of object that will be 
	/// spawned. (ex. Meteor, Rocket, Health/Fuel Barrel, etc.)
	/// </summary>
	public GameObject spawnObject;

	/// <summary>
	/// A float variable which represents the initial velocity of the meteor.
	/// </summary>
	public float initialVelocity = 3f;

	/// <summary>
	/// A float variable which represents the range of randomness.
	/// </summary>
	public float randomness = 0.2f;
	
	/// <summary>
	/// A float representing the meteor life time.
	/// </summary>
	public float meteorLifeTime = 10f;

	/// <summary>
	/// A float representing the time it take for a new meteor to spawn.
	/// </summary>
	public float spawnTime = 1f;
	
	/// <summary>
	/// A float variable which represents the vertical distance at which 
	/// a meteor will spawn relative to the lander (above visible camera range).	
	/// </summary>
	private float spawnHeight = 15f;

	/// <summary>
	/// A float variable which represents the horizonal distance at which
	/// a meteor will spawn relative to the lander. (0 - 10 units to the left
	 /// or right of the lander.
	/// </summary>
	private float spawnWidth = 20f;

	/// <summary>
	/// A boolean variable which is used to determine whether the meteors 
	/// are on or off.
	/// </summary>
	private bool disableMeteors = true;

	/// <summary>
	/// A boolean variable which is used to determine whether the rockets
	/// are on or off.
	/// </summary>
	private bool disableRockets = true;

	/// <summary>
	/// This method starts or stops incoming meteors and rockets at the
	/// player's input or "t" or "y".
	/// </summary>
	void Update()
	{
		if (Input.GetKeyDown ("t")) 
		{
			print ("space key was pressed");
			disableMeteors = !disableMeteors;
			return;
		}
		if (Input.GetKeyDown ("y"))
		{
			print ("space key was pressed");
			disableRockets = !disableRockets;
			return;
		}
	}

	/// <summary>
	/// This method is used to continually spawn meteors.
	/// </summary>
	void Start () 
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	/// <summary>
	/// This method is used to spawn meteors outside of the cameras Y-axis view,
	/// and meteors X-axis position is randomized (e.i. the left or right side
	/// of the lander).
	/// </summary>
	void Spawn()
	{
		if(disableMeteors)
		{
			return;
		}

		float spawnY = spawnTarget.position.y + spawnHeight;
		float spawnX = Random.Range(spawnTarget.position.x - spawnWidth, spawnTarget.position.x + spawnWidth);
		
		// New Spawning Code
		int direction = (int)(Random.Range (0, 4));
		switch (direction) 
		{
			// Spawn meteor from the landers left
			case 0:
				spawnX = spawnTarget.position.x - spawnWidth;
				spawnY = Random.Range(spawnTarget.position.y - spawnWidth, spawnTarget.position.y + spawnWidth);
				break;
			
			// Spawn meteor from the landers right
			case 1:
				spawnX = spawnTarget.position.x + spawnWidth;
				spawnY = Random.Range(spawnTarget.position.y - spawnWidth, spawnTarget.position.y + spawnWidth);
				break;
			
			// Spawn meteor from above the lander
			case 2:
				spawnY = spawnTarget.position.x + spawnWidth;
				spawnX = Random.Range(spawnTarget.position.x - spawnWidth, spawnTarget.position.x + spawnWidth);
				break;

			// Spawn meteor from the bottom of the lander
			default:
				spawnY = spawnTarget.position.x - spawnWidth;
				spawnX = Random.Range(spawnTarget.position.x - spawnWidth, spawnTarget.position.x + spawnWidth);
				break;
		}
		
		Vector2 spawnPos = spawnTarget.position;
		spawnPos.x = spawnX;
		spawnPos.y = spawnY;

		// Instantiates a meteor
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
	/// This method controls the behavior of incoming meteors.
	/// </summary>
	/// <returns>The object.</returns>
	/// <param name="startingPosition">The starting position of the meteor.</param>
	/// <param name="targetPosition">The position of the meteors's target (lander).</param>
	public static Quaternion FaceObject(Vector2 startingPosition, Vector2 targetPosition) 
	{
		Vector2 direction = targetPosition - startingPosition;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		return Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
