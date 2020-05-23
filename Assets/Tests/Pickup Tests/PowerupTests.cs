using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine;
using Utility;

namespace Tests
{
	public class PowerupTests
	{
		private Powerup powerup;
		private float WAIT_TIME = 0.05f;

		[SetUp]
		public void Setup()
		{
			powerup = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Pickups/Powerup")).GetComponent<Powerup>();
		}

		[TearDown]
		public void Teardown()
		{
			Object.Destroy(powerup.gameObject);
		}

		[UnityTest]
		public IEnumerator PowerupRotates()
		{
			Vector3 rotation = powerup.transform.rotation.eulerAngles;
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreNotEqual(rotation, powerup.transform.rotation.eulerAngles);
		}
	}
}