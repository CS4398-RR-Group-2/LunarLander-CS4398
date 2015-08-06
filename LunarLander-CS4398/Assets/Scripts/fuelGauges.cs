/* fuelgauges.cs
 * 
 * This class is used to instantiate the lander's fuel gauge and update it
 * until all fuel is depleated. 
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/ 
using UnityEngine;
using System.Collections;

/// <summary>
/// This class is used to instantiate the lander's fuel gauge and update it
/// when fuel is depleated. 
/// </summary>
public class fuelGauges : MonoBehaviour 
{
	/// <summary>
	/// Instantiates a lander using the LanderControllerScript.
	/// </summary>
	public LanderControllerScript lander;

	/// <summary>
	/// Represents the landers maximum amount of fuel.
	/// </summary>
	private float MAX_FUEL;

	/// <summary>
	/// Represents the beginning amount of the fuel gauge that is filled in.
	/// </summary>
	private float scale = 1;

	/// <summary>
	/// Initializes the fuel gauge for a lander. 
	/// </summary>
	void Start () 
	{
		MAX_FUEL = 5000;
		transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
	}
	
	/// <summary>
	/// Is used to update the amount of the fuel gauge that is filled in.
	/// Update is called once per frame.
	/// </summary>
	void Update () 
	{
		scale = (lander.fuelAmount / MAX_FUEL);
		transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
	}
}
