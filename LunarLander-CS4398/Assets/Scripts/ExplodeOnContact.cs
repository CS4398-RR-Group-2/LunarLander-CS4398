using UnityEngine;
using System.Collections;

public class ExplodeOnContact : MonoBehaviour {
	
	public GameObject explosion;
	public float explosionLifetime = 0.5f;
	public AudioClip explosionSound;

	// Use this for initialization
	private GameObject lander;
	
	// Use this for initialization
	void Start () {
		lander = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {	

		if (other.gameObject != lander)
			return;

		// Spawn Object
		GameObject newMeteor = (GameObject) Instantiate (explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);
		// Destroy the meteor after it's lifetime ends
		Destroy(newMeteor, explosionLifetime); 
		Destroy (this.gameObject);
	}



	void OnCollisionEnter2D(Collision2D other) {		
		
		if (other.gameObject != lander)
			return;


		// Spawn Object
		GameObject newMeteor = (GameObject) Instantiate (explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);
		// Destroy the meteor after it's lifetime ends
		PlayExplosionSound ();
		Destroy(newMeteor, explosionLifetime); 
		Destroy (this.gameObject);

	}


	void PlayExplosionSound()
	{
		if(explosionSound != null)
		{
			//explosionSound.Play();
			PlayClipAt(explosionSound, new Vector3(0,0,-100));
		}
	}

	AudioSource PlayClipAt(AudioClip clip, Vector3 pos){
		GameObject tempGO = new GameObject("TempAudio"); // create the temp object
		tempGO.transform.position = pos; // set its position
		AudioSource aSource = tempGO.AddComponent<AudioSource>(); // add an audio source
		aSource.clip = clip; // define the clip
		// set other aSource properties here, if desired
		aSource.volume = 0.02f;
		aSource.Play(); // start the sound
		Destroy(tempGO, clip.length); // destroy object after clip duration
		return aSource; // return the AudioSource reference
	}

}
