﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pacman
{
    public class PacmanScore : MonoBehaviour
    {
        public TextMesh scoreText;
        public int score = 0;

        public void AddScore(int scoreToAdd)
        {
            score += scoreToAdd;
            scoreText.text = score.ToString();
        }
    }
}