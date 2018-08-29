using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pacman
{
    public class PacmanMovement : MonoBehaviour
    {
        public bool useAccelerometer;

        [Space()]
        public float speed;
        public float originalSpeed;
        public float accelerometerSensitivity;

        void Start()
        {
            if (!SystemInfo.supportsGyroscope)
            {
                useAccelerometer = true;
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
        }
    }
}