/* Restarter.cs
 * 
 * This class is used to restart the current level. 
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/
using System;
using UnityEngine;

/// <summary>
/// Importing a unity 2D standard library.
/// </summary>
namespace UnityStandardAssets._2D
{
	/// <summary>
	/// This class is used to restart the current level. 
	/// </summary>
	public class Restarter : MonoBehaviour
    {
		/// <summary>
		/// Reloads the current scene. 
		/// </summary>
		/// <param name="other">A Collider2D variable.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                Application.LoadLevel(Application.loadedLevelName);
            }
        }
    }
}