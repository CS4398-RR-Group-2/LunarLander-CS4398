using UnityEngine;
using System.Collections;

// Import Text library
using UnityEngine.UI;

public class FinishAreaCollider : MonoBehaviour {

	public Text gameText;
	public InputField inputField;
	public string initialsKey;
	public float maxLandingSpeed = 1f;
	public AudioSource victoryAudioSource;
	public bool isLastLevel = false;
	public int key;

	public float playerScore = 0;		
	public bool isObjectiveScene = false;

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

//			Debug.Log("Vel:" + colliderVelocity);


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
				
				//GameManager.LoadNextLevel();
				
				
				
				// Change GameText over time

				if(isObjectiveScene == true)
				{
					fireworks = null;
					victoryAudioSource = null;
					ExitObjectiveScene();
					Invoke("NextLevel", 6);
				}
				// Change GameText over time (to next level)
				else if(isLastLevel == false)
				{
					CountDown3(); 
					Invoke("CountDown2", 2); 
					Invoke("CountDown1", 4); 
					Invoke("NextLevel", 6); 
				}
				else
				{
					Invoke("EndOfGameText1",0);


					if(ScoreManager.NewHighscore(ref key)){
						Invoke("getInitials",3);
						Invoke("NextLevel",12 ); 
					}else{
						Invoke("EndOfGameText2", 3);
						Invoke("EndOfGameText3", 6);
						Invoke("NextLevel", 9); 
					}
				}
			}
		}


	}

	//I think this part below would have to be changed to alert the player if they're score
	//qualifies for the top ten, if not then it should just go back to the main menu
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
		gameText.text = ScoreManager.score.ToString(); 
	}
	void getInitials()
	{
		gameText.text = "You have a new highscore, Enter your initials: "; 

		inputField.gameObject.SetActive (true);
		
		
	

		Invoke ("wait", 5);

	

	}



	void NextLevel()
	{


		if (isObjectiveScene)
		{
			GameManager.LoadLevel(1);
		}
		else if (isLastLevel)
		{
			ScoreManager.FinalScore();
			GameManager.LoadLevel(5);
		} 
		else 
		{
			gameText.text = "";
			GameManager.LoadNextLevel ();

		}

	}

	void wait(){

		initialsKey = inputField.text;
		
		Debug.Log ("Key: " + initialsKey);
		
		PlayerPrefs.SetString ((key+1).ToString (), initialsKey);
	}

	void ExitObjectiveScene()
	{
		gameText.gameObject.SetActive (true);
		gameText.text = "";
	}
/*	void ToContinue(bool isObjectiveScene)
	{
		if (isObjectiveScene == true) 
		{
			didFinish = true;
			didStop = true;
			isInsideTriggerBox = true;
		}
	}*/
}
