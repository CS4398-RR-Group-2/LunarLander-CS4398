/* ScrollUV.cs
 * 
 * This class is used to instantiate and control the movements of
 * the 2D camera. 
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/ 
using UnityEngine;
using System.Collections;

/// <summary>
/// This class is used to instantiate and control the movements of
/// the 2D camera. 
/// </summary>
public class ScrollUV : MonoBehaviour 
{
	/// <summary>
	/// A Camera variable representing the game's camera.
	/// </summary>
	public Camera cam;

	/// <summary>
	/// The speed at which the camera scrolls or moves.
	/// </summary>
	public float scrollFactor = 1f;

	/// <summary>
	/// A MeshRenderer variable used to add visual asthetics.
	/// </summary>
	MeshRenderer meshRenderer;

	/// <summary>
	/// Initializes the MeshRenderer variable so the player can see
	/// the object.
	/// </summary>
	void Start()
	{
		meshRenderer = GetComponent<MeshRenderer> ();
	}

	/// <summary>
	/// This method is usede to update the position of the camera.
	/// Update is called once per frame.
	/// </summary>
	void Update () 
	{
		Material mat = meshRenderer.material;
		Vector2 offset = mat.mainTextureOffset;
		
		offset.x = cam.transform.position.x * scrollFactor / 30;
		offset.y = cam.transform.position.y * scrollFactor / 30;

		meshRenderer.material.SetTextureOffset ("_MainTex", offset);
	}
}
