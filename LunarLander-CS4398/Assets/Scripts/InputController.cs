using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputController : MonoBehaviour {

	static public InputField input;
	// Use this for initialization
	void Start () {
		input.GetComponent<InputField> ();
		input.gameObject.SetActive (false);
	}
	

	}
