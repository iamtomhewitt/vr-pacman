﻿using UnityEngine;
using System.Collections;

namespace Utility
{
	/// <summary>
	/// The powerup that makes the Ghosts flash.
	/// </summary>
    public class Powerup : MonoBehaviour 
    {
        public Vector3 speed;

        private void Update()
        {
            transform.Rotate(speed);
        }
    }
}