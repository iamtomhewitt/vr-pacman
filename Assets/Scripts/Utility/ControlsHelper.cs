using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utility
{
	/// <summary>
	/// Used in the controls scene to show a set of controls based on if the device has gyroscope support.
	/// </summary>
	public class ControlsHelper : MonoBehaviour
	{
		public GameObject accelerometerControls;
		public GameObject gyroscopeControls;
		public GameObject[] gameObjectsToHide;
		public Text countdownText;

		public bool useGyro;
		public int countdownTime;

		private void Start()
		{
			useGyro = SystemInfo.supportsGyroscope;

			if (useGyro)
			{
				accelerometerControls.SetActive(false);
			}
			else
			{
				gyroscopeControls.SetActive(false);
			}
		}

		public void HideGameObjects()
		{
			foreach (GameObject g in gameObjectsToHide)
			{
				g.SetActive(false);
			}
		}

		public void LoadSceneWithCountdown(string scene)
		{
			StartCoroutine(LoadRoutine(scene));
		}

		private IEnumerator LoadRoutine(string name)
		{
			for (int i = countdownTime; i >= 0; i--)
			{
				countdownText.text = "PUT ON YOUR VR HEADSET\n" + i.ToString();
				yield return new WaitForSeconds(1f);
			}

			SceneManager.LoadScene(name);
		}
	}
}
