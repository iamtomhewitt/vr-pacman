using Manager;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Utility;

namespace Highscores
{
	public class HighscoreDisplayHelper : MonoBehaviour
	{
		[SerializeField] private GameObject halloweenUi;
		[SerializeField] private GameObject christmasUi;
		[SerializeField] private GameObject uploadModal;
		[SerializeField] private HighscoreEntry entryPrefab;
		[SerializeField] private Text localHighscoreText;
		[SerializeField] private Text statusText;
		[SerializeField] private Transform entriesParent;
		[SerializeField] private GameObject normalParticleSystem;

		private HighscoreManager highscoreManager;
		private float refreshRate = 60f;

		private void Start()
		{
			highscoreManager = HighscoreManager.instance;
			localHighscoreText.text = highscoreManager.GetLocalHighscore().ToString();

			statusText.text = "Downloading highscores...";
			statusText.color = Color.green;

			if (Utilities.isOctober())
			{
				halloweenUi.SetActive(true);
				normalParticleSystem.SetActive(false);
			}

			if (Utilities.isDecember())
			{
				christmasUi.SetActive(true);
				normalParticleSystem.SetActive(false);
			}

			InvokeRepeating("RefreshHighscores", 0f, refreshRate);
		}

		public void DisplayHighscores(List<Highscore> highscores)
		{
			ClearEntries();
			statusText.text = "";

			for (int i = 0; i < highscores.Count; i++)
			{
				int rank = i + 1;
				HighscoreEntry entry = Instantiate(entryPrefab, entriesParent).GetComponent<HighscoreEntry>();
				entry.Populate(rank + ".", "", "");
				entry.SetTextColourBasedOnRank(rank);

				if (highscores.Count > i)
				{
					entry.Populate(rank + ".", highscores[i].GetName(), highscores[i].GetScore().ToString());
					entry.SetTextColourBasedOnRank(rank);
				}
			}
		}

		public void DisplayError(string message)
		{
			statusText.text = message;
			statusText.color = Color.red;
		}

		private void RefreshHighscores()
		{
			highscoreManager.DownloadHighscores();
		}

		/// <summary>
		/// Called from a Unity button, uploads the highscore to Firebase.
		/// </summary>
		public void Upload(InputField usernameInputField)
		{
			Text placeholderText = usernameInputField.placeholder.GetComponent<Text>();

			string formatted = Utilities.StripNonLatinLetters(usernameInputField.text);

			if (highscoreManager.GetLocalHighscore() <= 0)
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
			else if (string.IsNullOrEmpty(formatted))
			{
				usernameInputField.text = "";
				placeholderText.text = Constants.INVALID_NAME;
			}
			else
			{
				highscoreManager.UploadNewHighscore(formatted, highscoreManager.GetLocalHighscore());
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

		public void ShowUploadModal()
		{
			uploadModal.SetActive(true);
		}

		public void HideUploadModal()
		{
			uploadModal.SetActive(false);
		}
	}
}
