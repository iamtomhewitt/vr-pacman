using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pacman;

namespace Manager
{
    /// <summary>
    /// Data class used to access different parts of Pacman, but under one variable in the above script.
    /// </summary>
    [System.Serializable]
    public class PacmanData
    {
        public PacmanCollision collision;
        public PacmanMovement movement;
        public PacmanScore score;

        public PacmanData(PacmanCollision collision, PacmanMovement movement, PacmanScore score)
        {
            this.collision = collision;
            this.movement = movement;
            this.score = score;
        }
    }
}
