using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanCollision : MonoBehaviour 
{
    public GameObject[] lives;

    public bool godMode;
    public bool dead;

    public int currentLives = 3;

    PacmanScore pacmanScore;

    PacmanAudio pacmanAudio;

    GameManager gameManager;

    void Start()
    {
        pacmanScore = GetComponent<PacmanScore>();

        pacmanAudio = GetComponent<PacmanAudio>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void OnCollisionEnter(Collision other)
    { 
        switch (other.gameObject.tag)
        {
            case "Food":
                other.gameObject.SetActive(false);
                pacmanScore.SetScoreText(10);
                pacmanAudio.eatFood.Play();
                //pacmanAudio.eatFoodTimer = 0f;

                if (gameManager.numberOfFood <= 1)
                    gameManager.ResetLevelAfterGameComplete();
                break;

            case "Powerup":
                other.gameObject.SetActive(false);
                gameManager.MakeGhostsEdible();
                break;

            case "Cherry":
                pacmanAudio.eatFruit.Play();
                Destroy(other.gameObject);
                pacmanScore.SetScoreText(100);
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (godMode)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Ghost":   
                // If we have activated a powerup then the ghost can be eaten
                if (other.gameObject.GetComponent<Ghost>().canBeEaten)
                {
                    other.gameObject.GetComponent<Ghost>().eaten = true;
                    pacmanAudio.eatenGhost.Play();
                    pacmanScore.SetScoreText(200);
                }
                // Otherwise we have died
                else
                { 
                    dead = true;

                    // Pause all the sounds
                    pacmanAudio.PauseAllSounds(); 

                    currentLives--;

                    // If we have lives
                    if (currentLives > -1)
                    {
                        // Set the lives graphics
                        lives[currentLives].SetActive(false);
                        gameManager.PlayDeathSequence(2f);
                    }
                    else
                    {
                        gameManager.GameOver();
                    }
                }
                break;
        }
    }

}
