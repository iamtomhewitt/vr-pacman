﻿using Ghosts;
using Manager;
using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine;

namespace Tests
{
	public class GhostTests
	{
		private AudioManager audio;
		private GameObject ghostPaths;
		private GameObject pacman;
		private GameObjectManager goManager;
		private Ghost ghost;
		private float WAIT_TIME = 0.1f;

		[SetUp]
		public void Setup()
		{
			audio = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Managers/Audio Manager")).GetComponent<AudioManager>();
			ghost = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Ghost")).GetComponent<Ghost>();
			ghostPaths = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Ghost Paths"));
			goManager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Managers/Game Object Manager")).GetComponent<GameObjectManager>();
			pacman = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Pacman"));
		}

		[TearDown]
		public void Teardown()
		{
			Object.Destroy(audio.gameObject);
			Object.Destroy(ghost.gameObject);
			Object.Destroy(ghostPaths.gameObject);
			Object.Destroy(goManager.gameObject);
			Object.Destroy(pacman.gameObject);
		}

		[UnityTest]
		public IEnumerator GhostMoves()
		{
			Vector3 position = ghost.transform.position;
			ghost.SetSpeed(ghost.GetMovingSpeed());
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreNotEqual(position, ghost.transform.position);
		}

		[UnityTest]
		public IEnumerator CollidingWithGhostHomeSelectsANewPath()
		{
			GhostPath path = ghost.GetPath();
			GameObject ghostHome = goManager.GetGhostHome();
			ghostHome.SetActive(true);
			ghost.transform.position = goManager.GetGhostHome().transform.position;
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreNotSame(path, ghost.GetPath());
		}

		[UnityTest]
		public IEnumerator CollidingWithGhostHomeResetsGhost()
		{
			GhostPath path = ghost.GetPath();
			GameObject ghostHome = goManager.GetGhostHome();
			ghostHome.SetActive(true);
			ghost.transform.position = goManager.GetGhostHome().transform.position;
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.False(ghost.IsRunningHome());
			Assert.False(ghost.IsEdible());
		}

		[UnityTest]
		public IEnumerator GhostCanBecomeEdible()
		{
			ghost.BecomeEdible();
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.True(ghost.IsEdible());
		}

		[UnityTest]
		public IEnumerator GhostCanResetPosition()
		{
			Vector3 originalPosition = ghost.transform.position;
			ghost.SetSpeed(ghost.GetMovingSpeed());
			yield return new WaitForSeconds(WAIT_TIME);

			ghost.ResetPosition();
			Assert.AreEqual(originalPosition, ghost.transform.position);
		}

		[UnityTest]
		public IEnumerator MakingGhostRunHomeChangesGhostSpeed()
		{
			ghost.RunHome();
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreEqual(ghost.GetSpeed(), ghost.GetEatenSpeed());
			Assert.True(ghost.IsRunningHome());
		}

		[UnityTest]
		public IEnumerator MakingGhostRunHomeMakesGhostNotEdible()
		{
			ghost.RunHome();
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.False(ghost.IsEdible());
			Assert.True(ghost.IsRunningHome());
		}

		[UnityTest]
		public IEnumerator SelectingANewPathSetsCurrentPathToUsedAndCurrentPathIsDifferent()
		{
			GhostPath path = ghost.GetPath();
			GameObject ghostHome = goManager.GetGhostHome();
			ghostHome.SetActive(true);
			ghost.transform.position = goManager.GetGhostHome().transform.position;
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.True(ghost.GetPath().isUsed());
			Assert.AreNotSame(path, ghost.GetPath());
		}

		[UnityTest]
		public IEnumerator IncreasingGhostSpeedWorks()
		{
			float currentSpeed = ghost.GetSpeed();
			ghost.IncreaseSpeed();
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreNotSame(currentSpeed, ghost.GetSpeed());
		}

		[UnityTest]
		public IEnumerator ResettingGhostResetsMoveSoundPitch()
		{
			float pitch = audio.GetSound(SoundNames.GHOST_MOVE).source.pitch;
			ghost.IncreaseSpeed();
			ghost.Reset();
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.AreEqual(pitch, audio.GetSound(SoundNames.GHOST_MOVE).source.pitch);
		}

		[Test]
		public void StopMovingWorks()
		{
			ghost.SetSpeed(ghost.GetMovingSpeed());
			ghost.StopMoving();
			Assert.AreEqual(0f, ghost.GetSpeed());
		}
	}
}