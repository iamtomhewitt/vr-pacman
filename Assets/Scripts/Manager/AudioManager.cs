using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using Ghosts;

namespace Manager
{
	public class AudioManager : MonoBehaviour
	{
		[SerializeField] private Sound[] sounds;

		public static AudioManager instance;

		private Coroutine durationRoutine;

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			else
			{
				Destroy(this.gameObject);
				return;
			}

			// Don't need this here as audio only present on game scene FOR NOW
			//DontDestroyOnLoad(this.gameObject);

			foreach (Sound s in sounds)
			{
				s.source = this.gameObject.AddComponent<AudioSource>();
				s.source.clip = s.clip;
				s.source.volume = s.volume;
				s.source.pitch = s.pitch;
				s.source.loop = s.loop;
			}
		}

		public void Play(string name)
		{
			Sound s = GetSound(name);

			if (s != null)
			{
				s.source.Play();
			}
		}

		/// <summary>
		/// Plays a sound which loops for a specified duration.
		/// </summary>
		public void PlayForDuration(string name, float duration)
		{
			Stop(name);

			if (durationRoutine != null)
			{
				StopCoroutine(durationRoutine);
			}

			durationRoutine = StartCoroutine(PlayForDurationRoutine(name, duration));
		}

		private IEnumerator PlayForDurationRoutine(string name, float duration)
		{
			Play(name);
			yield return new WaitForSeconds(duration);
			Stop(name);
		}

		public void Pause(string name)
		{
			Sound s = GetSound(name);

			if (s != null)
			{
				s.source.Pause();
			}
		}

		public void Stop(string name)
		{
			Sound s = GetSound(name);

			if (s != null)
			{
				s.source.Stop();
			}
		}

		public void PauseAllSounds()
		{
			foreach (Sound s in sounds)
			{
				s.source.Pause();
			}
		}

		public Sound GetSound(string name)
		{
			Sound s = Array.Find(sounds, sound => sound.name == name);

			if (s == null)
			{
				print("WARNING! Sound: '" + name + "' was not found.");
				return null;
			}
			return s;
		}

		/// <summary>
		/// Stops the Ghost run home sound if there are no ghosts running home.
		/// </summary>
		public void StopGhostRunSound()
		{
			if (FindObjectsOfType<Ghost>().Where(ghost => ghost.IsRunningHome()).Count() == 0)
			{
				Pause(SoundNames.GHOST_RUN);
			}
		}

		[System.Serializable]
		public class Sound
		{
			public string name;

			public AudioClip clip;

			[Range(0f, 1f)]
			public float volume;

			[Range(0.5f, 3f)]
			public float pitch;

			public bool loop;

			[HideInInspector]
			public AudioSource source;
		}
	}

	public class SoundNames
	{
		public const string FOOD = "Eat Food";
		public const string EAT_FRUIT = "Eat Fruit";
		public const string EAT_GHOST = "Eat Ghost";
		public const string GHOST_EDIBLE = "Ghost Edible";
		public const string GHOST_MOVE = "Ghost Move";
		public const string GHOST_RUN = "Ghost Run";
		public const string PACMAN_DEATH = "Pacman Death";
		public const string INTRO_MUSIC = "Intro Music";
		public const string LEVEL_COMPLETE = "Level Complete";
		public const string BUTTON_PRESS = "Button Press";
	}
}

