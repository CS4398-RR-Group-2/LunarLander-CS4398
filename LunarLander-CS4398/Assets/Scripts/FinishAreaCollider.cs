/* FinishAreaCollider.cs
 * 
 * This file is used to determine if the lander is within proximity of the target landing 
 * area. If the lander has landed upright within proximity of the target area, fireworks 
 * are shown and an audio clip is played. The player's score is visible as well as text 
 * counting down to the next level. The next level is loaded if available, otherwise the 
 * leaderboard is displayed. If the lander is not within proximity of the target area, 
 * the game continues. 
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class controls the behavior of the finish area collider and tests if the player 
/// has landed the lander upright onto the target area. If the lander is not on the target 
/// area, the game continues. If it is, the player is congratulated with fireworks, text, 
/// and their score is displayed. A countdown occurs, then the next level is loaded if 
/// available. If not and the player has a highscore the player is asked to enter their 
/// initials which are updated to the leaderboard and it is displayed regardless of player score. 
/// </summary>
public class FinishAreaCollider : MonoBehaviour 
{
	/// <summary>
	/// Holds text displayed at the end of each level.
	/// </summary>
	public Text gameText;						

	/// <summary>
	/// Represents an area used to capture user input of initials.
	/// </summary>
	public InputField inputField;				

	/// <summary>
	/// Holds 3 character player initials.
	/// </summary>
	public string initialsKey;		

	/// <summary>
	/// Holds lander maximum safe landing speed.
	/// </summary>
	public float maxLandingSpeed = 1f;			

	/// <summary>
	/// Used to represent an audio clip.
	/// </summary>
	public AudioSource victoryAudioSource;		

	/// <summary>
	/// Used to determine if game is on the last level.
	/// </summary>
	public bool isLastLevel = false;	

	/// <summary>
	/// A number used to help store the player initials.
	/// </summary>
	public int key;								

	/// <summary>
	/// Used to store player score.
	/// </summary>
	public float playerScore = 0;		

	/// <summary>
	/// Used to represent if the player has finished the level.
	/// </summary>
	private bool didFinish = false;				

	/// <summary>
	/// Used to determine if the lander is at a stop.
	/// </summary>
	private bool didStop = false;				

	/// <summary>
	/// Used to determine if the lander is within proximity of the 
	/// trigger (target area) box.
	/// </summary>
	private bool isInsideTriggerBox = false;	

	/// <summary>
	/// The trigger collider.
	/// </summary>
	private Collider2D triggerCollider;		

	public FinishAreaCollider(bool stopped, bool insideTriggerBox)
	{
		didStop = stopped;
		isInsideTriggerBox = insideTriggerBox;
	}
	
	FinishAreaCollider(){
	}

	/// <summary>
	/// Determines whether the lander has landed on the target.
	/// </summary>
	/// <param name="other"> If true it determines that the lander has landed on the target. </param>
	void OnTriggerEnter2D(Collider2D other) 
	{
		isInsideTriggerBox = true;
		triggerCollider = other;
	}

	/// <summary>
	/// Determines whether the lander has landed on the target.
	/// </summary>
	/// <param name="other"> If true it determines that the lander has not landed on the target. </param>
	void OnTriggerExit2D(Collider2D other) 
	{
		isInsideTriggerBox = false;		
	}

	/// <summary>
	/// Checks for lander safe landing, if so fireworks, audio, and gametext are witnessed by the player
	/// then the next level or leaderboard is loaded. Otherwise, the game continues.
	/// </summary>
	void FixedUpdate()
	{
		if(isInsideTriggerBox && !didFinish)
		{
			float colliderVelocity = triggerCollider.attachedRigidbody.velocity.magnitude;
			didStop = (colliderVelocity - maxLandingSpeed) <= 0;

			if(didStop)
			{
				didFinish = true;
				didStop = true;
				int finalLevelScore;

//				finalLevelScore = (int)Mathf.Ceil((float)(triggerCollider.gameObject.GetComponent<LanderControllerScript>().fuelAmount * .02));
//				ScoreManager.AddScore(finalLevelScore);

				LanderControllerScript landScript = triggerCollider.gameObject.GetComponent<LanderControllerScript>();
				if(!landScript)
					landScript = triggerCollider.gameObject.GetComponentInParent<LanderControllerScript>();

				if(!landScript)
				{
					didStop = false;
					return;
				}


				Debug.Log("TriggerCollider: " + triggerCollider);


				finalLevelScore = (int)Mathf.Ceil((float)(landScript.fuelAmount * .02));
				finalLevelScore += (int)Mathf.Ceil ((float)(landScript.healthAmount / 2));
				ScoreManager.AddScore(finalLevelScore);

				if(victoryAudioSource != null)
				{
					victoryAudioSource.Play();
				}

				Transform fireworks = this.transform.parent.Find("FireworksEffect");
				Debug.Log ("Fireworks: " + fireworks);

				if(fireworks != null)
				{
					fireworks.gameObject.SetActive (true);
				}

				if(isLastLevel == false)
				{
					CountDown3(); 
					Invoke("CountDown2", 2); 
					Invoke("CountDown1", 4); 
					Invoke("NextLevel", 6); 
				}
				else
				{
					Invoke("EndOfGameText1",0);

					if(ScoreManager.NewHighscore(ref key))
					{
						Invoke("getInitials", 3);
						Invoke("NextLevel", 15); 
					}
					else
					{
						Invoke("EndOfGameText2", 3);
						Invoke("EndOfGameText3", 6);
						Invoke("NextLevel", 9); 
					}
				}
			}
		}
	}
	
	/// <summary>
	/// Initiates function calls which mimic a countdown 1/3
	/// </summary>
	void CountDown3()
	{
		gameText.gameObject.SetActive (true);
		gameText.text = "NEXT STAGE IN 3..";
	}

	/// <summary>
	/// Continues the countdown	2/3
	/// </summary>
	void CountDown2()
	{
		gameText.text = "NEXT STAGE IN 2..";
	}

	/// <summary>
	/// Finishes the countdown 3/3
	/// </summary>
	void CountDown1()
	{
		gameText.text = "NEXT STAGE IN 1..";
	}

	/// <summary>
	/// Initiates the end of game text display 1/3
	/// </summary>
	void EndOfGameText1()
	{
		gameText.gameObject.SetActive (true);
		gameText.text = " Congratulations";
	}

	/// <summary>
	/// Continues the end of game text 2/3
	/// </summary>
	void EndOfGameText2()
	{
		gameText.text = "Your score is...";
	}

	/// <summary>
	/// Continues the end of game text 3/3
	/// </summary>
	void EndOfGameText3()
	{
		gameText.text = ScoreManager.score.ToString(); 
	}

	/// <summary>
	/// Gets the player's initials if they attain a high score.
	/// </summary>
	void getInitials()
	{
		gameText.text = "You have a new highscore, Enter initials and press enter: "; 
		inputField.gameObject.SetActive (true);
		inputField.text = "";
		Invoke ("wait", 10);
	}

	/// <summary>
	/// Loads the next level if available, otherwise the leaderboard is loaded
	/// </summary>
	void NextLevel()
	{
		if (isLastLevel)
		{
//			ScoreManager.FinalScore();
			GameManager.LoadLevel(5);
		} 
		else 
		{
			gameText.text = "";
			GameManager.LoadNextLevel ();
		}
	}


	/// <summary>
	/// Updates player initials into the leaderboard.
	/// </summary>
	void wait()
	{
		initialsKey = inputField.text;	
		Debug.Log("Key: " + initialsKey);
		PlayerPrefs.SetString((key + 1).ToString(), initialsKey);
	}

	/// <summary>
	/// Used for testing, returns if level was finished or not.
	/// </summary>
	public bool getIfFinished(){
		return didFinish;
	}
}
