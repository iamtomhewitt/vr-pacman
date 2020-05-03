using Ghosts;
using Manager;
using NUnit.Framework;
using Pacman;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine;

namespace Tests
{
	public class GameObjectManagerTest
	{
		private GameObject pacman;
		private GameObject ghostPaths;
		private GameObject food;
		private GameObject powerup;

		private Ghost ghost;

		private AudioManager audio;

		private GameObjectManager goManager;

		private float WAIT_TIME = 0.1f;

		[SetUp]
		public void Setup()
		{
			audio = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Managers/Audio Manager")).GetComponent<AudioManager>();
			food = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Pickups/Food"));
			ghost = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Ghost")).GetComponent<Ghost>();
			ghostPaths = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Ghost Paths"));
			goManager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Managers/Game Object Manager")).GetComponent<GameObjectManager>();
			pacman = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Pacman"));
			powerup = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Pickups/Powerup"));
		}

		[TearDown]
		public void Teardown()
		{
			Object.Destroy(audio.gameObject);
			Object.Destroy(food.gameObject);
			Object.Destroy(ghost.gameObject);
			Object.Destroy(ghostPaths.gameObject);
			Object.Destroy(goManager.gameObject);
			Object.Destroy(pacman.gameObject);
			Object.Destroy(powerup.gameObject);
		}

		[UnityTest]
		public IEnumerator CanMakeGhostsEdible()
		{
			goManager.MakeGhostsEdible();
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.True(ghost.IsEdible());
		}

		[UnityTest]
		public IEnumerator CanMakeGhostsEdibleButNotIfTheyAreRunningHome()
		{
			ghost.RunHome();
			goManager.MakeGhostsEdible();
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.False(ghost.IsEdible());
		}

		[UnityTest]
		public IEnumerator CanMoveEntities()
		{
			PacmanMovement pm = pacman.GetComponent<PacmanMovement>();
			goManager.StartMovingEntities();
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreEqual(pm.GetOriginalSpeed(), pm.GetSpeed());
			Assert.AreEqual(ghost.GetSpeed(), ghost.GetMovingSpeed());
		}

		[UnityTest]
		public IEnumerator CanStopEntities()
		{
			PacmanMovement pm = pacman.GetComponent<PacmanMovement>();
			goManager.StopMovingEntities();
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreEqual(0f, pm.GetSpeed());
			Assert.AreEqual(0f, ghost.GetSpeed());
		}

		[UnityTest]
		public IEnumerator ResetPositionsWork()
		{
			Vector3 pacmanPos = pacman.transform.position;
			Vector3 ghostPos = ghost.transform.position;
			goManager.StartMovingEntities();
			yield return new WaitForSeconds(WAIT_TIME);
			goManager.StopMovingEntities();
			goManager.ResetEntityPositions();
			Assert.AreEqual(pacmanPos, pacman.transform.position);
			Assert.AreEqual(ghostPos, ghost.transform.position);
		}

		[UnityTest]
		public IEnumerator FoodIsActivated()
		{
			food.SetActive(false);
			goManager.ActivateFood();
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.True(food.activeSelf);
		}

		[UnityTest]
		public IEnumerator PowerupsAreActivated()
		{
			powerup.SetActive(false);
			goManager.ActivatePowerups();
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.True(powerup.activeSelf);
		}

		[UnityTest]
		public IEnumerator CanCountFoodCorrectly()
		{
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreEqual(1f, goManager.CountFood());
		}

		[UnityTest]
		public IEnumerator CanSpawnCherry()
		{
			// Populate more food so the cherry can spawn
			for (int i = 0; i <= 41; i++)
			{
				MonoBehaviour.Instantiate(food);
			}
			goManager.CountFood();
			goManager.SpawnCherry();
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.IsNotNull(GameObject.FindGameObjectWithTag("Cherry"));
		}
	}
}