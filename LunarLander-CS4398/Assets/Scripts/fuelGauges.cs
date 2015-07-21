using UnityEngine;
using System.Collections;

public class fuelGauges : MonoBehaviour {

	public LanderControllerScript lander;
	private float MAX_FUEL;
	private float scale = 1;
	private Sprite fuelIndicator;

	// Use this for initialization
	void Start () {
		MAX_FUEL = 5000;
		fuelIndicator = GetComponent<Sprite>();
		transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
	
	}
	
	// Update is called once per frame
	void Update () {
		scale = (lander.fuelAmount / MAX_FUEL);
		transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
	
	}
}
