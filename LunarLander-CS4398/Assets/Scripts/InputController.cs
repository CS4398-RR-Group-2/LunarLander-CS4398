/* InputController.cs
 * 
 * This class is used to create and initialize an input field.
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/ 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This class is used to create and initialize an input field.
/// </summary>
public class InputController : MonoBehaviour 
{
	/// <summary>
	/// Creation of an InputField variable.
	/// </summary>
	static public InputField input;

	/// <summary>
	/// Initialization of the InputField above variable.
	/// </summary>
	void Start () 
	{
		input.GetComponent<InputField> ();
		input.gameObject.SetActive (false);
	}
}
