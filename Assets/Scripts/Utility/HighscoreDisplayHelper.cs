using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class HighscoreDisplayHelper : MonoBehaviour
    {
        public HighscoreEntry[] highscoreEntries;
        public Text localHighscoreText;
        public Text statusText;

        void Start()
        {
            localHighscoreText.text = "Local Highscore: " + HighscoreManager.instance.LoadLocalHighscore();

            for (int i = 0; i < highscoreEntries.Length; i++)
            {
                highscoreEntries[i].Populate(i + 1 + ".", "Fetching...", "Fetching...");
            }

            InvokeRepeating("RefreshHighscores", 0f, 15f);
        }


        public void OnHighscoresDownloaded(Highscore[] highscoreList)
        {
            for (int i = 0; i < highscoreEntries.Length; i++)
            {
				highscoreEntries[i].Populate(i + 1 + ".", "", "");

                if (highscoreList.Length > i)
                {
                    highscoreEntries[i].Populate(i + 1 + ".", highscoreList[i].username, highscoreList[i].score.ToString());
                }
            }
        }


        public void NoInternetConnection()
        {
            statusText.text = "No Internet Connection.";
        }


        void RefreshHighscores()
        {
            HighscoreManager.instance.DownloadHighscores();
        }


        public void Upload(InputField usernameInputField)
        {
            if (HighscoreManager.instance.LoadLocalHighscore() <= 0)
                usernameInputField.placeholder.GetComponent<Text>().text = "Score cannot be 0!";

            else if (string.IsNullOrEmpty(usernameInputField.text))
                usernameInputField.placeholder.GetComponent<Text>().text = "Enter a nickname!";

            else if (PlayerPrefs.GetInt("hasUploadedHighscore") != 0)
            {
                usernameInputField.text = "";
                usernameInputField.placeholder.GetComponent<Text>().text = "Already uploaded!";
            }

            else
            {
                HighscoreManager.instance.AddNewHighscore(usernameInputField.text, HighscoreManager.instance.LoadLocalHighscore());
                usernameInputField.text = "";
                usernameInputField.placeholder.GetComponent<Text>().text = "Uploaded!";
            }
        }

        public void Load(string scene)
        {       
            SceneManager.LoadScene(scene);
        }
    }
}
