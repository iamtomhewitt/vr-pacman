using System.Collections;
using UnityEngine;
using Manager;
using Ghosts;
using Utility;

namespace Pacman
{
	/// <summary>
	/// Methods for when Pacman collides with something.
	/// </summary>
	public class PacmanCollision : MonoBehaviour
    {
        [SerializeField] private bool godMode;
        [SerializeField] private int currentLives = 3;

		private Rigidbody rb;
		private Debugger debugger;
        private GameObjectManager goManager;

		private static PacmanCollision instance;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			debugger = GetComponent<Debugger>();
			rb = GetComponent<Rigidbody>();
            goManager = GameObjectManager.instance;
		}

		private void OnCollisionEnter(Collision other)
        { 
            switch (other.gameObject.tag)
            {
                case "Food":
                    other.gameObject.SetActive(false);
                    PacmanScore.instance.AddScore(Constants.FOOD_SCORE);
                    AudioManager.instance.Play(SoundNames.FOOD);
                    goManager.CountFood();

                    print(string.Format("Number of food: {0}, divisble by 50: {1}", goManager.GetNumberOfFood(), (goManager.GetNumberOfFood() % 50) == 0));

					if (goManager.GetNumberOfFood() <= 0)
					{
						GameEventManager.instance.CompleteLevel();
					}
                    else if ((goManager.GetNumberOfFood() % 50) == 0)
                    {
                        AudioManager.instance.GetSound(SoundNames.GHOST_MOVE).source.pitch += 0.03f;
                        foreach(Ghost ghost in goManager.GetGhosts())
                        {
                            ghost.IncreaseSpeed();
                        }
                    }
                    break;

                case "Powerup":
					debugger.Info("activated powerup");
					other.gameObject.SetActive(false);
					AudioManager.instance.PlayForDuration(SoundNames.GHOST_EDIBLE, Constants.POWERUP_DURATION);
					goManager.MakeGhostsEdible();
					PacmanMovement.instance.BoostSpeed();
                    break;

                case "Cherry":
					debugger.Info("collected a cherry");
					AudioManager.instance.Play(SoundNames.EAT_FRUIT);
                    Destroy(other.gameObject);
					PacmanScore.instance.AddScore(Constants.FRUIT_EATEN_SCORE);
                    break;
            }
        }
	
        private void OnTriggerEnter(Collider other)
        {
            if (godMode)
            {
				debugger.Info("god mode enabled, ignoring collision");
				return;
            }

            switch (other.gameObject.tag)
            {
                case "Ghost":   
                    Ghost ghost = other.GetComponent<Ghost>();

					if (ghost.IsRunningHome())
					{
						return;
					}

					// Reset the velocity to stop pacman drifting away
					rb.velocity = Vector3.zero; 

                    if (ghost.IsEdible())
                    {
                        ghost.RunHome();
                        AudioManager.instance.Play(SoundNames.EAT_GHOST);
						PacmanScore.instance.AddScore(Constants.GHOST_EATEN_SCORE);
                    }
                    else
                    { 
                        StartCoroutine(Die());
                    }
                    break;
            }
        }

		/// <summary>
		/// Routine called when a Ghost collides with Pacman.
		/// </summary>
        private IEnumerator Die()
        {
			debugger.Info("has died");

			currentLives--;

            AudioManager.instance.PauseAllSounds();
            goManager.StopMovingEntities();
			AudioManager.instance.Pause(SoundNames.GHOST_MOVE);

			yield return new WaitForSeconds(1f);
            AudioManager.instance.Play(SoundNames.PACMAN_DEATH);
			yield return new WaitForSeconds(AudioManager.instance.GetSound(SoundNames.PACMAN_DEATH).clip.length + 1f);

            goManager.ResetEntityPositions();

            if (currentLives < 0)
            {
                bool newHighscore = false;
				int score = PacmanScore.instance.GetScore();

                if (score > HighscoreManager.instance.GetLocalHighscore())
                {
                    HighscoreManager.instance.SaveLocalHighscore(score);
                    newHighscore = true;
                }

                GameEventManager.instance.GameOver(newHighscore);
            }
            else
            {
				PacmanHud.instance.RemoveLife(currentLives);
				GameEventManager.instance.RespawnPacman();                
				AudioManager.instance.Play(SoundNames.GHOST_MOVE);
			}
        }

		public int GetCurrentLives()
		{
			return currentLives;
		}
    }
}