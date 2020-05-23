using Ghosts;
using Manager;
using System.Collections;
using UnityEngine;
using Utility;

namespace Pacman
{
	/// <summary>
	/// Collision component for Pacman.
	/// </summary>
	public class PacmanCollision : MonoBehaviour
	{
		[SerializeField] private bool godMode;
		[SerializeField] private int currentLives = 3;

		private AudioManager audioManager;
		private Debugger debugger;
		private GameEventManager gameEventManager;
		private GameObjectManager goManager;
		private PacmanHud pacmanHud;
		private PacmanScore pacmanScore;
		private PacmanMovement pacmanMovement;
		private Rigidbody rb;

		private void Start()
		{
			pacmanHud = GetComponent<PacmanHud>();
			pacmanScore = GetComponent<PacmanScore>();
			pacmanMovement = GetComponent<PacmanMovement>();
			debugger = GetComponent<Debugger>();
			rb = GetComponent<Rigidbody>();
			audioManager = AudioManager.instance;
			gameEventManager = GameEventManager.instance;
			goManager = GameObjectManager.instance;
		}

		private void OnCollisionEnter(Collision other)
		{
			switch (other.gameObject.tag)
			{
				case "Food":
					other.gameObject.SetActive(false);
					pacmanScore.AddScore(Constants.FOOD_SCORE);
					audioManager.Play(SoundNames.FOOD);
					goManager.CountFood();

					if (goManager.GetNumberOfFood() <= 0)
					{
						gameEventManager.CompleteLevel();
					}
					else if (goManager.GetNumberOfFood().IsAMultipleOf(Constants.FRUIT_EATEN_BEFORE_SPEED_INCREASE))
					{
						audioManager.GetSound(SoundNames.GHOST_MOVE).source.pitch += Constants.GHOST_SPEED_INCREASE_PITCH_INCREASE;
						foreach (Ghost ghost in goManager.GetGhosts())
						{
							ghost.IncreaseSpeed();
						}
					}
					break;

				case "Powerup":
					debugger.Info("activated powerup");
					other.gameObject.SetActive(false);
					audioManager.PlayForDuration(SoundNames.GHOST_EDIBLE, Constants.POWERUP_DURATION);
					goManager.MakeGhostsEdible();
					pacmanMovement.BoostSpeed();
					break;

				case "Cherry":
					debugger.Info("collected a cherry");
					audioManager.Play(SoundNames.EAT_FRUIT);
					pacmanScore.AddScore(Constants.FRUIT_EATEN_SCORE);
					Destroy(other.gameObject);
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

					// Reset velocity to stop drifting away
					rb.velocity = Vector3.zero;
					ghost.GetComponent<Rigidbody>().velocity = Vector3.zero;

					if (ghost.IsEdible())
					{
						ghost.RunHome();
						audioManager.Play(SoundNames.EAT_GHOST);
						pacmanScore.AddScore(Constants.GHOST_EATEN_SCORE);
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

			audioManager.PauseAllSounds();
			goManager.StopMovingEntities();
			audioManager.Pause(SoundNames.GHOST_MOVE);
			audioManager.GetSound(SoundNames.GHOST_MOVE).source.pitch = Constants.GHOST_MOVE_PITCH;

			yield return new WaitForSeconds(1f);
			audioManager.Play(SoundNames.PACMAN_DEATH);
			yield return new WaitForSeconds(audioManager.GetSound(SoundNames.PACMAN_DEATH).clip.length + 1f);

			goManager.ResetEntityPositions();

			if (currentLives < 0)
			{
				bool newHighscore = false;
				int score = pacmanScore.GetScore();
				HighscoreManager highscoreManager = HighscoreManager.instance;

				if (score > highscoreManager.GetLocalHighscore())
				{
					highscoreManager.SaveLocalHighscore(score);
					newHighscore = true;
				}

				gameEventManager.GameOver(newHighscore);
			}
			else
			{
				pacmanHud.RemoveLife(currentLives);
				gameEventManager.RespawnPacman();
				audioManager.Play(SoundNames.GHOST_MOVE);
			}
		}

		public int GetCurrentLives()
		{
			return currentLives;
		}
	}
}