using UnityEngine;
using System.Collections;

public class MoveTowardTarget : MonoBehaviour {

	private Rigidbody2D thisRigidBody;
	private Transform target;
	public float movementSpeed = 500;

	// Use this for initialization
	void Start () {
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
			// do something
			target = t;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (target != null) {
		


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
