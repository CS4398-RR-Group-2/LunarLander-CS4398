/* MoveTowardTarget.cs
 * 
 * This class is used to initialize the simulation of gravity onto 
 * an object and updates it position as it suffers from its pull.
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/ 
using UnityEngine;
using System.Collections;

/// <summary>
/// This class is used to initialize the simulation of gravity onto 
/// an object and updates it position as it suffers from its pull.  
/// </summary>
public class MoveTowardTarget : MonoBehaviour 
{
	/// <summary>
	/// A Rigidbody2D object variable.
	/// </summary>
	private Rigidbody2D thisRigidBody;

	/// <summary>
	/// A Transform variable.
	/// </summary>
	private Transform target;

	/// <summary>
	/// A float variable which represents the speed of an object
	/// </summary>
	public float movementSpeed = 500;

	/// <summary>
	/// This method is used to begin moving an object towards
	/// another object tha has gravity.
	/// </summary>
	void Start () 
	{
		thisRigidBody = GetComponent<Rigidbody2D>();
		Transform t = null;
		GameObject taggedObject = GameObject.FindWithTag("Player");
		if (taggedObject != null)
		{
			t = taggedObject.transform;
		}
		else
		{
			Debug.Log("Object not found");
		}
		
		if (t != null)
		{
			target = t;
		}
	}

	/// <summary>
	/// This method is used to move an object towards another object in order 
	/// to simulate gravity. FixedUpdate is called once per frame.
	/// </summary>
	void FixedUpdate () 
	{
		if (target != null) 
		{
			// Move Toward Target	
			Vector2 targetVector = target.position - this.transform.position;
			targetVector.Normalize();
			targetVector *= movementSpeed * Time.deltaTime;
			thisRigidBody.AddForce (targetVector);

			// Look at target
			Vector3 diff = target.position - transform.position;
			diff.Normalize();
			
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
		}
	}
}
