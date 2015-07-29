using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LeaderBoardManager : MonoBehaviour {
	
	string highscorePersonKey = "HighScorePerson";
	string highScoreKey = "HighScore";
	Text leadershipBoardUI;
	string[] leadershipBoardText;
	string displayFormat;
	int count = 0;


	// Use this for initialization
	void Start () {
		ScoreManager.FinalScore ();
		leadershipBoardUI = GetComponent<Text>();

		GetLeadershipBoard ();

	
	}

	void EnterNewName(int index){

	}

	void GetLeadershipBoard(){
		for (int i = 0; i < 10; i++) {
			leadershipBoardUI.text += PlayerPrefs.GetString (highscorePersonKey + i + 1, "XXX") + ": " + 
				(PlayerPrefs.GetInt (highScoreKey + (i + 1), 0)).ToString() + "\n";
		}

	}
	
	// Update is called once per frame
	void Update () {

	
	}
}
