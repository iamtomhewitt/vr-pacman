using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A data class used for helping display the name, rank and score of the downloaded highscores in the Highscore menu.
/// Used for the front end only.
/// </summary>
namespace Utility
{
    public class HighscoreEntry : MonoBehaviour
    {
        [SerializeField] private Text rank;
        [SerializeField] private Text username;
        [SerializeField] private Text score;

        private string[] devColours = new string[] { "#FF3132", "#00BDDF" };

        public void Populate(string rank, string name, string score)
        {
            this.rank.text = rank;
            this.username.text = name;
            this.score.text = score;

            if (name.Equals("Tom (The Developer)"))
            {
                username.text = ApplyDevColours(name);
            }
        }

        private string ApplyDevColours(string username)
        {
            string newUsername = "";
            int index = 0;
            foreach (char c in username)
            {
                newUsername += ColourCharacter(c, devColours[index]);
                if (c != ' ') index = ++index > devColours.Length - 1 ? 0 : index;
            }
            return newUsername;
        }

        private string ColourCharacter(char s, string colour)
        {
            return "<color=" + colour + ">" + s + "</color>";
        }
    }
}