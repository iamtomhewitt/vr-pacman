using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// A data class used for helping display the name, rank and score of the downloaded highscores in the Highscore menu.
/// Used for the front end only.
/// </summary>
public class HighscoreEntry : MonoBehaviour
{
	public Text rank;
	public Text name;
	public Text score;

	public void Populate(string rank, string name, string score)
	{
		this.rank.text = rank;
		this.name.text = name;
		this.score.text = score;
	}
}
