using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Complete
{
	public class PlayerController: MonoBehaviour
	{
		// the state of the plane
		private PlayerState playerState = PlayerState.GoingDown;

		// the bounds in which plane operates
		public float maxVerticalPosition;
		public float minVerticalPosition;

		// the position where plane is crashed
		public float groundPosition;

		// maximum angle to tilt the plane up and down
		public float maxTiltAngle;

		// the speed to move the plane up and down
		public float verticalSpeed;

		// SFX game objects that should be enabled when plane is falling or crashed
		public ParticleSystem FallingSmoke;
		public ParticleSystem CrashedSmoke;

		// Game controller that tracks player score
		public GameController gameController;

		// game layers that should be paused when plane is crashed
		public GameLayers gameLayers;


		// this ethod is called externally by whatever is controlling the plane
		public void SetAcceleratorState (bool acceleratorPressed)
		{
			// if plane is falling or crashed, user control is ignored
			if (playerState == PlayerState.Falling || playerState == PlayerState.Crashed)
				return;

			// if accelerator is pressed then plane goes up, otherwise it goes down
			if (acceleratorPressed) {
				playerState = PlayerState.GoingUp;
			} else
				playerState = PlayerState.GoingDown;
		}
	
		void Update ()
		{
			float verticalPosition = transform.position.y;
			
			float deltaY = 0f;
			
			if (playerState == PlayerState.GoingUp) 
			{
				if (verticalPosition>maxVerticalPosition)
					return;
				
				deltaY = verticalSpeed * Time.deltaTime;
			}
			
			if (playerState == PlayerState.GoingDown) 
			{
				if (verticalPosition<minVerticalPosition)
					return;
				deltaY = -verticalSpeed * Time.deltaTime;
				
			}
			
			// if plane is falling and it has reached the ground then move to crashed state
			if (playerState == PlayerState.Falling && this.transform.position.y <= groundPosition) 
			{
				TransitionToCrashed ();
			}
			
			
			// if plane is falling then it is moving down with it's vertical speed 
			// without additional multipliers and checks
			
			if (playerState == PlayerState.Falling) {
				
				// calculate new y position
				deltaY = -verticalSpeed * Time.deltaTime;
			}
			
			// apply calculated position to the object
			this.transform.Translate (0, deltaY, 0);
		}
	

		void OnTriggerEnter2D (Collider2D other)
		{

			// falling and crashed states does not react on triggers
			if (playerState == PlayerState.Falling || playerState == PlayerState.Crashed)
				return;

			// if player enters mine trigger then it changes state tio falling
			if (other.tag == "Obstacle") {
				TransitionToFalling ();
			}

			// if player enters coin trigger, then it changes state to crashed
			if (other.tag == "Coin") {
				gameController.Coins += 1;
			}

		}


		// Set plane to falling state
		void TransitionToFalling ()
		{
			// Tilt plane down
			var newAngle = Quaternion.Euler (0, 0, -maxTiltAngle);
			this.transform.rotation = Quaternion.Lerp (this.transform.rotation, newAngle, 0.2f);

			// update plane state
			playerState = PlayerState.Falling;

			// play smoke particles
			FallingSmoke.Play ();
		}

		// Set plane to Crashed state
		void TransitionToCrashed ()
		{
			playerState = PlayerState.Crashed;

			FallingSmoke.Stop ();
			CrashedSmoke.Play ();

			// stop the layers as plane is not flying any more
			gameLayers.SetSpeedFactor (0);
		}

	}

}