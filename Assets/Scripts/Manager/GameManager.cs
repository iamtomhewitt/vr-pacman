using UnityEngine;
using System.Collections;
using Pacman;
using Ghosts;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        [HideInInspector]
        public GameObject[] foods;
        public GameObject cherry;

        public GameObject gameOverText;
        public GameObject readyText;

        public int numberOfFood;

        public bool canStartGame;
        bool spawnedCherry;

        public static GameManager instance;

        PacmanData pacman;
        Ghost[] ghosts;
        GameObject[] powerups;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
        }

        void Start()
        {
            foods       = GameObject.FindGameObjectsWithTag("Food");
            ghosts      = GameObject.FindObjectsOfType<Ghost>();
            powerups    = GameObject.FindGameObjectsWithTag("Powerup");
            pacman      = new PacmanData(GameObject.FindObjectOfType<PacmanCollision>(), 
                                         GameObject.FindObjectOfType<PacmanMovement>(), 
                                         GameObject.FindObjectOfType<PacmanScore>());

            CountFood();
            
            for (int i = 0; i < foods.Length; i++)
            {
                // Name the food "Food ([coordinates])
                foods[i].name = "Food (" + foods[i].transform.position.x.ToString() + ", " + foods[i].transform.position.z.ToString() + ")";
                foods[i].transform.parent = GameObject.Find("Game Manager").transform;
            }

            InvokeRepeating("SpawnCherry", 10f, 10f);
        }


        void Update()
        {
            if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
            {
                Application.Quit();
            }
        }

        /// <summary>
        /// Makes a specific Ghost run home.
        /// </summary>
        public void RunHome(Ghost ghost)
        {
            AudioManager.instance.Play("Ghost Run");

            ghost.speed                     = ghost.eatenSpeed;
            ghost.edible                    = false;
            ghost.eaten                     = true;
            ghost.runningHome               = true;
            ghost.bodyColour.enabled        = false;
        }

        /// <summary>
        /// Resets a specific Ghost.
        /// </summary>
        public void ResetGhost(Ghost ghost)
        {
            ghost.eaten                     = false;
            ghost.edible                    = false;
            ghost.runningHome               = false;
            ghost.bodyColour.enabled        = true;
            ghost.speed                     = ghost.movingSpeed;
            ghost.ChangeToOriginalColour();

            if (!AllGhostsRunningHome())
            {
                AudioManager.instance.Pause("Ghost Run");
            }   
        }

        /// <summary>
        /// Resets all Ghosts.
        /// </summary>
        public void ResetGhosts()
        {
            for (int i = 0; i < ghosts.Length; i++)
            { 
                ResetGhost(ghosts[i]);                 
            }
        }

        /// <summary>
        /// Checks if any of the Ghosts are running home. If at least one Ghost is running home, this method
        /// returns true.
        /// </summary>
        /// <returns><c>true</c>, if ghosts running home was alled, <c>false</c> otherwise.</returns>
        public bool AllGhostsRunningHome()
        {
            if (!ghosts[0].runningHome &&
                !ghosts[1].runningHome &&
                !ghosts[2].runningHome &&
                !ghosts[3].runningHome)
            {
                //print("No Ghosts are running home.");
                return false;
            }
            else
            {
                //print("A Ghost is running home.");
                return true;
            }
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
                if (!g.runningHome)
                    g.BecomeEdible();
            }
        }

        /// <summary>
        /// Starts moving Pacman and the Ghosts.
        /// </summary>
        public void StartMovingEntities()
        {
            pacman.movement.speed = pacman.movement.originalSpeed;

            // Reset the Ghosts speed and their path node
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].speed = ghosts[i].movingSpeed;
                ghosts[i].currentNode = 0;
            }

            AudioManager.instance.Play("Ghost Move");
        }

        /// <summary>
        /// Stops moving Pacman and the Ghosts.
        /// </summary>
        public void StopMovingEntities()
        {
            // Stop all the ghosts moving
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].speed = 0f;
            }

            pacman.movement.speed = 0f;

            AudioManager.instance.Pause("Ghost Move");
        }

        /// <summary>
        /// Resets Pacman and Ghost positions.
        /// </summary>
        public void ResetEntityPositions()
        {
            // Reset the positions of the Ghosts
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].gameObject.transform.position = new Vector3((-1.5f + i), 0f, -1.5f);

                // We reset the current node here to stop the Ghosts immediately looking at the first node when pacman dies
                ghosts[i].currentNode = 0;
            }

            // Reset pacmans position
            pacman.movement.transform.position = new Vector3(0f, 0f, -3.43f);
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

        /// <summary>
        /// Spawns a cherry.
        /// </summary>
        void SpawnCherry()
        {
            if ((numberOfFood <= 80 && numberOfFood >= 40) && !spawnedCherry)
            {
                Instantiate(cherry, new Vector3(0f, -0.303f, -9.26f), Quaternion.Euler(new Vector3(0f, 90f, 0f)));
                spawnedCherry = true;
            }
            if (numberOfFood <= 1)
            {
                spawnedCherry = false;
            }
        }

        public void CountFood()
        {
            numberOfFood = GameObject.FindGameObjectsWithTag("Food").Length;
        }
    }
}
