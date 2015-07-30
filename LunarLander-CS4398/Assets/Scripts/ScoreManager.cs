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
			topScores [i] = PlayerPrefs.GetInt (highScoreKey + (i + 1).ToString(), 0);
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

	static private bool NewHighscoreHelper(ref int index){
		for (; index < 10; index++) {
			if (score > topScores [index]) {
				return true;
			}
		}
		return false;
	}

	static public bool NewHighscore(ref int index){

		if (scoresOnLB == 0) {
			PlayerPrefs.SetInt(highScoreKey + (index + 1).ToString(),score);
			return true;
		}
		if (NewHighscoreHelper (ref index)) {
			for(int j = scoresOnLB; j > index; j--){
				PlayerPrefs.SetInt(highScoreKey + (j+1).ToString(),PlayerPrefs.GetInt(highScoreKey + j.ToString()));
				PlayerPrefs.SetString((j+1).ToString(),PlayerPrefs.GetString(j.ToString()));
			}
			PlayerPrefs.SetInt(highScoreKey + (index + 1).ToString(),score);
			PlayerPrefs.Save();
			return true;
		} else {
			Debug.Log ("Did not get a top 10 highscore");
			return false;
		}
	}








}
