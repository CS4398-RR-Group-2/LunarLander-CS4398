using UnityEngine;
using System.Collections;

public class FinishAreaCollider : MonoBehaviour {

	private bool didFinish = false;


	void OnTriggerEnter2D(Collider2D other) {

		if(!didFinish)
		{
			didFinish = true;

			Transform fireworks = this.transform.Find("FireworksEffect");
			
			Debug.Log ("Fireworks: " + fireworks);
			if(fireworks != null)
				fireworks.gameObject.SetActive (true);

			GameManager.LoadNextLevel();

		}


	}


}
