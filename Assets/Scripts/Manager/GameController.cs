using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Pacman;

namespace Manager
{
    public class GameController : MonoBehaviour
    {
        public TextMesh statusText;
        public GameObject ghostHome;

        public static GameController instance;

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
            ghostHome.SetActive(false);
            StartCoroutine(StartGame());
        }


        IEnumerator StartGame()
        {
            statusText.text = "READY?";
            AudioManager.instance.Play("Intro Music");
            yield return new WaitForSeconds(AudioManager.instance.GetSound("Intro Music").clip.length);
            statusText.text = "";
            GameManager.instance.StartMovingEntities();

            StartCoroutine(ActivateGhostHome());
        }

        IEnumerator ActivateGhostHome()
        {
            yield return new WaitForSeconds(3f);
            ghostHome.SetActive(true);
        }

        IEnumerator LevelComplete()
        {
            GameManager.instance.StopMovingEntities();

            AudioManager.instance.Play("Level Complete");

            yield return new WaitForSeconds(AudioManager.instance.GetSound("Level Complete").clip.length);

            GameManager.instance.ActivateFood();
            GameManager.instance.ActivatePowerups();
            GameManager.instance.ResetEntityPositions();

            statusText.text = "READY?";
            yield return new WaitForSeconds(1.5f);
            statusText.text = "";

            GameManager.instance.StartMovingEntities();
        }

        IEnumerator GameOver(bool newHighscore)
        {
            statusText.text = "GAME OVER";
            if (newHighscore)
                statusText.text += "\nNEW HIGHSCORE!";
            AudioManager.instance.PauseAllSounds(); 
            yield return new WaitForSeconds(5f);
			Screen.orientation = ScreenOrientation.Portrait;
            SceneManager.LoadScene("Main Menu");
        }

        public void TriggerGameOver(bool newHighscore)
        {
            StartCoroutine(GameOver(newHighscore)); 
        }

        public void TriggerLevelComplete()
        {
            StartCoroutine(LevelComplete());
        }
    }
}

