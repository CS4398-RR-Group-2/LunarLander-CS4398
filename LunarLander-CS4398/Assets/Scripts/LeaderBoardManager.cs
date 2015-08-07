/* LeaderBoardManager.cs
 * 
 * This class instantiates the leaderboard and retreives it for viewing
 * by the player.
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/ 
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class instantiates the leaderboard and retreives it for viewing
/// by the player.
/// </summary>
public class LeaderBoardManager : MonoBehaviour 
{
	/// <summary>
	/// A string which represents a key and where to store player's score.
	/// </summary>
	string highScoreKey = "HighScore";

	/// <summary>
	/// A Text variable representing leaderboard user interface.
	/// </summary>
	Text leadershipBoardUI;

	/// <summary>
	/// An array of strings which represents the leadershipboard text. 
	/// </summary>
	string[] leadershipBoardText;

	/// <summary>
	/// A string which represents the player's input. 
	/// </summary>
	string userInput;

	/// <summary>
	/// This method instantiates the leaderboard.
	/// </summary>
	void Start ()
	{
		//PlayerPrefs.DeleteAll ();
		leadershipBoardUI = GetComponent<Text>();
		leadershipBoardUI.text = ""; 
		GetLeadershipBoard ();
	}
	
	/// <summary>
	/// This method retreives the leaderboard for player viewing. 
	/// </summary>
	void GetLeadershipBoard()
	{
		for (int i = 0; i < 10; i++) 
		{
			leadershipBoardUI.text += PlayerPrefs.GetString ((i + 1).ToString(), "XXX") + ": " + 
				(PlayerPrefs.GetInt (highScoreKey + (i + 1).ToString(), 0)).ToString() + "\n";
		}
	}
}