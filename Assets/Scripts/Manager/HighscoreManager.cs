using System.Collections;
using UnityEngine;
using Utility;
using UnityEngine.Networking;

namespace Manager
{
	public class HighscoreManager : MonoBehaviour
    {
        private Highscore[] highscoresList;

        private const string privateCode    = "YHc0fAncbE-2ieJokE0NIAB9ZEzv8l10WovytuLwzMAw";
        private const string publicCode     = "5ad0d623d6024519e0be327e";
        private const string url            = "http://dreamlo.com/lb/";

        public static HighscoreManager instance;

		private void Awake()
		{
			if (instance)
			{
				DestroyImmediate(this.gameObject);
			}
			else
			{
				DontDestroyOnLoad(this.gameObject);
				instance = this;
			}

			//UploadNewHighscore("Tom (The Developer)", 4500);
		}

		/// <summary>
		/// Saves the highscore to PlayerPrefs.
		/// </summary>
		public void SaveLocalHighscore(int score)
        {
            int currentHighscore = GetLocalHighscore();

            if (score > currentHighscore)
            {
                Debug.Log("New highscore of " + score + "! Saving...");
                PlayerPrefs.SetInt(Constants.PLAYER_PREFS_HIGHSCORE_KEY, score);

                // Player has got a new highscore, which hasn't been uploaded yet, so set it to false (0)
                PlayerPrefs.SetInt(Constants.ALREADY_UPLOADED_KEY, 0);
            }
        }

        public int GetLocalHighscore()
        {
            return PlayerPrefs.GetInt(Constants.PLAYER_PREFS_HIGHSCORE_KEY);
        }

		/// <summary>
		/// Uploads a new highscore to Dreamlo.
		/// </summary>
        public void UploadNewHighscore(string username, int score)
        {
            StartCoroutine(UploadNewHighscoreRoutine(username, score));
        }

		/// <summary>
		/// Routine for uploading a highscore to Dreamlo.
		/// </summary>
        private IEnumerator UploadNewHighscoreRoutine(string username, int score)
        {
            string id = System.DateTime.Now.ToString("MMddyyyyhhmmss");
			UnityWebRequest request = UnityWebRequest.Post(url + privateCode + "/add/" + UnityWebRequest.EscapeURL(id+username) + "/" + score, "");
            yield return request.SendWebRequest();

			if (!request.downloadHandler.text.StartsWith("ERROR"))
			{
                Debug.Log("Upload successful! " + request.responseCode);
                PlayerPrefs.SetInt(Constants.ALREADY_UPLOADED_KEY, 1);
            }
            else
            {
                Debug.Log("Error uploading: " + request.error);
            }
        }

		public Highscore[] GetHighscoresList()
		{
			return highscoresList;
		}

		/// <summary>
		/// Downloads the highscores from Dreamlo.
		/// </summary>
        public IEnumerator DownloadHighscores()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
				FindObjectOfType<HighscoreDisplayHelper>().ShowNoInternetConnection();
                yield break;
            }

			UnityWebRequest request = UnityWebRequest.Get(url + publicCode + "/pipe/0/10");
			yield return request.SendWebRequest();

			if (!request.downloadHandler.text.StartsWith("ERROR"))
			{
				highscoresList = ToHighScoreList(request.downloadHandler.text);
			}
			else
			{
				Debug.Log("Error downloading: " + request.error);
			}
        }

		/// <summary>
		/// Coverts a text stream into an array of Highscores.
		/// </summary>
        private Highscore[] ToHighScoreList(string textStream)
        {
            string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

			Highscore[] list = new Highscore[entries.Length];

            for (int i = 0; i < entries.Length; i++)
            {
                string[] entryInfo = entries[i].Split(new char[] { '|' });

                // Get the username from online
                string usr = entryInfo[0];

                // Replace with spaces
                string username = usr.Replace('+', ' ');

                // Dont want the date included in the username, so substring itself to show only the username
                username = username.Substring(14, (username.Length-14));

                int score = int.Parse(entryInfo[1]);

                list[i] = new Highscore(username, score);

                //print(highscoresList[i].username + ": " + highscoresList[i].score + " Date: "+date);
            }

			return list;
        }
    }

	/// <summary>
	/// Data class for a highscore entry.
	/// </summary>
    public struct Highscore
    {
        public string username;
        public int score;

        public Highscore(string username, int score)
        {
            this.username = username;
            this.score = score;
        }
    }
}
