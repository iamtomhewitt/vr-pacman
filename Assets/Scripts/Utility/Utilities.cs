using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utility
{
    public class Utilities : MonoBehaviour
    {
        public int startTime;
        public Text countdownText;

        public void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }

        public void LoadSceneWithCountdown(string name)
        {
            StartCoroutine(Countdown(name));
        }

        IEnumerator Countdown(string name)
        {
            for (int i = startTime; i >= 0; i--)
            {
                countdownText.text = "PUT ON YOUR VR HEADSET\n"+i.ToString();
                yield return new WaitForSeconds(1f);
            }

            SceneManager.LoadScene(name);
        }

        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }

        public void Quit()
        {
            Application.Quit();
        }

		public void RotateScreenLandscape()
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
		}

		public void RotateScreenPortrait()
		{
			Screen.orientation = ScreenOrientation.Portrait;
		}

		/// <summary>
		/// Used to activate VR mode with Google VR.
		/// </summary>
		public void ActivateVR()
		{
			StartCoroutine(ActivateVRRoutine());
		}

		private IEnumerator ActivateVRRoutine()
		{
			UnityEngine.XR.XRSettings.LoadDeviceByName("Cardboard");
			yield return null;
			UnityEngine.XR.XRSettings.enabled = true;
		}

		/// <summary>
		/// Used to deactivate VR mode with Google VR.
		/// Also makes the screen portrait.
		/// </summary>
		public void DeActivateVR()
		{
			StartCoroutine(DeActivateVRRoutine());
		}

		public IEnumerator DeActivateVRRoutine()
		{
			UnityEngine.XR.XRSettings.LoadDeviceByName("");
			yield return null;
			UnityEngine.XR.XRSettings.enabled = false;
			Screen.orientation = ScreenOrientation.Portrait;
		}
	}
}
