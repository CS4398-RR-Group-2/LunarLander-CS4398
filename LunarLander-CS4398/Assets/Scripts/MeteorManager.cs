using UnityEngine;
using System.Collections;

public class MeteorManager : MonoBehaviour {

	// A reference to the Lander's position
	public Transform spawnTarget;

	// The type of Game Object that will be spawned. (ex. Meteor, Rocket, Health/Fuel Barrel, etc.)
	public GameObject spawnObject;

	// The initial velocity of the Meteor
	public float initialVelocity = 3f;

	public float randomness = 0.2f;

	// The total number of meteors that can exist at once. If there are too many, then the oldest meteor
	// will be destroyed.
	public float meteorLifeTime = 10f;

	// A new meteor will spawn every few seconds.
	public float spawnTime = 1f;
	
	// Spawn meteors 10 units above the lander. This should be above the camera's visible range.
	private float spawnHeight = 15f;

	// Spawn meteors from 0 - 10 units to the left or right of the lander.
	private float spawnWidth = 20f;



	// Use this for initialization
	void Start () {

		// Constantly call the spawn function every few seconds
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Spawn()
	{
		// Meteors will be spawned outside the view of the player, and they will be aimed toward the
		// player with some randomness. The Y position is set above the camera's visible range,
		// and the X position will be randomly selected from either the left or right.
		float spawnY = spawnTarget.position.y + spawnHeight;
		float spawnX = Random.Range(spawnTarget.position.x - spawnWidth, spawnTarget.position.x + spawnWidth);

	

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


			
			//Debug.Log("Meteor Angle = " + angle);

			//newMeteor.transform.rotation = MeteorManager.FaceObject(meteorRigidBody.position, spawnTarget.position);
			//meteorRigidBody.AddForce(dir * initialVelocity * 10);

		}
	}

	public static Quaternion FaceObject(Vector2 startingPosition, Vector2 targetPosition) {
		Vector2 direction = targetPosition - startingPosition;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		return Quaternion.AngleAxis(angle, Vector3.forward);
	}

}
