using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanAudio : MonoBehaviour 
{
    [HideInInspector]
    public float eatFoodTimer;

    public AudioSource  death;
    public AudioSource  eatFood;
    public AudioSource  eatenGhost; 
    public AudioSource  eatFruit;
    public AudioSource  levelComplete;

    public GameObject[] allSounds;

	void Start () 
    {
        allSounds = GameObject.FindGameObjectsWithTag("Sound");
	}
	
	void Update () 
    {
        eatFoodTimer += Time.deltaTime;

        // If the timer is less than 0 or bigger than .8, then dont play the waka sound
        // This allows for the sound to play correctly, and not have a jittery effect
//        if (eatFoodTimer > 0.8f || eatFoodTimer < 0)
//        {
//            eatFood.Pause();
//        }
//        else
//        {
//            // Otherwise play the waka sound
//            eatFood.UnPause();
//        }
	}

    public void PauseAllSounds()
    {
        for (int i = 0; i < allSounds.Length; i++)
        {
            allSounds[i].GetComponent<AudioSource>().Pause();
        }
    }
}
