using Highscores;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;
using UnityEngine;
using Utility;

namespace Manager
{
	public class HighscoreManager : MonoBehaviour
	{
		public static HighscoreManager instance;
		private string datetime;

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
				PlayerPrefs.SetInt(Constants.ALREADY_UPLOADED_KEY, Constants.NO);
			}
		}

		public int GetLocalHighscore()
		{
			return PlayerPrefs.GetInt(Constants.PLAYER_PREFS_HIGHSCORE_KEY);
		}

		/// <summary>
		/// Uploads a new highscore to Firebase.
		/// </summary>
		public void UploadNewHighscore(string username, int score)
		{
			StartCoroutine(UploadNewHighscoreRoutine(username, score));
		}

		/// <summary>
		/// Routine for uploading a highscore to Firebase.
		/// </summary>
		private IEnumerator UploadNewHighscoreRoutine(string username, int score)
		{
			yield return GetDateFromInternet();

			HighscoreDisplayHelper displayHelper = FindObjectOfType<HighscoreDisplayHelper>();

			string url = Config.instance.GetConfig()["firebase"];

			JSONObject body = new JSONObject();
			body.Add("date", this.datetime);
			body.Add("name", username);
			body.Add("score", score);

			UnityWebRequest request = UnityWebRequest.PostWwwForm(url, "POST");
			byte[] bytes = Encoding.UTF8.GetBytes(body.ToString());

			request.uploadHandler = new UploadHandlerRaw(bytes);
			request.downloadHandler = new DownloadHandlerBuffer();
			request.SetRequestHeader("Content-Type", "application/json");

			yield return request.SendWebRequest();

			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.Log("Error uploading: " + request.downloadHandler.text);
				displayHelper.ClearEntries();
				displayHelper.DisplayError("Could not upload score. Please try again later.\n\n" + request.downloadHandler.text);
			}
			else
			{
				Debug.Log("Upload successful! " + request.responseCode);
				PlayerPrefs.SetInt(Constants.ALREADY_UPLOADED_KEY, 1);
			}
		}

		public void DownloadHighscores()
		{
			StartCoroutine(DownloadHighscoresRoutine());
		}

		/// <summary>
		/// Downloads the highscores from Firebase.
		/// </summary>
		private IEnumerator DownloadHighscoresRoutine()
		{
			HighscoreDisplayHelper displayHelper = FindObjectOfType<HighscoreDisplayHelper>();

			if (Application.internetReachability == NetworkReachability.NotReachable)
			{
				displayHelper.DisplayError("No internet connection.");
				yield break;
			}

			string url = Config.instance.GetConfig()["firebase"];

			UnityWebRequest request = UnityWebRequest.Get(url);
			yield return request.SendWebRequest();

			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.Log("Error downloading: " + request.downloadHandler.text);
				displayHelper.DisplayError("Could not download highscores. Please try again later.\n\n" + request.downloadHandler.text);
			}
			else
			{
				JSONNode json = JSON.Parse(request.downloadHandler.text);
				List<Highscore> highscores = new List<Highscore>();

				foreach (JSONNode i in json)
				{
					highscores.Add(new Highscore(i["name"], i["score"], i["date"]));
				}

				highscores.Sort((p1, p2) => p2.GetScore().CompareTo(p1.GetScore()));

				displayHelper.DisplayHighscores(highscores);
			}
		}

		private IEnumerator GetDateFromInternet() 
		{
			UnityWebRequest request = UnityWebRequest.Get("https://worldtimeapi.org/api/timezone/Europe/London");
			yield return request.SendWebRequest();
			JSONNode json = JSON.Parse(request.downloadHandler.text);
			this.datetime = json["datetime"];
		}
	}
}
