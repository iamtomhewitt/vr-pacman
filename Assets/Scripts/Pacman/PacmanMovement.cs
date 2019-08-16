using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pacman
{
    public class PacmanMovement : MonoBehaviour
    {
        public bool useAccelerometer;
		public bool debug;

        [Space()]
        public float speed;
        public float originalSpeed;
		public float boostSpeed;
        public float accelerometerSensitivity;

		public static PacmanMovement instance;

		private void Awake()
		{
			instance = this;
		}

		void Start()
        {
            if (!SystemInfo.supportsGyroscope)
            {
                useAccelerometer = true;
				Input.gyro.enabled = false;				// Sanity check
            }

            originalSpeed = speed;
            speed = 0f;
        }

        void Update()
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
        }

		public IEnumerator BoostSpeed()
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
	}
}