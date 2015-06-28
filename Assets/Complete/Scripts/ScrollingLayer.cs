using UnityEngine;
using System.Collections;

namespace Complete
{
	public class ScrollingLayer : MonoBehaviour
	{

		public float scrollSpeed;
		public float speedFactor;
		Renderer rendererComponent;
	
		// Let's cache the renderer as call to GetComponent might be expensive
		void Start ()
		{
			rendererComponent = GetComponent<Renderer> ();
		}

		// Scroll the texture
		void Update ()
		{
			float offset = rendererComponent.material.mainTextureOffset.x;
			offset += Time.deltaTime * scrollSpeed * speedFactor;
			rendererComponent.material.mainTextureOffset = new Vector2 (offset % 1, 0);
		}

	}
}