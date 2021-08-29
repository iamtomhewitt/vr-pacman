using Manager;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using Utility;

/// <summary>
/// Helper class for actions performed in the main menu.
/// </summary>
public class MainMenuHelper : MonoBehaviour
{
	[SerializeField] private GameObject countdownUi;
	[SerializeField] private GameObject halloweenUi;
	[SerializeField] private GameObject mainMenuUi;
	[SerializeField] private GameObject normalParticleSystem;
	[SerializeField] private Text countdownText;

	private void Start()
	{
		if (Utilities.isOctober())
		{
			halloweenUi.SetActive(true);
			normalParticleSystem.SetActive(false);
		}

		mainMenuUi.SetActive(true);
		countdownUi.SetActive(false);
	}

	public void StartGame(string sceneName)
	{
		StartCoroutine(StartGameRoutine(sceneName));
	}

	private IEnumerator StartGameRoutine(string sceneName)
	{
		AudioManager audio = AudioManager.instance;

		// Have to play the sound here, because the button gets deactivated on press so the sound will not play
		audio.Play(SoundNames.BUTTON_PRESS);

		mainMenuUi.SetActive(false);
		countdownUi.SetActive(true);

		FindObjectOfType<Utilities>().RotateScreenLandscape();

		for (int i = 10; i >= 0; i--)
		{
			countdownText.text = "Put on your VR headset!\n" + i.ToString();
			yield return new WaitForSeconds(1f);
		}

		audio.Pause(SoundNames.MENU_MUSIC);

		SceneManager.LoadScene(sceneName);
	}
}