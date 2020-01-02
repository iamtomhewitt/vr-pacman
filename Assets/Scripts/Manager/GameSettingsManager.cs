using UnityEngine;

namespace Manager
{
	public class GameSettingsManager : MonoBehaviour
	{
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
			}
		}

		public void SetSensitivity(float sensitivity)
		{
			accelerometerSensitivity = sensitivity;
		}

		public float GetSensitivity()
		{
			return accelerometerSensitivity;
		}
	}
}