﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Manager
{
    public class AudioManager : MonoBehaviour 
    {
        public Sound[] sounds;

        public static AudioManager instance;

        void Awake()
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
                s.source.Play();
        }

        public void Pause(string name)
        {
            Sound s = GetSound(name);

            if (s != null) s.source.Pause();
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

        [System.Serializable]
        public class Sound
        {
            public string name;

            public AudioClip clip;

            [Range(0f,1f)]
            public float volume;

            [Range(0.5f, 3f)]
            public float pitch;

            public bool loop;

            [HideInInspector]
            public AudioSource source;
        }
    }
}
