using Manager;
using UnityEngine.UI;
using UnityEngine;

namespace Settings
{
	/// <summary>
	/// A script to control behaviours from the sensitivity slider on the settings scene.
	/// </summary>
	public class SensitivitySlider : MonoBehaviour
	{
		private void Start()
		{
			Slider slider = GetComponent<Slider>();
			slider.value = GameSettingsManager.instance.GetSensitivity();
			UpdateSensitivityInGameSettings(slider); 
		}

		/// <summary>
		/// Called from a Slider.
		/// </summary>
		public void UpdateSensitivityInGameSettings(Slider slider)
		{
			GameSettingsManager.instance.SetSensitivity(slider.value);
		}
	}
}
