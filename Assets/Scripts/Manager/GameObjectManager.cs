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

		[SerializeField] private Transform cherrySpawn;

		private int foodCount;
		private bool spawnedCherry;
		private Debugger debugger;

        public static GameObjectManager instance;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            foods = GameObject.FindGameObjectsWithTag("Food");
			powerups = GameObject.FindGameObjectsWithTag("Powerup");
			ghosts = FindObjectsOfType<Ghost>();
			debugger = GetComponent<Debugger>();

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
			debugger.Info("moving everything");

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
			debugger.Info("stopping everything");

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
			debugger.Info("resetting positions");

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
			debugger.Info("ghost home activated");
		}

		/// <summary>
		/// Spawns a cherry.
		/// </summary>
		private void SpawnCherry()
        {
            if ((foodCount <= 80 && foodCount >= 40) && !spawnedCherry)
            {
                Instantiate(cherry, cherrySpawn.position, cherrySpawn.rotation);
                spawnedCherry = true;
				debugger.Info("spawned cherry");
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
