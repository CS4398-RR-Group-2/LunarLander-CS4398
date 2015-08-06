/* GameManager.cs
 * 
 * This class is used to control the loading of previous and next levels. Also,
 * it loads a specific level and restarts the current level at the player's request. 
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/ 
using UnityEngine;
using System.Collections;

/// <summary>
/// This class is used to control the loading of previous and next levels.
/// Also, it loads a specific level and restarts a level at player's request.
/// </summary>
public class GameManager : MonoBehaviour 
{
	/// <summary>
	/// This method is used to load the next level in the game.
	/// </summary>
	public static void LoadNextLevel()
	{
		int i = Application.loadedLevel;
		Application.LoadLevel(i + 1);
	}

	/// <summary>
	/// This method is used to load the previous level in the game.
	/// </summary>
	public static void LoadPrevLevel()
	{
		int i = Application.loadedLevel;
		Application.LoadLevel(i - 1);
	}

	/// <summary>
	/// This method is used to load a level at player's request. 
	/// </summary>
	/// <param name="levelNum">Represents the level the player wants to be loaded.</param>
	public static void LoadLevel(int levelNum)
	{
		Application.LoadLevel(levelNum);
	}

	/// <summary>
	/// This method is used to restart the level at player's request.
	/// </summary>
	public static void RestartLevel()
	{
		Application.LoadLevel(Application.loadedLevelName);
	}
}
