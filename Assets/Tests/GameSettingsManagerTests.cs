using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine;
using Manager;

namespace Tests
{
	public class GameSettingsManagerTests
	{
		private GameSettingsManager manager;
		private float WAIT_TIME = 0.001f;

		[SetUp]
		public void Setup()
		{
			manager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Managers/Game Settings Manager")).GetComponent<GameSettingsManager>();
		}

		[TearDown]
		public void Teardown()
		{
			Object.Destroy(manager.gameObject);
		}

		[UnityTest]
		public IEnumerator GameSettingsManagerStartsWithDefaultSensitivityOfFive()
		{
			Assert.AreEqual(5f, manager.GetSensitivity());
			yield return new WaitForSeconds(WAIT_TIME);
		}

		[UnityTest]
		public IEnumerator GameSettingsManagerChangeSensitivityWorks()
		{
			manager.SetSensitivity(3f);
			Assert.AreEqual(3f, manager.GetSensitivity());
			yield return new WaitForSeconds(WAIT_TIME);
		}
	}
}
