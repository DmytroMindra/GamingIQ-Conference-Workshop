using UnityEngine;
using System.Collections;

public class GameLayers : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetSpeedFactor(float factor)
	{
		foreach (Transform layer in transform) 
		{
			var movingLayer = layer.GetComponent<MovingLayer>();
			if (movingLayer) movingLayer.speedFactor = factor;

			var scrollingLayer = layer.GetComponent<ScrollingGround>();
			if (scrollingLayer) scrollingLayer.speedFactor = factor;

		}
	}

}
