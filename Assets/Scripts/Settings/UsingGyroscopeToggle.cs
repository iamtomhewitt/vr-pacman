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
		/// <summary>
		/// Called from a Toggle.
		/// </summary>
		public void SetUsingGyro(Toggle toggle)
		{
			GameSettingsManager.instance.SetUsingGyro(toggle.isOn);
		}
	}
}
