using UnityEngine;
using System.Collections;

namespace Complete
{
	public class SetAcceleratorStateForTest : MonoBehaviour
	{

		public PlayerController playerController;
		public bool acceleratorState;

		void Start ()
		{
			playerController.SetAcceleratorState (acceleratorState);
		}

	}
}
