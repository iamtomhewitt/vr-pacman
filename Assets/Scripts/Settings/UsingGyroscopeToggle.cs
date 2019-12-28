using UnityEngine;
using UnityEngine.UI;
using Manager;

namespace Setting
{
	/// <summary>
	/// A script to control behaviours from the Toggle Gyroscope button on the settings scene.
	/// </summary>
	public class UsingGyroscopeToggle : MonoBehaviour
	{
		[SerializeField] private GameObject accelerometerSettings;
		[SerializeField] private GameObject gyroscopeSettings;

		/// <summary>
		/// Called from a Toggle.
		/// </summary>
		public void SetUsingGyro(Toggle toggle)
		{
			GameSettingsManager.instance.SetUsingGyro(toggle.isOn);
		}

		/// <summary>
		/// Shows the correct settings based on whether we are using gyroscope.
		/// </summary>
		public void ShowSettings()
		{
			bool usingGyro = GameSettingsManager.instance.IsUsingGyro();
			gyroscopeSettings.SetActive(usingGyro);
			accelerometerSettings.SetActive(!usingGyro);
		}
	}
}
