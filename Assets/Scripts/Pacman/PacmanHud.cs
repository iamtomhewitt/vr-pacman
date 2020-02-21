using UnityEngine;

namespace Pacman
{
	/// <summary>
	/// A class for accessing the heads up display of the player.
	/// </summary>
	public class PacmanHud : MonoBehaviour
	{
		[SerializeField] private GameObject[] lifeSprites;
		[SerializeField] private TextMesh statusText;
		[SerializeField] private TextMesh scoreText;

		private Debugger debugger;

		public static PacmanHud instance;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			debugger = GetComponent<Debugger>();
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
			debugger.Info("removing life from HUD");
			lifeSprites[index].SetActive(false);
		}
	}
}