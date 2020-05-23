using UnityEngine;

namespace Pacman
{
	public class PacmanScore : MonoBehaviour
	{
		[SerializeField] private int score = 0;

		private PacmanHud hud;

		public void Start()
		{
			hud = GetComponent<PacmanHud>();
		}

		public void AddScore(int amount)
		{
			score += amount;
			hud.SetScoreText(score.ToString());
		}

		public int GetScore()
		{
			return score;
		}
	}
}
