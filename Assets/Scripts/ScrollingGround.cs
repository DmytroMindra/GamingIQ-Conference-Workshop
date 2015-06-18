using UnityEngine;
using System.Collections;

public class ScrollingGround : MonoBehaviour {

	public float scrollSpeed = 0.02f;
	Renderer renderer;

	void Start()
	{
		renderer = GetComponent<Renderer> ();
	}

	void Update()
	{
		float offset = Time.time * scrollSpeed;
		renderer.material.mainTextureOffset = new Vector2(offset % 1, 0);
	}

}
