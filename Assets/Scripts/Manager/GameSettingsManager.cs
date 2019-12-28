using UnityEngine;

namespace Manager
{
	public class GameSettingsManager : MonoBehaviour
	{
		[SerializeField] private bool useGyro;
		[SerializeField] private float accelerometerSensitivity;

		public static GameSettingsManager instance;

		private void Awake()
		{
			if (instance)
			{
				DestroyImmediate(this.gameObject);
			}
			else
			{
				DontDestroyOnLoad(this.gameObject);
				instance = this;
				useGyro = SystemInfo.supportsGyroscope;
			}
		}

		public void SetAccelerometerSensitivity(float sensitivity)
		{
			accelerometerSensitivity = sensitivity;
		}

		public float GetAccelerometerSensitivity()
		{
			return accelerometerSensitivity;
		}

		public void SetUsingGyro(bool gyro)
		{
			useGyro = gyro;
		}

		public bool IsUsingGyro()
		{
			return useGyro;
		}
	}
}