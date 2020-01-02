﻿using UnityEngine;
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
			UpdateAccelerometerSettings(GetComponent<Slider>()); 
		}

		/// <summary>
		/// Called from a Slider.
		/// </summary>
		public void UpdateAccelerometerSettings(Slider slider)
		{
			GameSettingsManager.instance.SetAccelerometerSensitivity(slider.value);
		}
	}
}