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
		[SerializeField] private GameObject cherry;
		[SerializeField] private GameObject ghostHome;
		[SerializeField] private Transform cherrySpawn;

        private GameObject[] food;
        private GameObject[] powerups;
        private Ghost[] ghosts;

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
            food = GameObject.FindGameObjectsWithTag("Food");
			powerups = GameObject.FindGameObjectsWithTag("Powerup");
			ghosts = FindObjectsOfType<Ghost>();
			debugger = GetComponent<Debugger>();

			ghostHome.SetActive(false);

			foodCount = CountFood();
            
            for (int i = 0; i < food.Length; i++)
            {
                // Name the food "Food ([coordinates])"
                food[i].name = "Food (" + food[i].transform.position.x.ToString() + ", " + food[i].transform.position.z.ToString() + ")";
                food[i].transform.parent = this.transform;
            }

            InvokeRepeating("SpawnCherry", 10f, 10f);
        }

        /// <summary>
        /// Makes all Ghosts edible.
        /// </summary>
        public void MakeGhostsEdible()
        {
			foreach (Ghost ghost in ghosts)
			{
				// Only want to become edible if we are not running home
				if (!ghost.IsRunningHome())
				{
					ghost.BecomeEdible();
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
			foreach (Ghost ghost in ghosts)
			{
				ghost.SetSpeed(ghost.GetMovingSpeed());
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
						
			foreach (Ghost ghost in ghosts)
			{
				ghost.StopMoving();
			}

			PacmanMovement.instance.Stop();
        }

        /// <summary>
        /// Resets Pacman and Ghost positions.
        /// </summary>
        public void ResetEntityPositions()
        {
			debugger.Info("resetting positions");

			foreach (Ghost ghost in ghosts)
			{
				ghost.ResetPosition();		
			}

			// We reset the current node here to stop the Ghosts immediately looking at the first node when pacman dies
			foreach (GhostPath path in FindObjectsOfType<GhostPath>())
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
            for (int i = 0; i < food.Length; i++)
            {
                food[i].SetActive(true);
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
		public void SpawnCherry()
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
            foodCount = GameObject.FindGameObjectsWithTag("Food").Length;
			return foodCount;
        }

        public int GetNumberOfFood()
        {
            return foodCount;
        }

		public GameObject GetGhostHome()
		{
			return ghostHome;
		}
    }
}
