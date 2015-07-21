using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static void LoadNextLevel()
	{
		int i = Application.loadedLevel;
		Application.LoadLevel(i + 1);
	}

	public static void LoadPrevLevel()
	{
		int i = Application.loadedLevel;
		Application.LoadLevel(i - 1);
	}

	public static void LoadLevel(int levelNum)
	{
		Application.LoadLevel(levelNum);
	}

	public static void RestartLevel()
	{
		Application.LoadLevel(Application.loadedLevelName);
	}
}
