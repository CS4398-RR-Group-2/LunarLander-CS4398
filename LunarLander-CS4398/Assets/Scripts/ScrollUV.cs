using UnityEngine;
using System.Collections;

public class ScrollUV : MonoBehaviour {

	public Camera cam;
	public float scrollFactor = 1f;

	MeshRenderer meshRenderer;

	void Start()
	{
		meshRenderer = GetComponent<MeshRenderer> ();
	}

	// Update is called once per frame
	void Update () {

		Material mat = meshRenderer.material;

		Vector2 offset = mat.mainTextureOffset;
		
		offset.x = cam.transform.position.x * scrollFactor / 30;
		offset.y = cam.transform.position.y * scrollFactor / 30;

		meshRenderer.material.SetTextureOffset ("_MainTex", offset);
	}
}
