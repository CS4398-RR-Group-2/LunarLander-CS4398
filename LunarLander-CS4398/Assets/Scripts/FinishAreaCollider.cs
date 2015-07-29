using UnityEngine;
using System.Collections;

// Import Text library
using UnityEngine.UI;

public class FinishAreaCollider : MonoBehaviour 
{
	public Text gameText;
	public float maxLandingSpeed = 1f;
	public AudioSource victoryAudioSource;
	public bool isLastLevel = false;
	public float playerScore = 0;			

	private bool didFinish = false;
	private bool didStop = false;
	private bool isInsideTriggerBox = false;

	private Collider2D triggerCollider;

	void OnTriggerEnter2D(Collider2D other) 
	{
		isInsideTriggerBox = true;
		triggerCollider = other;
	}

	void OnTriggerExit2D(Collider2D other) 
	{
		isInsideTriggerBox = false;		
	}

	void FixedUpdate()
	{
<<<<<<< HEAD

=======
>>>>>>> origin/master
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
				int finalLevelScore;
				finalLevelScore = (int)Mathf.Ceil((float)(triggerCollider.gameObject.GetComponent<LanderControllerScript>().fuelAmount * .02));
				ScoreManager.AddScore(finalLevelScore);


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

				// Change GameText over time (to next level)
				if(isLastLevel == false)
				{
					CountDown3(); 
					Invoke("CountDown2", 2); 
					Invoke("CountDown1", 4); 
					Invoke("NextLevel", 6); 
				}
				// Change GameText over time (end of game)
				else
				{
					EndOfGameText1();
					Invoke("EndOfGameText2", 3);
					Invoke("EndOfGameText3", 6);
					Invoke("NextLevel", 9); 
				}
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

	void EndOfGameText1()
	{
		gameText.gameObject.SetActive (true);
		gameText.text = " Congratulations";
	}

	void EndOfGameText2()
	{
		gameText.text = "Your score is...";
	}

	void EndOfGameText3()
	{
		gameText.text = playerScore.ToString(); 
	}

	void NextLevel()
	{
<<<<<<< HEAD
		if (isLastLevel) {
			ScoreManager.FinalScore();
=======
		if (isLastLevel) 
		{
>>>>>>> origin/master
			GameManager.LoadLevel(5);
		} 
		else 
		{
			gameText.text = "LOADING..";
			GameManager.LoadNextLevel ();
		}
	}
}
