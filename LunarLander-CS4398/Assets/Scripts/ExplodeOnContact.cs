using UnityEngine;
using System.Collections;

public class ExplodeOnContact : MonoBehaviour {

	public GameObject explosion;
	public float explosionLifetime = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {		
		// Spawn Object
		GameObject newMeteor = (GameObject) Instantiate (explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);
		// Destroy the meteor after it's lifetime ends
		Destroy(newMeteor, explosionLifetime); 

		Destroy (this.gameObject);
	}

}
