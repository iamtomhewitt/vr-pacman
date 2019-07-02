﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
using Ghosts;

namespace Pacman
{
    public class PacmanCollision : MonoBehaviour
    {
        public GameObject[] lifeSprites;

        public bool godMode;
        public bool dead;

        public int currentLives = 3;

        PacmanScore pacmanScore;

        void Start()
        {
            pacmanScore = GetComponent<PacmanScore>();
        }

        void OnCollisionEnter(Collision other)
        { 
            switch (other.gameObject.tag)
            {
                case "Food":
                    other.gameObject.SetActive(false);
                    pacmanScore.AddScore(10);
                    AudioManager.instance.Play("Eat Food");

                    GameManager.instance.CountFood();

                    if (GameManager.instance.numberOfFood <= 0)
                        GameController.instance.TriggerLevelComplete();
                    break;

                case "Powerup":
                    other.gameObject.SetActive(false);
                    AudioManager.instance.Play("Ghost Edible");
                    GameManager.instance.MakeGhostsEdible();
                    break;

                case "Cherry":
                    AudioManager.instance.Play("Eat Fruit");
                    Destroy(other.gameObject);
                    pacmanScore.AddScore(100);
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
                    Ghost ghost = other.GetComponent<Ghost>();

                    if (ghost.runningHome)
                        return;

                    if (ghost.edible)
                    {
                        GameManager.instance.RunHome(ghost);
                        AudioManager.instance.Play("Eat Ghost");
                        pacmanScore.AddScore(200);
                    }
                    else
                    { 
                        StartCoroutine(Die());
                    }
                    break;
            }
        }

        IEnumerator Die()
        {
            dead = true;
            currentLives--;

            AudioManager.instance.PauseAllSounds();
            GameManager.instance.StopMovingEntities();

            yield return new WaitForSeconds(1f);
            AudioManager.instance.Play("Pacman Death");
            yield return new WaitForSeconds(AudioManager.instance.GetSound("Pacman Death").clip.length+1f);

            GameManager.instance.ResetEntityPositions();
            dead = false;

            if (currentLives < 0)
            {
                bool newHighscore = false;
                if (pacmanScore.score > HighscoreManager.instance.LoadLocalHighscore())
                {
                    HighscoreManager.instance.SaveLocalHighscore(pacmanScore.score);
                    newHighscore = true;
                }

                GameController.instance.TriggerGameOver(newHighscore);
            }
            else
            {
                lifeSprites[currentLives].SetActive(false);
                GameController.instance.statusText.text = "READY?";
                yield return new WaitForSeconds(2f);
                GameController.instance.statusText.text = "";
                GameManager.instance.StartMovingEntities();
            }
        }
    }
}