using System.Collections;
using System.Collections.Generic;
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
        public bool godMode;

        public int currentLives = 3;

		private static PacmanCollision instance;

		private void Awake()
		{
			instance = this;
		}

		private void OnCollisionEnter(Collision other)
        { 
            switch (other.gameObject.tag)
            {
                case "Food":
                    other.gameObject.SetActive(false);
                    PacmanScore.instance.AddScore(Constants.FOOD_SCORE);
                    AudioManager.instance.Play(SoundNames.FOOD);
                    GameObjectManager.instance.CountFood();

					if (GameObjectManager.instance.GetNumberOfFood() <= 0)
					{
						GameEventManager.instance.CompleteLevel();
					}
                    break;

                case "Powerup":
                    other.gameObject.SetActive(false);
					AudioManager.instance.PlayForDuration(SoundNames.GHOST_EDIBLE, Constants.POWERUP_DURATION);
					GameObjectManager.instance.MakeGhostsEdible();
					PacmanMovement.instance.BoostSpeed();
                    break;

                case "Cherry":
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
            currentLives--;

            AudioManager.instance.PauseAllSounds();
            GameObjectManager.instance.StopMovingEntities();
			AudioManager.instance.Pause(SoundNames.GHOST_MOVE);

			yield return new WaitForSeconds(1f);
            AudioManager.instance.Play(SoundNames.PACMAN_DEATH);
			yield return new WaitForSeconds(AudioManager.instance.GetSound(SoundNames.PACMAN_DEATH).clip.length + 1f);

            GameObjectManager.instance.ResetEntityPositions();

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
    }
}
