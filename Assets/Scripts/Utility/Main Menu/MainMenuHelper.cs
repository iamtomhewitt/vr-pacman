using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Utility;

/// <summary>
/// Helper class for actions performed in the main menu.
/// </summary>
public class MainMenuHelper : MonoBehaviour
{
	[SerializeField] private GameObject mainMenuUi;
	[SerializeField] private GameObject countdownUi;
	[SerializeField] private Text countdownText;

	private void Start()
	{
		mainMenuUi.SetActive(true);
		countdownUi.SetActive(false);
	}

	public void StartGame(string sceneName)
	{
		StartCoroutine(StartGameRoutine(sceneName));
	}

	private IEnumerator StartGameRoutine(string sceneName)
	{
		mainMenuUi.SetActive(false);
		countdownUi.SetActive(true);

		FindObjectOfType<Utilities>().RotateScreenLandscape();

		int countdownTime = 10;

		for (int i = countdownTime; i >= 0; i--)
		{
			countdownText.text = "Put on your VR headset!\n" + i.ToString();
			yield return new WaitForSeconds(1f);
		}

		SceneManager.LoadScene(sceneName);
	}
}
