using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class ScoreManager : MonoBehaviour {


	public Text ScoreText;
	static public int score;

	// Use this for initialization
	void Start () {
		score = 0;
		ScoreText = GetComponent<Text>();
		ScoreText.text = "Score : " + score.ToString();
	
	}
	
	// Update is called once per frame
	void Update () {
		ScoreText.text = "Score : " + score.ToString();
	
	}




}
