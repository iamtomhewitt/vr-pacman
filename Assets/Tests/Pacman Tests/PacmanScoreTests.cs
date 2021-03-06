﻿using NUnit.Framework;
using Pacman;
using UnityEngine;

namespace Tests
{
	public class PacmanScoreTests
	{
		private PacmanScore pacmanScore;

		[SetUp]
		public void Setup()
		{
			pacmanScore = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Pacman")).GetComponent<PacmanScore>();
			pacmanScore.Start();
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