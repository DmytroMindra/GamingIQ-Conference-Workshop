using UnityEngine;
using System.Collections;

namespace Complete
{
	public class GameLayers : MonoBehaviour
	{

		// Update speed for nested layers

		public void SetSpeedFactor (float factor)
		{
			foreach (Transform layer in transform) {
				var movingLayer = layer.GetComponent<MovingLayer> ();
				if (movingLayer)
					movingLayer.speedFactor = factor;

				var scrollingLayer = layer.GetComponent<ScrollingLayer> ();
				if (scrollingLayer)
					scrollingLayer.speedFactor = factor;

			}
		}

	}
}