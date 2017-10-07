using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    GameObject[] ghosts;
    GameObject[] powerups;
    GameObject pacman;

    [HideInInspector]
    public GameObject[] foods;
    public GameObject cherry;

    public GameObject gameOverText;
    public GameObject readyText;

    public int numberOfFood;

    public bool canStartGame;
    bool spawnedCherry;

    public AudioSource startGameSound;

    void Start()
    {
        // Plays the sound and then allows the game to be played
        StartCoroutine(ieWaitForStartGame(startGameSound));

        foods = GameObject.FindGameObjectsWithTag("Food");
        ghosts = GameObject.FindGameObjectsWithTag("Ghost");
        powerups = GameObject.FindGameObjectsWithTag("Powerup");
        pacman = GameObject.Find("Pacman");

        for (int i = 0; i < foods.Length; i++)
        {
            // Name the food "Food ([coordinates])
            foods[i].name = "Food ("+ foods[i].transform.position.x.ToString() + ", " + foods[i].transform.position.z.ToString() + ")";
            foods[i].transform.parent = GameObject.Find("Game Manager").transform;
        }

        InvokeRepeating("CheckToSpawnCherry", 10f, 10f);
    }


    void Update()
    {
        numberOfFood = GameObject.FindGameObjectsWithTag("Food").Length;

        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
        {
            Application.Quit();
        }
    }


    void WaitForStartGame()
    {
        StartCoroutine(ieWaitForStartGame(startGameSound));
    }


    IEnumerator ieWaitForStartGame(AudioSource sound)
    {
        yield return new WaitForSeconds(sound.clip.length);
        canStartGame = true;
        StartEveryonesSpeed();
    }


    public void ResetLevelAfterGameComplete()
    {
        StartCoroutine(ieResetLevelAfterGameComplete());
    }


    IEnumerator ieResetLevelAfterGameComplete()
    {
        StopEveryoneMoving();

        pacman.GetComponent<PacmanAudio>().levelComplete.Play();

        yield return new WaitForSeconds(pacman.GetComponent<PacmanAudio>().levelComplete.clip.length);

        //yield return new WaitForSeconds(3f);

        SetAllFoodActive();

        SetAllPowerupsActive();

        HardResetGhosts();

        ResetEveryonesPosition();

        readyText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        readyText.SetActive(false);

        StartEveryonesSpeed();
    }


    void ResetLevelAfterGameOver()
    {
        StartCoroutine(ieResetLevelAfterGameOver());
    }


    IEnumerator ieResetLevelAfterGameOver()
    {
        // Give user adjust time
        yield return new WaitForSeconds(2f);

        gameOverText.SetActive(false);

        // Reset pacmans score
        pacman.GetComponent<PacmanScore>().score = 0;
        pacman.GetComponent<PacmanScore>().SetScoreText(0);

        // Reset the lives
        pacman.GetComponent<PacmanCollision>().currentLives = 3;  

        // Lives graphics
        //SetAllLivesActive();

        SetAllPowerupsActive();

        SetAllFoodActive(); 

        // Play the intro music again, and wait until that has finished to carry on
        startGameSound.Play();
        yield return new WaitForSeconds(startGameSound.clip.length);
        //yield return ieWaitForStartGame(startGameSound);

        readyText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        readyText.SetActive(false);

        pacman.GetComponent<PacmanCollision>().dead = false;   

        HardResetGhosts();

        ResetEveryonesPosition();

        StartEveryonesSpeed();
    }


    void StopEveryoneMoving()
    {
        // Stop all the ghosts moving
        for (int i = 0; i < ghosts.Length; i++)
            ghosts[i].GetComponent<Ghost>().speed = 0f;

        // Stop ourselves moving
        if (pacman.GetComponent<PacmanMovement>() != null)
            pacman.GetComponent<PacmanMovement>().speed = 0f;

        else
            pacman.GetComponent<PacmanAccelerometerMovement>().speed = 0f;
    }


    void SetAllFoodActive()
    {
        for (int i = 0; i < foods.Length; i++)
        {
            foods[i].SetActive(true);
        }
    }


    void SetAllPowerupsActive()
    {
        for (int i = 0; i < powerups.Length; i++)
        {
            powerups[i].SetActive(true);
        }
    }


    void HardResetGhosts()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].GetComponent<Ghost>().HardReset();
        }
    }


    public void MakeGhostsEdible()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].GetComponent<Ghost>().canBeEaten = true;
        }
    }


    void ResetEveryonesPosition()
    {
        // Reset the positions of the Ghosts
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.transform.position = new Vector3((-1.5f + i), 0f, -1.5f);

            // We reset the current node here to stop the Ghosts immediately looking at the first node when pacman dies
            ghosts[i].GetComponent<Ghost>().currentNode = 0;
        }

        // Reset pacmans position
        pacman.transform.position = new Vector3(0f, 0f, -3.43f);
    }


    void StartEveryonesSpeed()
    {
        // Reset pacmans speed
        if (pacman.GetComponent<PacmanMovement>() != null)
            pacman.GetComponent<PacmanMovement>().speed = pacman.GetComponent<PacmanMovement>().originalSpeed;

        else
            pacman.GetComponent<PacmanAccelerometerMovement>().speed = pacman.GetComponent<PacmanAccelerometerMovement>().originalSpeed;

        // Reset the Ghosts speed and their path node
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].GetComponent<Ghost>().speed = ghosts[i].GetComponent<Ghost>().movingSpeed;
            ghosts[i].GetComponent<Ghost>().currentNode = 0;
        }
    }


    public void GameOver()
    {
        // Set ourselves to dead, so everything to do with pacman being dead happens
        pacman.GetComponent<PacmanCollision>().dead = true;

        gameOverText.SetActive(true);

        // Pause all the sounds
        for (int i = 0; i < pacman.GetComponent<PacmanAudio>().allSounds.Length; i++)
        {
            pacman.GetComponent<PacmanAudio>().allSounds[i].GetComponent<AudioSource>().Pause();
        }      
    }


    public void PlayDeathSequence(float timeBetween)
    {
        StartCoroutine(iePlayDeathSequence(timeBetween));
    }


    IEnumerator iePlayDeathSequence(float timeBetween)
    {
        StopEveryoneMoving();

        HardResetGhosts();

        yield return new WaitForSeconds(timeBetween / 2f);

        pacman.GetComponent<PacmanAudio>().death.Play();

        yield return new WaitForSeconds(timeBetween);

        pacman.GetComponent<PacmanCollision>().dead = false;

        ResetEveryonesPosition();

        if (pacman.GetComponent<PacmanCollision>().currentLives == 0)
        {
            GameOver();
            ResetLevelAfterGameOver();
        }
        else
        {
            readyText.SetActive(true);
            yield return new WaitForSeconds(timeBetween);
            readyText.SetActive(false);
            StartEveryonesSpeed();
        }
    }


    void CheckToSpawnCherry()
    {
        if ((numberOfFood <= 80 && numberOfFood >= 40) && !spawnedCherry)
        {
            Instantiate(cherry, new Vector3(0f, -0.303f, -9.26f), Quaternion.Euler(new Vector3(0f, 90f, 0f)));
            spawnedCherry = true;
        }
        if (numberOfFood <= 1)
        {
            spawnedCherry = false;
        }
    }
}
