using UnityEngine;

namespace Pacman
{
	public class PacmanScore : MonoBehaviour
    {
        [SerializeField] private int score = 0;

		public static PacmanScore instance;

		private void Awake()
		{
			instance = this;
		}

		public void AddScore(int amount)
        {
            score += amount;
			PacmanHud.instance.SetScoreText(score.ToString());
        }

		public int GetScore()
		{
			return score;
		}
    }
}
