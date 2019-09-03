using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Pacman
{
    public class PacmanMovement : MonoBehaviour
    {
		[SerializeField] private bool useAccelerometer;
		[SerializeField] private bool debug = false;

        [Space()]
		[SerializeField] private float speed;
		[SerializeField] private float originalSpeed;
		[SerializeField] private float boostSpeed;
		[SerializeField] private float accelerometerSensitivity;

		private Rigidbody rb;
		private Debugger debugger;

		public static PacmanMovement instance;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
        {
			debugger = GetComponent<Debugger>();
			rb = GetComponent<Rigidbody>();

            if (!SystemInfo.supportsGyroscope)
            {
                useAccelerometer = true;
				Input.gyro.enabled = false;				// Sanity check
            }

            originalSpeed = speed;
            speed = 0f;
        }

        private void Update()
        {
            if (useAccelerometer)
            {
                transform.Rotate(0f, Input.acceleration.x * accelerometerSensitivity, 0f);
                transform.position += transform.forward * speed * Time.deltaTime;
            }
            else
            {
                Vector3 direction = Camera.main.transform.forward;
                direction.y = 0f;
				transform.position += direction * speed * Time.deltaTime;
            }

			// Even though rigidbody is checked to lock the y-pos, just ensure here
			transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

			// Reset the velocity to stop pacman drifting away
			rb.velocity = Vector3.zero;
		}

		/// <summary>
		/// Boosts Pacmans speed for an amount of time.
		/// </summary>
		public void BoostSpeed()
		{
			debugger.Info("boosting speed");
			StartCoroutine(BoostSpeedRoutine());
		}

		public IEnumerator BoostSpeedRoutine()
		{
			speed = boostSpeed;
			yield return new WaitForSeconds(Constants.POWERUP_DURATION);
			speed = originalSpeed;
		}

		private void OnGUI()
		{
			if (debug)
			{
				GUI.Label(new Rect(0, 0, 200, 100), "Use accelerometer: " + useAccelerometer);
			}
		}
		
		/// <summary>
		/// Resets the speed to the original movement speed.
		/// </summary>
		public void ResetSpeed()
		{
			debugger.Info("resetting speed");
			speed = originalSpeed;
		}

		/// <summary>
		/// Resets the position of Pacman to the start.
		/// </summary>
		public void ResetPosition()
		{
			debugger.Info("resetting position");
			transform.position = new Vector3(0f, 0f, -3.43f);
		}

		/// <summary>
		/// Stops Pacman moving.
		/// </summary>
		public void Stop()
		{
			debugger.Info("stopping");
			speed = 0f;
		}
	}
}