using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	}
}
