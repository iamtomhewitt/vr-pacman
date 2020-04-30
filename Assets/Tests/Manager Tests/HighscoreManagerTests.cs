using System.Collections;
using System.Collections.Generic;
using Manager;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Utility;

namespace Tests
{
    public class HighscoreManagerTests
    {
		private HighscoreManager manager;
		private float WAIT_TIME = 0.001f;

		[SetUp]
		public void Setup()
		{
			PlayerPrefs.DeleteAll();
			manager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Managers/Highscore Manager")).GetComponent<HighscoreManager>();
		}

		[TearDown]
		public void Teardown()
		{
			Object.Destroy(manager.gameObject);
		}

		[UnityTest]
		public IEnumerator SaveWorksIfNewHighscoreAchieved()
		{
			// Set a score first
			PlayerPrefs.SetInt(Constants.PLAYER_PREFS_HIGHSCORE_KEY, 50);
			manager.SaveLocalHighscore(100);
			Assert.AreEqual(100, manager.GetLocalHighscore());
			Assert.AreEqual(0, PlayerPrefs.GetInt(Constants.ALREADY_UPLOADED_KEY)); // Also check that uploaded is set to no (0)
			yield return new WaitForSeconds(WAIT_TIME);
		}

		[UnityTest]
		public IEnumerator HighscoreNotSavedIfScoreIsLessThanHighscore()
		{
			// Set a score first and already uploaded
			PlayerPrefs.SetInt(Constants.PLAYER_PREFS_HIGHSCORE_KEY, 100);
			PlayerPrefs.SetInt(Constants.ALREADY_UPLOADED_KEY, 1);
			manager.SaveLocalHighscore(50);
			Assert.AreNotEqual(50, manager.GetLocalHighscore());
			Assert.AreEqual(1, PlayerPrefs.GetInt(Constants.ALREADY_UPLOADED_KEY)); // Also check that uploaded is set to no (0)
			yield return new WaitForSeconds(WAIT_TIME);
		}

		[UnityTest]
		public IEnumerator SaveWorksIfNewGame()
		{
			manager.SaveLocalHighscore(100);
			Assert.AreEqual(100, manager.GetLocalHighscore());
			Assert.AreEqual(0, PlayerPrefs.GetInt(Constants.ALREADY_UPLOADED_KEY)); // Also check that uploaded is set to no (0)
			yield return new WaitForSeconds(WAIT_TIME);
		}
    }
}
