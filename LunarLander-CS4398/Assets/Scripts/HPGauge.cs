using UnityEngine;
using System.Collections;

public class HPGauge : MonoBehaviour {

	public LanderControllerScript lander;
	private float MAX_HP;
	private float scale = 1;
	//private Sprite healthIndicator;
	
	// Use this for initialization
	void Start () {
		MAX_HP = 100;
		//healthIndicator = GetComponent<Sprite>();
		transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
		
	}
	
	// Update is called once per frame
	void Update () {
		scale = (lander.healthAmount / MAX_HP);
		transform.localScale = new Vector3(scale, transform.localScale.y, transform.localScale.z);
		
	}
}
