/* SpaceDebrisCollider.cs
 * 
 * This class is used to initialize the ability for there to be 
 * collisions, it checks for any occuring collisions, and destroys
 * the meteor and rocket if they impact against the lander.
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/ 
using UnityEngine;
using System.Collections;

/// <summary>
/// This class is used to initialize the ability for there to be 
/// collisions, checks for any occuring collisions, and destroys
/// the meteor and rocket if they impact against the lander.
/// </summary>
public class SpaceDebrisCollider : MonoBehaviour 
{

	// Use this for initialization
	/// <summary>
	/// This method is used to initialize the ability to produce
	/// collisions between objects.  
	/// </summary>
	void Start () 
	{
	}

	/// <summary>
	/// Update continuosly checks for collisions. Update is called 
	/// once per frame.
	/// </summary>
	void Update () 
	{
	}

	/// <summary>
	/// This method destroys the meteor or rocket that impacts against
	/// the lander. 
	/// </summary>
	/// <param name="coll">A Collision2D variable used to identify what
	/// the meteor or rocket impacts.</param>
	void OnCollisionEnter2D(Collision2D coll) 
	{
		Debug.Log ("COLLISION");

		if (coll.gameObject.tag == "Player") 
		{
			// Spawn Explosion
			Destroy (gameObject);		
		}
	}
}
