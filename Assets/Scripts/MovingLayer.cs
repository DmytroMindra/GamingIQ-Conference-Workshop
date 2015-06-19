using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingLayer : MonoBehaviour {

	public float layerSpeed;
	public float layerDelay;

	public bool pause;


	public float spawnAtPosition;
	public float resetAtPosition;
	public float fragmentLength;
	public int fragmentBuffer;


	public List<Transform> fragmentPool;
	public List<Transform> activeFragments;
	public Transform lastSpawnedFragment;


	// Use this for initialization
	void Start () {

		foreach (Transform child in transform) 
		{
			child.gameObject.SetActive(false);
			fragmentPool.Add(child);
		}

		SpawnFragment ();
	}

	void SpawnFragment()
	{
		int fragmentCount = fragmentPool.Count;

		if (fragmentCount == 0)
			return;

		int randomFragment = Random.Range (0, fragmentCount);
		var fragment = fragmentPool [randomFragment];
		fragmentPool.Remove (fragment);
		activeFragments.Add (fragment);
		lastSpawnedFragment = fragment;
		fragment.transform.position = new Vector3 (spawnAtPosition, fragment.transform.position.y, 0);
		fragment.gameObject.SetActive(true);
	}

	void RemoveFragment(Transform fragment)
	{
		fragment.gameObject.SetActive (false);
		activeFragments.Remove (fragment);
		fragmentPool.Add (fragment);
	}
	
	void Update () 
	{

		if (pause)
			return;

		if (Time.time < layerDelay)
			return;

		if (lastSpawnedFragment.position.x + fragmentLength<spawnAtPosition) {
			SpawnFragment ();	
		}

		float dx = -Time.deltaTime * layerSpeed;

		Transform fragmentToRemove = null;

		foreach (Transform fragment in activeFragments) 
		{
			fragment.Translate(dx,0,0);
			if (fragment.position.x<resetAtPosition)
				fragmentToRemove = fragment;
		}

		if (fragmentToRemove) 
			RemoveFragment(fragmentToRemove);

	}

	public void AddObject(Transform transform)
	{

	}

	public void RemoveObject(Transform transform)
	{
		
	}


}
