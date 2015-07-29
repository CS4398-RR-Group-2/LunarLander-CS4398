using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public  class ScoreManager : MonoBehaviour {

	//public LanderControllerScript lander;
	static public int DEFAULT_NUMBER_OF_SCORES = 10;
	static public Text ScoreText;
	static public int score = 0;
	static public int[] topScores ;
	static public string highScoreKey = "HighScore";
	static public int scoresOnLB = 0;



	// Use this for initialization
	void Start () {
		topScores = new int[DEFAULT_NUMBER_OF_SCORES];
		for (int i = 0; i < DEFAULT_NUMBER_OF_SCORES; i++) {
			highScoreKey = "HighScore" + (i + 1).ToString ();
			topScores [i] = PlayerPrefs.GetInt (highScoreKey, 0);
			if(topScores[i] != 0){
				scoresOnLB++;
			}
		}


		ScoreText = GetComponent<Text>();
		ScoreText.text = "Score : " + score.ToString();
	
	}
	
	// Update is called once per frame
	void Update(){

		ScoreText.text = "Score: " + score.ToString ();
	}


	static public void AddScore(int amount){
			score += amount;
	}

	static public void SubtractScore(int amount){
		if (score - amount >= 0) {
			score -= amount;
		} else {
			score = 0;
		}
	}

	static public bool NewHighscore(ref int index){
		for (int i = 0; i < 10; i++) {
			if (score > topScores [i]) {
				return true;
			}
		}
		return false;
	}

	static public void FinalScore(){
		int index = 0;
		//LeaderBoardManager board;
		if (scoresOnLB == 0) {
			PlayerPrefs.SetInt(highScoreKey + (index + 1).ToString(),score);
			return;
		}
		if (NewHighscore (ref index)) {
			for(int j = scoresOnLB; j > index; j--){
				PlayerPrefs.SetInt(highScoreKey + (j+1).ToString(),PlayerPrefs.GetInt(highScoreKey + j.ToString()));
			}
			PlayerPrefs.SetInt(highScoreKey + (index + 1).ToString(),score);
		} else {
			Debug.Log ("Did not get a top 10 highscore");
		}
	}






}
