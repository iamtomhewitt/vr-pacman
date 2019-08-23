using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pacman
{
    public class PacmanScore : MonoBehaviour
    {
		[SerializeField] private TextMesh scoreText;
        [SerializeField] private int score = 0;

		public static PacmanScore instance;

		private void Awake()
		{
			instance = this;
		}

		public void AddScore(int scoreToAdd)
        {
            score += scoreToAdd;
            scoreText.text = score.ToString();
        }

		public int GetScore()
		{
			return score;
		}
    }
}
