﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Pacman;
using Utility;

namespace Manager
{
    public class GameEventManager : MonoBehaviour
    {
		public static GameEventManager instance;

        private void Awake()
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

        private void Start()
        {
            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
			PacmanHud.instance.SetStatusText("READY?");
            AudioManager.instance.Play("Intro Music");
            yield return new WaitForSeconds(AudioManager.instance.GetSound("Intro Music").clip.length);
			PacmanHud.instance.SetStatusText("");
			GameObjectManager.instance.StartMovingEntities();
			AudioManager.instance.Play("Ghost Move");
			GameObjectManager.instance.ActivateGhostHome();
        }
        
		/// <summary>
		/// Routine that completes the current level.
		/// </summary>
        private IEnumerator CompleteLevelRoutine()
        {
            GameObjectManager.instance.StopMovingEntities();

			AudioManager.instance.Pause("Ghost Move");
			AudioManager.instance.Play("Level Complete");

            yield return new WaitForSeconds(AudioManager.instance.GetSound("Level Complete").clip.length);

            GameObjectManager.instance.ActivateFood();
            GameObjectManager.instance.ActivatePowerups();
            GameObjectManager.instance.ResetEntityPositions();

			PacmanHud.instance.SetStatusText("READY?");
            yield return new WaitForSeconds(1.5f);
			PacmanHud.instance.SetStatusText("");

            GameObjectManager.instance.StartMovingEntities();
			AudioManager.instance.Play("Ghost Move");
		}

		/// <summary>
		/// Routine that makes the game end.
		/// </summary>
        private IEnumerator GameOverRoutine(bool newHighscore)
        {
			PacmanHud.instance.SetStatusText("GAME OVER");

			if (newHighscore)
			{
				PacmanHud.instance.SetStatusText("GAME OVER\nNEW HIGHSCORE!");
			}

            AudioManager.instance.PauseAllSounds(); 

            yield return new WaitForSeconds(5f);
			yield return GameObject.FindObjectOfType<Utilities>().DeActivateVRRoutine();

			SceneManager.LoadScene("Main Menu");
        }

		/// <summary>
		/// Routine that respawns Pacman after he has died.
		/// </summary>
		private IEnumerator RespawnRoutine()
		{
			PacmanHud.instance.SetStatusText("READY?");
			yield return new WaitForSeconds(2f);
			PacmanHud.instance.SetStatusText("");
			GameObjectManager.instance.StartMovingEntities();
		}

		/// <summary>
		/// Ends the game.
		/// </summary>
        public void GameOver(bool newHighscore)
        {
            StartCoroutine(GameOverRoutine(newHighscore)); 
        }

		/// <summary>
		/// Completes the current level.
		/// </summary>
        public void CompleteLevel()
        {
            StartCoroutine(CompleteLevelRoutine());
        }

		/// <summary>
		/// Respawns Pacman after he's died.
		/// </summary>
		public void RespawnPacman()
		{
			StartCoroutine(RespawnRoutine());
		}
    }
}

