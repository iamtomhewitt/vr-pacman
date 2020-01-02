using UnityEngine;

namespace Manager
{
	public class GameSettingsManager : MonoBehaviour
	{
		[SerializeField] private float sensitivity = 5f;

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
			this.sensitivity = sensitivity;
		}

		public float GetSensitivity()
		{
			return sensitivity;
		}
	}
}