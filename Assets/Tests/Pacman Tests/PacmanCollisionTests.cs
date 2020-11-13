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
	public class PacmanCollisionTests
	{
		private AudioManager audio;
		private GameEventManager geManager;
		private GameObject food;
		private GameObject ghostPaths;
		private GameObject pacman;
		private GameObject teleporter;
		private GameObjectManager goManager;
		private Ghost ghost;
		private PacmanCollision pacmanCollision;
		private PacmanMovement pacmanMovement;
		private PacmanScore pacmanScore;
		private Powerup powerup;
		private float WAIT_TIME = 0.1f;

		[SetUp]
		public void Setup()
		{
			audio = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Managers/Audio Manager")).GetComponent<AudioManager>();
			food = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Pickups/Food"));
			geManager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Managers/Game Event Manager")).GetComponent<GameEventManager>();
			ghost = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Ghost")).GetComponent<Ghost>();
			ghostPaths = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Ghost Paths"));
			goManager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Managers/Game Object Manager")).GetComponent<GameObjectManager>();
			pacman = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Pacman"));
			powerup = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Pickups/Powerup")).GetComponent<Powerup>();
			teleporter = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Teleporter"));
			
			pacmanCollision = pacman.GetComponent<PacmanCollision>();
			pacmanMovement = pacman.GetComponent<PacmanMovement>();
			pacmanScore = pacman.GetComponent<PacmanScore>();
			teleporter.transform.position = new Vector3(10f, 1f, 10f);
		}

		[TearDown]
		public void Teardown()
		{
			Object.Destroy(audio.gameObject);
			Object.Destroy(food.gameObject);
			Object.Destroy(geManager.gameObject);
			Object.Destroy(ghost.gameObject);
			Object.Destroy(ghostPaths.gameObject);
			Object.Destroy(goManager.gameObject);
			Object.Destroy(pacman.gameObject);
			Object.Destroy(powerup.gameObject);
			Object.Destroy(teleporter.gameObject);
		}

		[UnityTest]
		public IEnumerator CollidingWithFoodDeactivatesFood()
		{
			pacman.transform.position = food.transform.position;
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.False(food.activeSelf);
		}

		[UnityTest]
		public IEnumerator CollidingWithFoodIncreasesScore()
		{
			int score = pacmanScore.GetScore();
			pacman.transform.position = food.transform.position;
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreEqual(score + Constants.FOOD_SCORE, pacmanScore.GetScore());
		}

		[UnityTest]
		public IEnumerator CollidingWithCherryIncreasesScore()
		{
			GameObject cherry = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Pickups/Cherry"));
			int score = pacmanScore.GetScore();
			pacman.transform.position = cherry.transform.position;
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreEqual(score + Constants.FRUIT_EATEN_SCORE, pacmanScore.GetScore());
			Object.Destroy(cherry);
		}

		[UnityTest]
		public IEnumerator CollidingWithPowerupIncreasesSpeed()
		{
			pacman.transform.position = powerup.transform.position;
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreEqual(pacmanMovement.GetBoostSpeed(), pacmanMovement.GetSpeed());
		}

		[UnityTest]
		public IEnumerator CollidingWithPowerupMakesGhostsBecomeEdible()
		{
			pacman.transform.position = powerup.transform.position;
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.True(ghost.IsEdible());
		}

		[UnityTest]
		public IEnumerator CollidingWithGhostWhenGhostIsEdibleMakesGhostRunHome()
		{
			pacman.transform.position = powerup.transform.position;
			yield return new WaitForSeconds(WAIT_TIME);
			pacman.transform.position = ghost.transform.position;
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.True(ghost.IsRunningHome());
		}

		[UnityTest]
		public IEnumerator CollidingWithGhostWhenGhostIsEdibleIncreasesScore()
		{
			int score = pacmanScore.GetScore();
			pacman.transform.position = powerup.transform.position;
			yield return new WaitForSeconds(WAIT_TIME);
			pacman.transform.position = ghost.transform.position;
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreEqual(score + Constants.GHOST_EATEN_SCORE, pacmanScore.GetScore());
		}

		[UnityTest]
		public IEnumerator CollidingWithGhostWhenGhostIsNotEdibleRemovesLife()
		{
			int expectedLivesAfterDeath = pacmanCollision.GetCurrentLives() - 1;
			pacman.transform.position = ghost.transform.position;
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreEqual(expectedLivesAfterDeath, pacmanCollision.GetCurrentLives());
		}

		[UnityTest]
		public IEnumerator CollidingWithTeleporterMovesPlayer()
		{
			Vector3 positionBeforeTeleport = pacman.transform.position;
			pacman.transform.position = teleporter.transform.position;
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreNotEqual(positionBeforeTeleport, pacman.transform.position);
		}
	}
}