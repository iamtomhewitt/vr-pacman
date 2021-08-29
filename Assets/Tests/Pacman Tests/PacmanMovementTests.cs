using Environment;
using Manager;
using NUnit.Framework;
using Pacman;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine;
using Utility;

namespace Tests
{
	public class PacmanMovementTests
	{
		private AudioManager audio;
		private GameObject pacman;
		private GameObjectManager goManager;
		private PacmanMovement pacmanMovement;
		private Powerup powerup;
		private float WAIT_TIME = 0.05f;

		[SetUp]
		public void Setup()
		{
			if (AudioManager.instance == null)
			{
				audio = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Managers/Audio Manager")).GetComponent<AudioManager>();
			}
			goManager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Managers/Game Object Manager")).GetComponent<GameObjectManager>();
			pacman = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Pacman"));
			pacmanMovement = pacman.GetComponent<PacmanMovement>();
		}

		[TearDown]
		public void Teardown()
		{
			Object.Destroy(pacman);
		}

		[UnityTest]
		public IEnumerator PacmanMoves()
		{
			pacmanMovement.ResetSpeed();
			float startingPosition = pacman.transform.position.x;
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.Greater(pacman.transform.position.x, startingPosition);
		}

		[UnityTest]
		public IEnumerator PacmansSpeedResets()
		{
			pacmanMovement.ResetSpeed();
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreEqual(pacmanMovement.GetSpeed(), pacmanMovement.GetOriginalSpeed());
		}

		[UnityTest]
		public IEnumerator PacmanSpeedIsBoostedWhenHittingAPowerup()
		{
			Powerup powerup = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Pickups/Powerup")).GetComponent<Powerup>();
			pacman.transform.position = powerup.transform.position;

			yield return new WaitForSeconds(0.05f);
			Assert.AreEqual(pacmanMovement.GetBoostSpeed(), pacmanMovement.GetSpeed());
			yield return new WaitForSeconds(Constants.POWERUP_DURATION);
			Assert.AreEqual(pacmanMovement.GetOriginalSpeed(), pacmanMovement.GetSpeed());

			Object.Destroy(powerup.gameObject);
		}

		[UnityTest]
		public IEnumerator PacmansPositionResets()
		{
			Vector3 startingPosition = pacman.transform.position;
			yield return new WaitForSeconds(WAIT_TIME);
			pacmanMovement.ResetPosition();
			Assert.AreEqual(startingPosition, pacman.transform.position);
		}

		[UnityTest]
		public IEnumerator PacmanStops()
		{
			pacmanMovement.Stop();
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreEqual(0f, pacmanMovement.GetSpeed());
		}
	}
}