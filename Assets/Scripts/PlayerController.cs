using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerController: MonoBehaviour
{
	private PlayerState playerState = PlayerState.GoingDown;

	public float maxVerticalPosition;
	public float minVerticalPosition;
	public float maxAngle;
	public float verticalSpeed;

	public Transform NormalPlaneView;


	public void SetAcceleratorState(bool acceleratorPressed)
	{
		if (playerState == PlayerState.Falling || playerState == PlayerState.Crashed)
			return;

		if (acceleratorPressed) {
			playerState = PlayerState.GoingUp;
		} else
			playerState = PlayerState.GoingDown;
	}


	

	void Start()
	{

	}
	
	void Update()
	{
		this.transform.Translate (0, GetVerticalPosition (), 0);
	}
	
	
	float GetVerticalPosition()
	{
		float verticalPosition = transform.position.y;
		float totalVerticalSpace = maxVerticalPosition - minVerticalPosition;
		//float speedingFactor = (verticalPosition - minVerticalPosition) / totalVerticalSpace;

		if (playerState == PlayerState.GoingUp) 
		{
			float distanceToBound = maxVerticalPosition - verticalPosition;
			float distanceToBoundNormalized = distanceToBound / totalVerticalSpace;
			float speedingFactor = Mathf.Sin(distanceToBoundNormalized * Mathf.PI/2);
			var newAngle = Quaternion.Euler(0, 0, maxAngle*speedingFactor);
			NormalPlaneView.rotation =  Quaternion.Lerp(NormalPlaneView.transform.rotation, newAngle, 0.1f);


			float deltaY = verticalSpeed*speedingFactor*Time.deltaTime;
			return deltaY;
		}

		if (playerState == PlayerState.GoingDown) 
		{
			float distanceToBound = verticalPosition - minVerticalPosition;
			float distanceToBoundNormalized = distanceToBound / totalVerticalSpace;
			float speedingFactor = Mathf.Sin(distanceToBoundNormalized * Mathf.PI/2);
			float deltaY = verticalSpeed*speedingFactor*Time.deltaTime;
			var newAngle = Quaternion.Euler(0, 0, -maxAngle*speedingFactor);
			NormalPlaneView.rotation =  Quaternion.Lerp(NormalPlaneView.transform.rotation, newAngle, 0.1f);

			return -deltaY;
		}

		return 0f;
	}
}

