using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LeaderBoardManager : MonoBehaviour {
	
	string highscorePersonKey = "HighScorePerson";
	string highScoreKey = "HighScore";
	Text leadershipBoardUI;
	string[] leadershipBoardText;
	string userInput;
	int count = 0;


	// Use this for initialization
	void Start ()
	{
		//PlayerPrefs.DeleteAll ();
		leadershipBoardUI = GetComponent<Text>();
		leadershipBoardUI.text = ""; 

		GetLeadershipBoard ();

	
	}
	

	void GetLeadershipBoard(){
		for (int i = 0; i < 10; i++) {
			leadershipBoardUI.text += PlayerPrefs.GetString ((i + 1).ToString(), "XXX") + ": " + 
				(PlayerPrefs.GetInt (highScoreKey + (i + 1).ToString(), 0)).ToString() + "\n";
		}

	}
	

}
