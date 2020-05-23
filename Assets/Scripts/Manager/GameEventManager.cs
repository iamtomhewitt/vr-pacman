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
        private const string GAME_OVER_NEW_HIGHSCORE = "GAME OVER \nNEW HIGHSCORE!";

        private Debugger debugger;
        private AudioManager audioManager;
        private GameObjectManager gameObjectManager;
        private PacmanHud hud;

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
            audioManager = AudioManager.instance;
            gameObjectManager = GameObjectManager.instance;
            hud = PacmanHud.instance;

            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            debugger.Info("starting game");

            hud.SetStatusText(READY);
            audioManager.Play(SoundNames.INTRO_MUSIC);
            yield return new WaitForSeconds(audioManager.GetSound(SoundNames.INTRO_MUSIC).clip.length);
            hud.SetStatusText("");
            audioManager.Play(SoundNames.GHOST_MOVE);
            gameObjectManager.StartMovingEntities();
            gameObjectManager.ActivateGhostHome();
        }

        /// <summary>
        /// Completes the current level.
        /// </summary>
        public void CompleteLevel()
        {
            StartCoroutine(CompleteLevelRoutine());
        }

        /// <summary>
        /// Routine that completes the current level.
        /// </summary>
        private IEnumerator CompleteLevelRoutine()
        {
            debugger.Info("completed level");

            gameObjectManager.StopMovingEntities();

            audioManager.Pause(SoundNames.GHOST_MOVE);
            audioManager.Play(SoundNames.LEVEL_COMPLETE);

            yield return new WaitForSeconds(audioManager.GetSound(SoundNames.LEVEL_COMPLETE).clip.length);

            gameObjectManager.ActivateFood();
            gameObjectManager.ActivatePowerups();
            gameObjectManager.ResetEntityPositions();
            gameObjectManager.ResetAllGhosts();

            hud.SetStatusText(READY);
            yield return new WaitForSeconds(1.5f);
            hud.SetStatusText("");

            gameObjectManager.StartMovingEntities();
            audioManager.Play(SoundNames.GHOST_MOVE);
        }

        /// <summary>
        /// Ends the game.
        /// </summary>
        public void GameOver(bool newHighscore)
        {
            StartCoroutine(GameOverRoutine(newHighscore));
        }

        /// <summary>
        /// Routine that makes the game end.
        /// </summary>
        private IEnumerator GameOverRoutine(bool newHighscore)
        {
            debugger.Info("game over");

            hud.SetStatusText(GAME_OVER);

            if (newHighscore)
            {
                hud.SetStatusText(GAME_OVER_NEW_HIGHSCORE);
            }

            audioManager.PauseAllSounds();

            yield return new WaitForSeconds(5f);

            FindObjectOfType<Utilities>().RotateScreenPortrait();

            audioManager.Play(SoundNames.MENU_MUSIC);
            SceneManager.LoadScene(Constants.MAIN_MENU_SCENE);
        }

        /// <summary>
        /// Respawns Pacman after he's died.
        /// </summary>
        public void RespawnPacman()
        {
            StartCoroutine(RespawnRoutine());
        }

        /// <summary>
        /// Routine that respawns Pacman after he has died.
        /// </summary>
        private IEnumerator RespawnRoutine()
        {
            debugger.Info("respawning");

            hud.SetStatusText(READY);
            yield return new WaitForSeconds(2f);
            hud.SetStatusText("");
            gameObjectManager.StartMovingEntities();
        }
    }
}

