namespace Highscores
{
	public class Highscore
	{
		private string name;
		private string date;
		private int score;

		public Highscore(string name, int score, string date)
		{
			this.name = name;
			this.score = score;
			this.date = date;
		}

		public override string ToString()
		{
			return "{name=" + name + ", " +
				"score=" + score + ", " +
				"date=" + date + "}";
		}

		public string GetName()
		{
			return name;
		}

		public int GetScore()
		{
			return score;
		}

		public string GetDate()
		{
			return date;
		}
	}
}