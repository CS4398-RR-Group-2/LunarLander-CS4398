/* HPGauge.cs
 * 
 * This class is ussed to instantiate, display, and update the lander HP gauge
 * during gameplay. The updates occur once per frame.
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/ 
using UnityEngine;
using System.Collections;

/// <summary>
/// This class is ussed to instantiate, display, and update the lander HP gauge
/// during gameplay.
/// </summary>
public class HPGauge : MonoBehaviour 
{
	/// <summary>
	/// A variable which represents an instantiation of the LanderControllerScript.
	/// </summary>
	public LanderControllerScript lander;

	/// <summary>
	/// A variable which represents the maximum HP a lander can have. 
	/// </summary>
	private float MAX_HP;

	/// <summary>
	/// The standard scale. Used to identify the size of the HP gauge.
	/// </summary>
	private float scale = 1;

	/// <summary>
	/// This method initializes and displays the HP gauge.
	/// </summary>
	void Start () 
	{
		MAX_HP = 100;
		transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);	
	}
	
	// Update is called once per frame
	/// <summary>
	/// This method is used to update the HP gauge of the lander during gameplay.
	/// Update is called once per game.
	/// </summary>
	void Update () 
	{
		scale = (lander.healthAmount / MAX_HP);
		transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
	}
}
