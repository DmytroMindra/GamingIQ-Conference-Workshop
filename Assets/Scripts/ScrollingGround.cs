using UnityEngine;
using System.Collections;

public class ScrollingGround : MonoBehaviour {

	public float scrollSpeed;
	public float speedFactor;

	Renderer renderer;


	void Start()
	{
		renderer = GetComponent<Renderer> ();
	}

	void Update()
	{
		float offset = renderer.material.mainTextureOffset.x;
		offset += Time.deltaTime * scrollSpeed * speedFactor;
		renderer.material.mainTextureOffset = new Vector2(offset % 1, 0);
	}

}
