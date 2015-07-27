using UnityEngine;
using System.Collections;

// Import Text library
using UnityEngine.UI;

public class FinishAreaCollider : MonoBehaviour {

	public Text gameText;
	public float maxLandingSpeed = 1f;
	public AudioSource victoryAudioSource;

	private bool didFinish = false;
	private bool didStop = false;
	private bool isInsideTriggerBox = false;

	private Collider2D triggerCollider;

	void OnTriggerEnter2D(Collider2D other) {
		isInsideTriggerBox = true;
		triggerCollider = other;
	}

	void OnTriggerExit2D(Collider2D other) {
		isInsideTriggerBox = false;		
	}

	void FixedUpdate()
	{


		if(isInsideTriggerBox && !didFinish)
		{
			// Check if the Lander stopped.
			float colliderVelocity = triggerCollider.attachedRigidbody.velocity.magnitude;

			Debug.Log("Vel:" + colliderVelocity);


			// The Lander must completely stop or slow down enough in order to properly land on the landing pad
			didStop = (colliderVelocity - maxLandingSpeed) <= 0;


			if(didStop)
			{
				didFinish = true;
				didStop = true;

				if(victoryAudioSource != null)
				{
					victoryAudioSource.Play();
					//victorySound.play();
					//AudioSource.PlayClipAtPoint(victorySound, this.transform.position);
				}

				Transform fireworks = this.transform.parent.Find("FireworksEffect");
				
				Debug.Log ("Fireworks: " + fireworks);
				if(fireworks != null)
					fireworks.gameObject.SetActive (true);
				
				//GameManager.LoadNextLevel();
				
				
				
				// Change GameText over time
				CountDown3(); 
				Invoke("CountDown2", 1); 
				Invoke("CountDown1", 2); 
				Invoke("NextLevel", 3); 
				
			}
		}


	}

	
	void CountDown3()
	{
		gameText.gameObject.SetActive (true);
		gameText.text = "NEXT STAGE IN 3..";
	}
	void CountDown2()
	{
		gameText.text = "NEXT STAGE IN 2..";
	}
	void CountDown1()
	{
		gameText.text = "NEXT STAGE IN 1..";
	}
	void NextLevel()
	{
		gameText.text = "LOADING..";
		GameManager.LoadNextLevel();
	}


}
