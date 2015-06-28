using UnityEngine;
using System.Collections;

namespace Complete
{
	public class Explosion : MonoBehaviour
	{

		// OnExplosionEnd is called when explosion animation is over.

		void OnExplosionEnd ()
		{
			this.gameObject.SetActive (false);
			Destroy (gameObject, 1f);
		}
	
	}
}