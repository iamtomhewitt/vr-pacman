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
        [SerializeField] private HighscoreEntry entryPrefab;
        [SerializeField] private Transform entriesParent;
        [SerializeField] private Text localHighscoreText;
        [SerializeField] private Text statusText;

        private float refreshRate = 60f;

        private void Start()
        {
            localHighscoreText.text = "Local Highscore: " + HighscoreManager.instance.GetLocalHighscore();

            statusText.text = "Downloading highscores...";
            statusText.color = Color.green;

            InvokeRepeating("RefreshHighscores", 0f, refreshRate);
        }

        /// <summary>
        /// Fills each of the entrys with values from the supplied array.
        /// </summary>
        public void DisplayHighscores(JSONArray entries)
        {
			statusText.text = "";
            for (int i = 0; i < entries.Count; i++)
            {
                int rank = i + 1;
                HighscoreEntry entry = Instantiate(entryPrefab, entriesParent).GetComponent<HighscoreEntry>();
                entry.Populate(rank + ".", "", "");
                entry.SetTextColourBasedOnRank(rank);

                if (entries.Count > i)
                {
                    entry.Populate(rank + ".", entries[i]["name"], entries[i]["score"]);
                    entry.SetTextColourBasedOnRank(rank);
                }
            }
        }

        /// <summary>
        /// Updates the status text.
        /// </summary>
        public void DisplayError(string message)
        {
            statusText.text = message;
            statusText.color = Color.red;
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
            foreach (Transform child in entriesParent)
            {
                Destroy(child);
            }
        }
    }
}
