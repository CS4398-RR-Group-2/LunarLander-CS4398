using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	public void LoadScene(int level)
	{
		if (level == 1) {
			ScoreManager.score = 0;
		}
		Application.LoadLevel (level);
	}
}
