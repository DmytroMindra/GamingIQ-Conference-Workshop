using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UserInputHandler: MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	
	public PlayerController playerController;

	public void OnPointerDown (PointerEventData eventData)
	{
		playerController.SetAcceleratorState (true);
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		playerController.SetAcceleratorState (false);
	}

}
