using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine;
using Pacman;
using Utility;
using Manager;
using Ghosts;

namespace Tests
{
	public class PacmanCollisionTests
	{
		private GameObject pacman;
		private PacmanCollision pacmanCollision;
		private PacmanScore pacmanScore;
		private PacmanMovement pacmanMovement;

		private Ghost ghost;
		private GameObject ghostPaths;

		private Powerup powerup;

		private AudioManager audio;

		private GameObjectManager goManager;
		private GameEventManager geManager;
		
		private float WAIT_TIME = 0.1f;

		[SetUp]
		public void Setup()
		{
			audio = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Managers/Audio Manager")).GetComponent<AudioManager>();

			pacman = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Pacman"));
			pacmanCollision = pacman.GetComponent<PacmanCollision>();
			pacmanMovement = pacman.GetComponent<PacmanMovement>();
			pacmanScore = pacman.GetComponent<PacmanScore>();

			ghost = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Ghost")).GetComponent<Ghost>();
			ghostPaths = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Ghost Paths"));

			goManager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Managers/Game Object Manager")).GetComponent<GameObjectManager>();
			geManager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Managers/Game Event Manager")).GetComponent<GameEventManager>();

			powerup = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Pickups/Powerup")).GetComponent<Powerup>();
		}

		[TearDown]
		public void Teardown()
		{
			Object.Destroy(pacman.gameObject);
			Object.Destroy(powerup.gameObject);
			Object.Destroy(ghost.gameObject);
			Object.Destroy(ghostPaths.gameObject);
			Object.Destroy(goManager.gameObject);
			Object.Destroy(geManager.gameObject);
			Object.Destroy(audio.gameObject);
		}

		[UnityTest]
		public IEnumerator CollidingWithFoodDeactivatesFood()
		{
			GameObject food = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Pickups/Food"));
			pacman.transform.position = food.transform.position;
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.False(food.activeSelf);
			Object.Destroy(food);
		}

		[UnityTest]
		public IEnumerator CollidingWithFoodIncreasesScore()
		{
			GameObject food = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Pickups/Food"));
			int score = pacmanScore.GetScore();
			pacman.transform.position = food.transform.position;
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreEqual(score + Constants.FOOD_SCORE, pacmanScore.GetScore());
			Object.Destroy(food);
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
	}
}