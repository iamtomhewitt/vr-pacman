using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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

		public void OpenPrivacyPolicy()
		{
			OpenURL("https://iamtomhewitt.github.io/website/#/vr-pac-mac-privacy-policy");
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

		public static string StripNonLatinLetters(string name)
		{
			string newName = "";
			foreach (char c in name)
			{
				int code = (int)c;
				
				// Only english numbers, letters, symbols
				if ((code >= 32 && code <= 127))
				{
					newName += c;
				}
			}

			return newName;
		}
	}
}
