using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour 
{
    public AudioSource ghostCanBeEaten;
    public AudioSource ghostHasBeenEaten;
    public AudioSource ghostMoving; 
    public AudioSource ghostRunningHome;
  
    GameObject[]        ghosts;
    GameObject          pacman;

    bool                played;

    GameManager         gameManager;

    void Start()
    {
        ghosts      = GameObject.FindGameObjectsWithTag("Ghost");
        pacman      = GameObject.Find("Pacman");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }



    void Update()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            // If any ghosts can be eaten
            if (ghosts[i].GetComponent<Ghost>().canBeEaten)
            {
                PlaySound(ghostCanBeEaten);
                PauseSound(ghostMoving);
            }

            // If none of the ghosts are edible
            if (!ghosts[0].GetComponent<Ghost>().canBeEaten &&
                !ghosts[1].GetComponent<Ghost>().canBeEaten &&
                !ghosts[2].GetComponent<Ghost>().canBeEaten &&
                !ghosts[3].GetComponent<Ghost>().canBeEaten)
            {
                PauseSound(ghostCanBeEaten);

                // If pacman isnt dead and we can start the game and there is food on the field
                if (!pacman.GetComponent<PacmanCollision>().dead && gameManager.canStartGame && gameManager.numberOfFood > 0)
                {
                    PlaySound(ghostMoving);
                }
            }

            // If any of the ghosts are running home
            if (ghosts[0].GetComponent<Ghost>().runningHome ||
                ghosts[1].GetComponent<Ghost>().runningHome ||
                ghosts[2].GetComponent<Ghost>().runningHome ||
                ghosts[3].GetComponent<Ghost>().runningHome)
            {
                PlaySound(ghostRunningHome);
                PauseSound(ghostCanBeEaten);
            }

            // If there is no food then the game is complete, therefore dont play any sound
            if (gameManager.numberOfFood <= 0)
            {
                PauseAllSound();
            }
        }
    }


    void PlaySound(AudioSource sound)
    {
        if (sound.isPlaying)
        {
            return;
        }
        else
        {
            sound.Play();
        }
    }


    void PauseSound(AudioSource sound)
    {
        if (!sound.isPlaying)
        {
            return;
        }
        else
        {
            sound.Pause();
        }
    }


    void PauseAllSound()
    {
        PauseSound(ghostMoving);
        PauseSound(ghostCanBeEaten);
        PauseSound(ghostHasBeenEaten);
        PauseSound(ghostRunningHome);
    }
}
