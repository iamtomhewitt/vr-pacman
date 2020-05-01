using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Pacman;
using Utility;

namespace Manager
{
    public class GameEventManager : MonoBehaviour
    {
		private const string READY = "READY?";
		private const string GAME_OVER = "GAME OVER";

		private Debugger debugger;

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
			debugger = GetComponent<Debugger>();
            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
			debugger.Info("starting game");

			PacmanHud.instance.SetStatusText(READY);
            AudioManager.instance.Play(SoundNames.INTRO_MUSIC);
            yield return new WaitForSeconds(AudioManager.instance.GetSound(SoundNames.INTRO_MUSIC).clip.length);
			PacmanHud.instance.SetStatusText("");
			AudioManager.instance.Play(SoundNames.GHOST_MOVE);
			GameObjectManager.instance.StartMovingEntities();
			GameObjectManager.instance.ActivateGhostHome();
        }
        
		/// <summary>
		/// Routine that completes the current level.
		/// </summary>
        private IEnumerator CompleteLevelRoutine()
        {
			debugger.Info("completed level");

			GameObjectManager.instance.StopMovingEntities();

			AudioManager.instance.Pause(SoundNames.GHOST_MOVE);
			AudioManager.instance.Play(SoundNames.LEVEL_COMPLETE);

            yield return new WaitForSeconds(AudioManager.instance.GetSound(SoundNames.LEVEL_COMPLETE).clip.length);

            GameObjectManager.instance.ActivateFood();
            GameObjectManager.instance.ActivatePowerups();
            GameObjectManager.instance.ResetEntityPositions();

			PacmanHud.instance.SetStatusText(READY);
            yield return new WaitForSeconds(1.5f);
			PacmanHud.instance.SetStatusText("");

            GameObjectManager.instance.StartMovingEntities();
			AudioManager.instance.Play(SoundNames.GHOST_MOVE);
		}

		/// <summary>
		/// Routine that makes the game end.
		/// </summary>
        private IEnumerator GameOverRoutine(bool newHighscore)
        {
			debugger.Info("game over");

			PacmanHud.instance.SetStatusText(GAME_OVER);

			if (newHighscore)
			{
				PacmanHud.instance.SetStatusText(GAME_OVER+"\nNEW HIGHSCORE!");
			}

            AudioManager.instance.PauseAllSounds(); 

            yield return new WaitForSeconds(5f);

			FindObjectOfType<Utilities>().RotateScreenPortrait();
			
			AudioManager.instance.Play(SoundNames.MENU_MUSIC);
			SceneManager.LoadScene(Constants.MAIN_MENU_SCENE);
        }

		/// <summary>
		/// Routine that respawns Pacman after he has died.
		/// </summary>
		private IEnumerator RespawnRoutine()
		{
			debugger.Info("respawning");

			PacmanHud.instance.SetStatusText(READY);
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

