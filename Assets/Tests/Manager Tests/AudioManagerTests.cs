using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine;
using Manager;
using static Manager.AudioManager;

namespace Tests
{
	public class AudioManagerTests
	{
		private AudioManager manager;
		private Sound sound;
		private float WAIT_TIME = 0.001f;

		[SetUp]
		public void Setup()
		{
			if (AudioManager.instance == null)
			{
				manager = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/Prefabs/Managers/Audio Manager")).GetComponent<AudioManager>();
			}
			sound = manager.GetSound(SoundNames.INTRO_MUSIC);
		}

		[TearDown]
		public void Teardown()
		{
			Object.Destroy(manager.gameObject);
		}

		[UnityTest]
		public IEnumerator AudioManagerPlaysSound()
		{
			manager.Play(sound.name);
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.True(sound.source.isPlaying);
		}

		[UnityTest]
		public IEnumerator AudioManagerPausesSound()
		{
			manager.Play(sound.name);
			yield return new WaitForSeconds(WAIT_TIME);
			manager.Pause(sound.name);
			Assert.False(sound.source.isPlaying);
		}

		[UnityTest]
		public IEnumerator AudioManagerPausesAllSounds()
		{
			Sound otherSound = manager.GetSound(SoundNames.EAT_GHOST);
			manager.PauseAllSounds();
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.False(sound.source.isPlaying);
			Assert.False(otherSound.source.isPlaying);
		}

		[UnityTest]
		public IEnumerator AudioManagerStopsSound()
		{
			manager.Play(sound.name);
			yield return new WaitForSeconds(WAIT_TIME);
			manager.Stop(sound.name);
			Assert.False(sound.source.isPlaying);
		}

		[UnityTest]
		public IEnumerator AudioManagerPlaysSoundForADuration()
		{
			manager.PlayForDuration(sound.name, WAIT_TIME);	
			Assert.True(sound.source.isPlaying);
			yield return new WaitForSeconds(WAIT_TIME);
			Assert.False(sound.source.isPlaying);
		}
	}
}
