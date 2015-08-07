/* ScoreManager.cs
 * 
 * This class is used to keep track of the player's score, instantiate 
 * a leaderboard, and compare it to existing scores found in the 
 * leaderboard, and update the leaderboard with the new highscore if applicable.
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/ 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This class is used to keep track of the player's score, instantiate 
/// a leaderboard, and compare it to existing scores found in the 
/// leaderboard, and update the leaderboard with the new highscore if applicable.
/// </summary>
public  class ScoreManager : MonoBehaviour
{
	/// <summary>
	/// An integer variable used to keep the default number of scores
	/// to be stored in the leaderboard.	
	/// </summary>
	static public int DEFAULT_NUMBER_OF_SCORES = 10;

	/// <summary>
	/// A Text variable used to store the text displayed with the score.
	/// </summary>
	static public Text ScoreText;

	/// <summary>
	/// An integer variable used to keep track of the player's score.
	/// </summary>
	static public int score = 0;

	/// <summary>
	/// An integer array used to store the top scores.
	/// </summary>
	static public int[] topScores ;

	/// <summary>
	/// A string variable used to hold text.
	/// </summary>
	static public string highScoreKey = "HighScore";

	/// <summary>
	/// An integer variable used to represent how many player's high
	/// scores are on the leaderboard.
	/// </summary>
	static public int scoresOnLB = 0;
	
	/// <summary>
	/// This method is used for leaderboard and player score 
	/// initialization.
	/// </summary>
	void Start () 
	{
		topScores = new int[DEFAULT_NUMBER_OF_SCORES];
		for (int i = 0; i < DEFAULT_NUMBER_OF_SCORES; i++) 
		{
			topScores [i] = PlayerPrefs.GetInt (highScoreKey + (i + 1).ToString(), 0);
			if(topScores[i] != 0)
			{
				scoresOnLB++;
			}
		}
	
		ScoreText = GetComponent<Text>();
		ScoreText.text = "Score : " + score.ToString();
	}
	
	/// <summary>
	/// This method is used to displaye the player's score during
	/// gameplay. Update is called once per frame.
	/// </summary>
	void Update()
	{
		ScoreText.text = "Score: " + score.ToString ();
	}

	/// <summary>
	/// This method adds newly accrued points to the player's
	/// score after level completion.
	/// </summary>
	/// <param name="amount">An integer variable used to determine
	/// the amount of points that are to be added to the 
	/// player's score.</param>
	static public void AddScore(int amount)
	{
			score += amount;
	}

	/// <summary>
	/// This method is used to deduct points from the player's score.
	/// </summary>
	/// <param name="amount">An integer variable used to determine
	/// the amount of points that are to be deducted from the 
	/// player's score.</param>
	static public void SubtractScore(int amount)
	{
		if (score - amount >= 0) 
		{
			score -= amount;
		} 
		else 
		{
			score = 0;
		}
	}

	/// <summary>
	/// This method is used to check if the player's score is higher
	/// than a score found in the leaderboard.
	/// </summary>
	/// <returns><c>true</c>, if the player's score is higher than
	/// scores found in the leaderboard, <c>false</c> is returned 
	/// otherwise.</returns>
	/// <param name="index">An integer variable used to traverse through 
	/// the leaderboard.</param>
	static private bool NewHighscoreHelper(ref int index)
	{
		for (; index < 10; index++) 
		{
			if (score > topScores [index]) 
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// This method checks if the player has a high score, if so the 
	/// leaderboard requires updating, otherwise it does not.
	/// </summary>
	/// <returns><c>true</c>, if the player's score is a new highscore,
	/// otherwise the method returns.<c>false</c> 
	/// otherwise.</returns>
	/// <param name="index">Index.</param>
	static public bool NewHighscore(ref int index)
	{
		if (scoresOnLB == 0) 
		{
			PlayerPrefs.SetInt(highScoreKey + (index + 1).ToString(),score);
			return true;
		}
		if (NewHighscoreHelper (ref index)) 
		{
			for(int j = scoresOnLB; j > index; j--)
			{
				PlayerPrefs.SetInt(highScoreKey + (j+1).ToString(),PlayerPrefs.GetInt(highScoreKey + j.ToString()));
				PlayerPrefs.SetString((j+1).ToString(),PlayerPrefs.GetString(j.ToString()));
			}
			PlayerPrefs.SetInt(highScoreKey + (index + 1).ToString(),score);
			PlayerPrefs.Save();
			return true;
		} 
		else
		{
			Debug.Log ("Did not get a top 10 highscore");
			return false;
		}
	}
}
