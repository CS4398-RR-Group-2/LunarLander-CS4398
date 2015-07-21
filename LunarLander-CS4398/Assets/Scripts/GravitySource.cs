using UnityEngine;
using System.Collections;

public class GravitySource : MonoBehaviour {


	private Rigidbody2D thisRigidBody;

	// Use this for initialization
	void Start () {
		thisRigidBody = GetComponent<Rigidbody2D>();
		if(thisRigidBody)
		{
			// Disable gravity and movement for the planetoid
			thisRigidBody.isKinematic = true;
		}
	}
	
	// Update is called during each Physics update
	void FixedUpdate () {
	
		// Get list of objects with GravitateSource Component

		GravitateSource[] gravObjects  = (GravitateSource[])FindObjectsOfType (typeof(GravitateSource));
		for (int i=0;i<gravObjects.Length;i++) {
			
			// Get rigidbody2d component from object and apply a force on the object.
			Rigidbody2D rb = gravObjects[i].gameObject.GetComponent<Rigidbody2D>();

			if(rb != null)
			{
				ApplyGravitationalForce(rb);
			}

		}
	}


	void ApplyGravitationalForce(Rigidbody2D rigidBody)
	{
		Vector2 dir = this.transform.position - rigidBody.gameObject.transform.position;

		float thisMass = thisRigidBody.mass;
		float distance = getDistance (this.transform, rigidBody.transform);

		rigidBody.AddForce (dir * thisMass / (distance));
	}

	float getDistance(Transform t1, Transform t2)
	{
		Vector2 heading = t1.position - t2.position;
		float distance = heading.magnitude;
		return distance;
	}



}


