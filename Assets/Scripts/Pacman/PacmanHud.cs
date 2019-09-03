using UnityEngine;

namespace Pacman
{
	/// <summary>
	/// A class for accessing the heads up display of the player.
	/// </summary>
	public class PacmanHud : MonoBehaviour
	{
		public GameObject[] lifeSprites;

		[SerializeField] private TextMesh statusText;
		[SerializeField] private TextMesh scoreText;

		public static PacmanHud instance;

		private void Awake()
		{
			instance = this;
		}

		public void SetStatusText(string message)
		{
			statusText.text = message;
		}

		public void SetScoreText(string message)
		{
			scoreText.text = message;
		}

		public void RemoveLife(int index)
		{
			lifeSprites[index].SetActive(false);
		}
	}
}
