using UnityEngine;
using System.Collections;

namespace Complete
{
	public class Mine : MonoBehaviour
	{

		public GameObject Explosion;

		// An explosion is instntiated on mine collision.
		// Mine is deactivated and would be enabled when it's fragment is used again

		void OnTriggerEnter2D (Collider2D other)
		{
		
			if (other.tag == "Player")
				this.gameObject.SetActive (false);

			Instantiate (Explosion, this.transform.position, Quaternion.identity);
		}

	}
}