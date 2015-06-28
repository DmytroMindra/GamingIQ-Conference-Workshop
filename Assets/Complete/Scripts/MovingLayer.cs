using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Complete
{
	public class MovingLayer : MonoBehaviour
	{
		// the speed od the layer which is latel multiplied by speedFactor
		// layers have various speeds to create parallax effect
		public float layerSpeed;

		// the delay with which layer starts moving
		// is used for obstacles for example not to kill player when game starts
		public float layerDelay;

		// increases or decreases speed without changing the speed itself
		// the layers are moving with different speeds to create parallax effect
		// changing speed factor on the layers keeps the parallax effect but thanges game dynamics at the same time
		public float speedFactor;

		// the coordinates of the position where next fragment would be spawned
		public float spawnAtPosition;

		// the coordinate where fragment would be deactivated and put back to pool
		public float resetAtPosition;

		// all the fragments are of the same length
		// it is used to calculate when next fragment should be spawned
		public float fragmentLength;

		// all the preconfigured level fragments are stored here
		// they are rmoved from the pool when instantiated
		// and put back to the pool when they reach reset point
		public List<Transform> fragmentPool;

		// the fragments that are active on the scene
		// they are processed and moved on each update cycle
		public List<Transform> activeFragments;

		// we cache last spawned fragment to track if next fragment should be instantiated
		// as this happens when last fragments right bound leaves spawnAtPosition
		public Transform lastSpawnedFragment;


		// Use this for initialization
		void Start ()
		{
			// deactivate fragments and add them to the pool
			foreach (Transform child in transform) {
				child.gameObject.SetActive (false);
				fragmentPool.Add (child);
			}

			// spawn first fragment for the game
			SpawnFragment ();
		}

		void SpawnFragment ()
		{
			// if we have no fragments to instantiate, then return
			if (fragmentPool.Count == 0)
				return;

			// select random fragment index
			int randomFragment = Random.Range (0, fragmentPool.Count);

			// get fragment from the pool
			var fragment = fragmentPool [randomFragment];

			// remove fragment from the pool
			fragmentPool.Remove (fragment);

			// add fragment to active fragments
			activeFragments.Add (fragment);

			// cache fragment as lastSpawnedFragment
			lastSpawnedFragment = fragment;

			// set position to spawnAtPosition
			fragment.transform.position = new Vector3 (spawnAtPosition, fragment.transform.position.y, 0);

			// activate fragment and let it start moving and interacting with player
			fragment.gameObject.SetActive (true);

		}


		void RemoveFragment (Transform fragment)
		{
			// disable fragment so that it would not be shown any more
			fragment.gameObject.SetActive (false);

			// remove from active fragments
			activeFragments.Remove (fragment);

			// return fragment back to pool
			fragmentPool.Add (fragment);
		}
	
		void Update ()
		{
			// if layer delay has not passed, then return
			if (Time.time < layerDelay)
				return;

			// if last spawned fragment left spawn zone, then spawn next fragment
			if (lastSpawnedFragment.position.x + fragmentLength < spawnAtPosition) {
				SpawnFragment ();	
			}

			// calculate the shift for the fragment
			float dx = -Time.deltaTime * layerSpeed * speedFactor;

			// store the fragment that should be removed
			Transform fragmentToRemove = null;

			// iterate active fragments
			foreach (Transform fragment in activeFragments) {
				// move every fragment
				fragment.Translate (dx, 0, 0);

				// if fragment reached reset position point then store it to fragmentToRemove variable
				// remember that we can not change collections when in foreach cycle
				if (fragment.position.x < resetAtPosition) {
					fragmentToRemove = fragment;
				}
			}

			// of one of the fragments should be removed then remove it
			if (fragmentToRemove) {
				RemoveFragment (fragmentToRemove);
			}

		}

	}
}