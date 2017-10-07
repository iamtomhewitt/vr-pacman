using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanScore : MonoBehaviour 
{
    public TextMesh[] scoreTexts;

    public int score = 0;

    public void SetScoreText(int scoreToAdd)
    {
        score += scoreToAdd;

        for (int i = 0; i < scoreTexts.Length; i++)
        {
            scoreTexts[i].text  = "SCORE "+score.ToString();
        }
    }
}
