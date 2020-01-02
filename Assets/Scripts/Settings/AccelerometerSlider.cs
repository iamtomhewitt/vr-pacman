using UnityEngine;
using UnityEngine.UI;
using Manager;

namespace Settings
{
	/// <summary>
	/// A script to control behaviours from the accelerometer sensitivity slider on the settings scene.
	/// </summary>
	public class AccelerometerSlider : MonoBehaviour
	{
		private void Start()
		{
			Slider slider = GetComponent<Slider>();
			slider.value = GameSettingsManager.instance.GetSensitivity();
			UpdateAccelerometerSettings(slider); 
		}

		/// <summary>
		/// Called from a Slider.
		/// </summary>
		public void UpdateAccelerometerSettings(Slider slider)
		{
			GameSettingsManager.instance.SetSensitivity(slider.value);
		}
	}
}
