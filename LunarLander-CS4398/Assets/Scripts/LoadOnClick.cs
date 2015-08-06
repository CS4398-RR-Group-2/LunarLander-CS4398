/* LoadOnClick.cs
 * 
 * This class is used to load a new scene when a player presses a GUI
 * button.
 *
 * This file is to be used as a script for LunarLander-CS4398
*/
using UnityEngine;
using System.Collections;

/// <summary>
/// This class is used to load a new scene upon the player press of  a
/// GUI button. 
/// </summary>
public class LoadOnClick : MonoBehaviour 
{
	/// <summary>
	/// Loads the scene selected by the player.
	/// </summary>
	/// <param name="level">The numbered level or scene the player 
	/// wants the game to load upon GUI button press.</param>
	public void LoadScene(int level)
	{
		if (level == 1) {
			ScoreManager.score = 0;
		}
		Application.LoadLevel (level);
	}
}