using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Complete
{
	public class UserInputHandler: MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
	
		public PlayerController playerController;

		// Process canvas event and send command to Player Controller
		public void OnPointerDown (PointerEventData eventData)
		{
			playerController.SetAcceleratorState (true);
		}

		// Process canvas event and send command to Player Controller
		public void OnPointerUp (PointerEventData eventData)
		{
			playerController.SetAcceleratorState (false);
		}

	}
}