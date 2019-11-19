﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Utility;
using SimpleJSON;

namespace Manager
{
	public class HighscoreManager : MonoBehaviour
	{
		private const string privateCode = "hERKFM6pT0qBOb8ZvypNDQmyok7nHxTkWkM2BFIe7hxQ";
		private const string publicCode = "5d810f91d1041303ecafee9f";
		private const string url = "http://dreamlo.com/lb/";

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
			HighscoreDisplayHelper displayHelper = FindObjectOfType<HighscoreDisplayHelper>();
			UnityWebRequest request = UnityWebRequest.Post(url + privateCode + "/add/" + username + "/" + score, "");
			yield return request.SendWebRequest();

			if (!request.downloadHandler.text.StartsWith("ERROR"))
			{
				Debug.Log("Upload successful! " + request.responseCode);
				PlayerPrefs.SetInt(Constants.ALREADY_UPLOADED_KEY, 1);
			}
			else
			{
				Debug.Log("Error uploading: " + request.downloadHandler.text);
				displayHelper.ClearEntries();
				displayHelper.DisplayError("Could not upload score. Please try again later.\n\n" + request.downloadHandler.text);
			}
		}

		public void DownloadHighscores()
		{
			StartCoroutine(DownloadHighscoresRoutine());
		}

		/// <summary>
		/// Downloads the highscores from Dreamlo.
		/// </summary>
		private IEnumerator DownloadHighscoresRoutine()
		{
			HighscoreDisplayHelper displayHelper = FindObjectOfType<HighscoreDisplayHelper>();

			if (Application.internetReachability == NetworkReachability.NotReachable)
			{
				displayHelper.DisplayError("No internet connection.");
				yield break;
			}

			UnityWebRequest request = UnityWebRequest.Get(url + publicCode + "/json/0/10");
			yield return request.SendWebRequest();

			if (!request.downloadHandler.text.StartsWith("ERROR"))
			{
				JSONNode json = JSON.Parse(request.downloadHandler.text);
				JSONArray entries = json["dreamlo"]["leaderboard"]["entry"].AsArray;
				displayHelper.DisplayHighscores(entries);
			}
			else
			{
				Debug.Log("Error downloading: " + request.downloadHandler.text);
				displayHelper.DisplayError("Could not download highscores. Please try again later.\n\n" + request.downloadHandler.text);
			}
		}
	}
}
