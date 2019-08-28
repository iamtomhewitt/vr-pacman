using UnityEngine;
using System.Collections;
using Pacman;
using Ghosts;

namespace Manager
{
	/// <summary>
	/// Manages the objects within the game scene, such as Pacman and the Ghosts.
	/// Any events such as game over, or starting the game, that does not involve objects, should be implemented in GameEventManager.
	/// </summary>
	public class GameObjectManager : MonoBehaviour
    {
        public GameObject cherry;
		public GameObject ghostHome;

        private GameObject[] foods;
        private GameObject[] powerups;
        private Ghost[] ghosts;

		private int foodCount;
		private bool spawnedCherry;

        public static GameObjectManager instance;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            foods = GameObject.FindGameObjectsWithTag("Food");
            ghosts = GameObject.FindObjectsOfType<Ghost>();
            powerups = GameObject.FindGameObjectsWithTag("Powerup");

			ghostHome.SetActive(false);

			foodCount = CountFood();
            
            for (int i = 0; i < foods.Length; i++)
            {
                // Name the food "Food ([coordinates])
                foods[i].name = "Food (" + foods[i].transform.position.x.ToString() + ", " + foods[i].transform.position.z.ToString() + ")";
                foods[i].transform.parent = this.transform;
            }

            InvokeRepeating("SpawnCherry", 10f, 10f);
        }

        /// <summary>
        /// Makes all Ghosts edible.
        /// </summary>
        public void MakeGhostsEdible()
        {
			AudioManager.instance.Play(SoundNames.GHOST_EDIBLE);

			for (int i = 0; i < ghosts.Length; i++)
            {
                Ghost g = ghosts[i];

				// Only want to become edible if we are not running home
				if (!g.IsRunningHome())
				{
					g.BecomeEdible();
				}
            }
        }

        /// <summary>
        /// Starts moving Pacman and the Ghosts.
        /// </summary>
        public void StartMovingEntities()
        {
			PacmanMovement.instance.ResetSpeed();

            // Reset the Ghosts speed and their path node
            for (int i = 0; i < ghosts.Length; i++)
            {
				Ghost g = ghosts[i];
				g.SetSpeed(g.GetMovingSpeed());
            }

			foreach (GhostPath path in GameObject.FindObjectsOfType<GhostPath>())
			{
				path.ResetCurrentWaypointIndex();
			}
		}

        /// <summary>
        /// Stops moving Pacman and the Ghosts.
        /// </summary>
        public void StopMovingEntities()
        {
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].StopMoving();
            }

			PacmanMovement.instance.Stop();
        }

        /// <summary>
        /// Resets Pacman and Ghost positions.
        /// </summary>
        public void ResetEntityPositions()
        {
            for (int i = 0; i < ghosts.Length; i++)
            {
				ghosts[i].ResetPosition(i);		
			}

			// We reset the current node here to stop the Ghosts immediately looking at the first node when pacman dies
			foreach (GhostPath path in GameObject.FindObjectsOfType<GhostPath>())
			{
				path.ResetCurrentWaypointIndex();
			}

			// Reset pacmans position
			PacmanMovement.instance.ResetPosition();
        }

        /// <summary>
        /// Activates the food.
        /// </summary>
        public void ActivateFood()
        {
            for (int i = 0; i < foods.Length; i++)
            {
                foods[i].SetActive(true);
            }
        }

        /// <summary>
        /// Activates the powerups.
        /// </summary>
        public void ActivatePowerups()
        {
            for (int i = 0; i < powerups.Length; i++)
            {
                powerups[i].SetActive(true);
            }
        }

		public void ActivateGhostHome()
		{
			StartCoroutine(ActivateGhostHomeRoutine());
		}

		private IEnumerator ActivateGhostHomeRoutine()
		{
			yield return new WaitForSeconds(3f);
			ghostHome.SetActive(true);
		}

		/// <summary>
		/// Spawns a cherry.
		/// </summary>
		private void SpawnCherry()
        {
            if ((foodCount <= 80 && foodCount >= 40) && !spawnedCherry)
            {
                Instantiate(cherry, new Vector3(0f, -0.303f, -9.26f), Quaternion.Euler(new Vector3(0f, 90f, 0f)));
                spawnedCherry = true;
            }
            if (foodCount <= 1)
            {
                spawnedCherry = false;
            }
        }

        public int CountFood()
        {
            return GameObject.FindGameObjectsWithTag("Food").Length;
        }

        public int GetNumberOfFood()
        {
            return foodCount;
        }
    }
}
