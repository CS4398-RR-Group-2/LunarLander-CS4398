using UnityEngine;
using System.Collections;

public class SpaceDebrisCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {

		Debug.Log ("COLLISION");

		if (coll.gameObject.tag == "Player") {

			// Spawn Explosion

			Destroy (gameObject);		
		}

	}
}
