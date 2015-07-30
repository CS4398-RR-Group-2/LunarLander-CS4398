using UnityEngine;
using System.Collections;

public class MoveTowardTarget : MonoBehaviour {

	private Rigidbody2D thisRigidBody;
	public Transform target;
	public float movementSpeed = 50;

	// Use this for initialization
	void Start () {
		thisRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector2 targetVector = target.position - this.transform.position;
		targetVector *= movementSpeed * Time.deltaTime;


		thisRigidBody.AddForce (targetVector);
	}
}
