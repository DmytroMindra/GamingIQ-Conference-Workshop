using UnityEngine;
using System.Collections;

namespace Complete
{
	public class Coin : MonoBehaviour
	{
		// Coin is deactivated and would be enable when it's chunk is revived
		void OnTriggerEnter2D (Collider2D other)
		{
		
			if (other.tag == "Player")
				this.gameObject.SetActive (false);

		}
	}
}
