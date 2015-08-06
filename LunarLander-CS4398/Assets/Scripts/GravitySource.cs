/* GravitySource.cs
 * 
 * This class is used to instantiate a gravity source, and apply that gravity
 * to another object tagged by the class in gravitateSource.cs. 
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/ 
using UnityEngine;
using System.Collections;

/// <summary>
/// This class is used to instantiate a gravity source, and apply that gravity
/// to another object tagged by the class in gravitateSource.cs. 
/// </summary>
public class GravitySource : MonoBehaviour 
{

	/// <summary>
	/// A Rigidbody2D variable which represents a body with gravity.
	/// </summary>
	private Rigidbody2D thisRigidBody;

	/// <summary>
	/// Initializes a body with gravity.
	/// </summary>
	void Start ()
	{
		thisRigidBody = GetComponent<Rigidbody2D>();
		if(thisRigidBody)
		{
			// Disable gravity and movement for the planetoid
			thisRigidBody.isKinematic = true;
		}
	}
	
	/// <summary>
	/// This method gets a list of objects which gravity is to be applied to
	/// and applies the force once every update via the physics engine.
	/// </summary>
	void FixedUpdate () 
	{
		// Get list of objects with GravitateSource Component
		GravitateSource[] gravObjects  = (GravitateSource[])FindObjectsOfType (typeof(GravitateSource));
		for (int i = 0; i < gravObjects.Length; i++) 
		{	
			// Get rigidbody2d component from object and apply a force on the object.
			Rigidbody2D rb = gravObjects[i].gameObject.GetComponent<Rigidbody2D>();

			if(rb != null)
			{
				ApplyGravitationalForce(rb);
			}
		}
	}

	/// <summary>
	/// Applies the gravitational force to a body. The amount of force an item suffers
	/// is determined by the variables distance, dir, and thisMass (the size of the 
	/// object that is the gravity source.   
	/// </summary>
	/// <param name="rigidBody">A Rigidbody2D variable which represents the object 
	/// which is to be pulled by gravity.</param>
	void ApplyGravitationalForce(Rigidbody2D rigidBody)
	{
		Vector2 dir = this.transform.position - rigidBody.gameObject.transform.position;

		float thisMass = thisRigidBody.mass;
		float distance = getDistance (this.transform, rigidBody.transform);

		rigidBody.AddForce (dir * thisMass / (distance));
	}

	/// <summary>
	/// This method is used to get the distance on object is from another object. 
	/// </summary>
	/// <returns>The distance from one object to another as a float.</returns>
	/// <param name="t1">An object.</param>
	/// <param name="t2">A second object.</param>
	float getDistance(Transform t1, Transform t2)
	{
		Vector2 heading = t1.position - t2.position;
		float distance = heading.magnitude;
		return distance;
	}
}


