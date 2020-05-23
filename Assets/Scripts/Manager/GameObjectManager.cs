using Ghosts;
using Pacman;
using System.Collections;
using UnityEngine;

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
        private Debugger debugger;
        private PacmanMovement pacmanMovement;
        private float cherrySpawnRepeateRate = 10f;
        private int foodCount;
        private int cherrySpawnMaxFood = 80;
        private int cherrySpawnMinFood = 40;
        private bool spawnedCherry;

        public static GameObjectManager instance;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            food = GameObject.FindGameObjectsWithTag(Tags.FOOD);
            powerups = GameObject.FindGameObjectsWithTag(Tags.POWERUP);
            ghosts = FindObjectsOfType<Ghost>();
            debugger = GetComponent<Debugger>();
            pacmanMovement = PacmanMovement.instance;

            ghostHome.SetActive(false);

            foodCount = CountFood();

            for (int i = 0; i < food.Length; i++)
            {
                // Name the food "Food ([coordinates])"
                food[i].name = "Food (" + food[i].transform.position.x.ToString() + ", " + food[i].transform.position.z.ToString() + ")";
                food[i].transform.parent = this.transform;
            }

            InvokeRepeating("SpawnCherry", cherrySpawnRepeateRate, cherrySpawnRepeateRate);
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

            pacmanMovement.ResetSpeed();

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

            pacmanMovement.Stop();
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
            pacmanMovement.ResetPosition();
        }

        public void ResetAllGhosts()
        {
            foreach (Ghost ghost in ghosts)
            {
                ghost.Reset();
                ghost.ResetPosition();
            }
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
            if (foodCount.IsBetween(cherrySpawnMinFood, cherrySpawnMaxFood) && !spawnedCherry)
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
            foodCount = GameObject.FindGameObjectsWithTag(Tags.FOOD).Length;
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

        public Ghost[] GetGhosts()
        {
            return ghosts;
        }
    }
}