using UnityEngine;
using UnityEngine.UI;
using Manager;
using SimpleJSON;

namespace Utility
{
	/// <summary>
	/// A helper class to display highscores in the scene.
	/// </summary>
	public class HighscoreDisplayHelper : MonoBehaviour
    {
        [SerializeField] private HighscoreEntry[] highscoreEntries;
        [SerializeField] private Text localHighscoreText;
        [SerializeField] private Text statusText;

		private float refreshRate = 60f;

        private void Start()
        {
            localHighscoreText.text = "Local Highscore: " + HighscoreManager.instance.GetLocalHighscore();

			for (int i=0; i<highscoreEntries.Length; i++)
			{
                highscoreEntries[i].Populate(i + 1 + ".", "Fetching...", "Fetching...");
            }

            InvokeRepeating("RefreshHighscores", 0f, refreshRate);
        }

		/// <summary>
		/// Fills each of the entrys with values from the supplied array.
		/// </summary>
        public void DisplayHighscores(JSONArray entries)
		{
			for (int i = 0; i < entries.Count; i++)
			{
				highscoreEntries[i].Populate(i + 1 + ".", "", "");

				if (entries.Count > i)
				{
					highscoreEntries[i].Populate(i + 1 + ".", entries[i]["name"], entries[i]["score"]);
				}
			}
		}

		/// <summary>
		/// Updates the status text.
		/// </summary>
		public void DisplayError(string message)
		{
			statusText.text = message;
		}
		
		/// <summary>
		/// Invoked every x seconds in the start menu to pull new highscores.
		/// </summary>
        private void RefreshHighscores()
        {
			HighscoreManager.instance.DownloadHighscores();
		}
		
		/// <summary>
		/// Called from a Unity button, uploads the highscore to Dreamlo.
		/// </summary>
        public void Upload(InputField usernameInputField)
        {
			Text placeholderText = usernameInputField.placeholder.GetComponent<Text>();

			if (HighscoreManager.instance.GetLocalHighscore() <= 0)
			{
				placeholderText.text = Constants.SCORE_NOT_ZERO;
			}
			else if (string.IsNullOrEmpty(usernameInputField.text))
			{
				placeholderText.text = Constants.NICKNAME_REQUIRED;
			}
			else if (PlayerPrefs.GetInt(Constants.ALREADY_UPLOADED_KEY) != 0)
			{
				usernameInputField.text = "";
				placeholderText.text = Constants.ALREADY_UPLOADED;
			}
			else
			{
				HighscoreManager.instance.UploadNewHighscore(usernameInputField.text, HighscoreManager.instance.GetLocalHighscore());
				usernameInputField.text = "";
				placeholderText.text = Constants.UPLOADED;
			}
        }

		public void ClearEntries()
		{
			foreach (HighscoreEntry entry in highscoreEntries)
			{
                entry.Populate("", "", "");
			}
		}
    }
}
