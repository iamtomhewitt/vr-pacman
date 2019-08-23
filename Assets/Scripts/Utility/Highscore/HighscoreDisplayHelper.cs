using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manager;

namespace Utility
{
	/// <summary>
	/// A helper class to display highscores in the scene.
	/// </summary>
    public class HighscoreDisplayHelper : MonoBehaviour
    {
        public HighscoreEntry[] highscoreEntries;
        public Text localHighscoreText;
        public Text statusText;

        private void Start()
        {
            localHighscoreText.text = "Local Highscore: " + HighscoreManager.instance.GetLocalHighscore();

            for (int i = 0; i < highscoreEntries.Length; i++)
            {
                highscoreEntries[i].Populate(i + 1 + ".", "Fetching...", "Fetching...");
            }

            InvokeRepeating("RefreshHighscores", 0f, 60f);
        }

		/// <summary>
		/// Fills each of the entrys with values from the supplied list.
		/// </summary>
        public void PopulateEntries(Highscore[] highscoreList)
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
		
		/// <summary>
		/// Updates the status text.
		/// </summary>
        public void ShowNoInternetConnection()
        {
            statusText.text = "No Internet Connection.";
        }
		
		/// <summary>
		/// Invoked every x seconds in the start menu to pull new highscores.
		/// </summary>
        private void RefreshHighscores()
        {
			StartCoroutine(RefreshHighscoresRoutine());
		}

		/// <summary>
		/// Gets the highscores from the HighscoreManager, then populate with the returned list.
		/// </summary>
		private IEnumerator RefreshHighscoresRoutine()
		{
			yield return StartCoroutine(HighscoreManager.instance.DownloadHighscores());
			Highscore[] list = HighscoreManager.instance.GetHighscoresList();
			PopulateEntries(list);
		}
		
		/// <summary>
		/// Called from a Unity button, uploads the highscore to Dreamlo.
		/// </summary>
        public void Upload(InputField usernameInputField)
        {
			Text placeholderText = usernameInputField.placeholder.GetComponent<Text>();

			if (HighscoreManager.instance.GetLocalHighscore() <= 0)
			{
				placeholderText.text = "Score cannot be 0!";
			}
			else if (string.IsNullOrEmpty(usernameInputField.text))
			{
				placeholderText.text = "Enter a nickname!";
			}
			else if (PlayerPrefs.GetInt(Constants.ALREADY_UPLOADED_KEY) != 0)
			{
				usernameInputField.text = "";
				placeholderText.text = "Already uploaded!";
			}
			else
			{
				HighscoreManager.instance.UploadNewHighscore(usernameInputField.text, HighscoreManager.instance.GetLocalHighscore());
				usernameInputField.text = "";
				placeholderText.text = "Uploaded!";
			}
        }
    }
}
