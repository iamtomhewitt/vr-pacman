using Ghosts;
using Manager;
using NUnit.Framework;
using Pacman;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine;
using Utility;

namespace Tests
{
	public class PacmanScoreTests
	{
		private PacmanScore pacmanScore;
		
		private float WAIT_TIME = 0.1f;

		[SetUp]
		public void Setup()
		{
			pacmanScore = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Pacman")).GetComponent<PacmanScore>();
		}

		[TearDown]
		public void Teardown()
		{
			Object.Destroy(pacmanScore.gameObject);
		}

		[Test]
		public void AddingScoreWorks()
		{
			pacmanScore.AddScore(50);
			Assert.AreEqual(50, pacmanScore.GetScore());
		}
	}
}