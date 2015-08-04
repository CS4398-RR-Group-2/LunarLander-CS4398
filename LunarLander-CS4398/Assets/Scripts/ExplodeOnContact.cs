/* ExplodeOnContact.cs
 * 
 * This class controls the creation and behaviors of a meteor. If a meteor impacts 
 * the lander an explosion occurs and an audio clip is played. If a meteor impacts 
 * agains something else, the meteor is destroyed and another is instantiated.
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/ 
using UnityEngine;
using System.Collections;

/// <summary>
/// This class controls the creation and behaviors of a meteor. If a meteor impacts 
/// the lander an explosion occurs and an audio clip is played. If a meteor impacts 
/// agains something else, the meteor is destroyed and another is instantiated.
/// </summary>
public class ExplodeOnContact : MonoBehaviour 
{
	/// <summary>
	/// An explosion game object.
	/// </summary>
	public GameObject explosion;

	/// <summary>
	/// Determines the lifetime of an explosion game object.
	/// </summary>
	public float explosionLifetime = 0.5f;

	/// <summary>
	/// An audio clip used for the sound of an explosion.
	/// </summary>
	public AudioClip explosionSound;

	/// <summary>
	/// A lander game object.
	/// </summary>
	private GameObject lander;
	
	/// <summary>
	/// Used for initialization of the lander.
	/// </summary>
	void Start () 
	{
		lander = GameObject.FindWithTag("Player");
	}
	
	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	void Update () 
	{}

	/// <summary>
	/// Creates a meteor and after it collides against something that is not the 
	/// lander waits then the meteor game object is destroyed. 
	/// </summary>
	/// <param name="other">Is a game object whose identity must be checked.</param>
	void OnTriggerEnter2D(Collider2D other) 
	{	
		if (other.gameObject != lander)
			return;

		// instantiate a new object
		GameObject newMeteor = (GameObject) Instantiate (explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);
		// destroy the meteor object after it's lifetime ends
		Destroy(newMeteor, explosionLifetime); 
		Destroy (this.gameObject);
	}

	/// <summary>
	/// Creates a meteor and if it collides against the lander it creates an explosion and a 
	/// sound, then that game object is destroyed. 
	/// </summary>
	/// <param name="other">Is a game object whose identity must be checked.</param>
	void OnCollisionEnter2D(Collision2D other) 
	{			
		if (other.gameObject != lander)
			return;
		
		GameObject newMeteor = (GameObject) Instantiate (explosion, this.gameObject.transform.position, this.gameObject.transform.rotation);

		PlayExplosionSound ();
		Destroy(newMeteor, explosionLifetime); 
		Destroy (this.gameObject);
	}

	/// <summary>
	/// Initiates the play of the audio clip for an explosion.
	/// </summary>
	void PlayExplosionSound()
	{
		if(explosionSound != null)
		{
			PlayClipAt(explosionSound, new Vector3(0,0,-100));
		}
	}

	/// <summary>
	/// Plays an audio clip (param "clip") which was "emmited" from the item found 
	/// at a position in the game (param "pos").
	/// </summary>
	/// <returns>The <see cref="UnityEngine.AudioSource"/>.</returns>
	/// <param name="clip"> The audio clip to play as a sound.</param>
	/// <param name="pos"> The position of the item in the game that is "emmiting" 
	/// the sound.</param>
	AudioSource PlayClipAt(AudioClip clip, Vector3 pos)
	{
		GameObject tempGO = new GameObject("TempAudio"); 			// create the temp object
		tempGO.transform.position = pos; 							// set its position
		AudioSource aSource = tempGO.AddComponent<AudioSource>(); 	// add an audio source
		aSource.clip = clip; 										// define the clip

		// set other aSource properties here, if desired
		aSource.volume = 0.02f;										// set the volume
		aSource.Play(); 											// start the sound
		Destroy(tempGO, clip.length); 								// destroy object after clip duration
		return aSource; 											// return the AudioSource reference
	}
}