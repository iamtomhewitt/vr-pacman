using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Manager;

namespace Utility
{
	/// <summary>
	/// A set of utility methods to be called anywhere in the game.
	/// </summary>
	public class Utilities : MonoBehaviour
    { 
		/// <summary>
		/// Loads a specific scene.
		/// </summary>
        public void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }
		
		/// <summary>
		/// Opens a URL on the internet.
		/// </summary>
        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }

		/// <summary>
		/// Exits the game.
		/// </summary>
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

		public void UpdateAccelerometerSettings(Slider slider)
		{
			GameSettingsManager.instance.SetAccelerometerSensitivity(slider.value);
		}
	}
}
