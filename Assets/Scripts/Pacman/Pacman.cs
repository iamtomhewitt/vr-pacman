using UnityEngine;
using System.Collections;

public class Pacman : MonoBehaviour 
{
    public bool godMode;

    [Header("Arrays")]
    public GameObject[] allSounds;
    public GameObject[] lives;
    GameObject[]        ghosts;
    GameObject[]        powerups;

    [Header("GameObjects")]
    public GameObject   gameOverText;
    public GameObject   readyText;

    [Header("Sounds")]
    public AudioSource  death;
    public AudioSource  eatFood;
    public AudioSource  eatenGhost; 
    public AudioSource  eatFruit;
    public AudioSource  levelComplete;

    [Header("Other Settings")]
    public bool         dead;

    public float        speed;

    public int          score = 0;
    public int          currentLives = 3;

    float               originalSpeed;
    float               eatFoodTimer;

    TextMesh            scoreTextFront, scoreTextBack;

    GameManager         gameManager;

	void Start () 
    {
	    ghosts          = GameObject.FindGameObjectsWithTag("Ghost");
        allSounds       = GameObject.FindGameObjectsWithTag("Sound");

        scoreTextFront  = GameObject.Find("Score/Front").GetComponent<TextMesh>();
        scoreTextBack   = GameObject.Find("Score/Back").GetComponent<TextMesh>();

        gameManager     = GameObject.Find("Game Manager").GetComponent<GameManager>();

        powerups        = GameObject.FindGameObjectsWithTag("Powerup");

        originalSpeed   = speed;
        speed           = 0f;

        // Set the timer for the waka sound to start playing to -start sound length. This way it stays quiet until the sound has finished
        eatFoodTimer    = (gameManager.startGameSound.clip.length*-1);

        StartCoroutine(WaitForStart(gameManager.startGameSound.clip.length));    
        eatFood.Pause();
    }
	
	void Update()
    {
        eatFoodTimer += Time.deltaTime;

        // If the timer is less than 0 or bigger than .8, then dont play the waka sound
        // This allows for the sound to play correctly, and not have a jittery effect
        if (eatFoodTimer > 0.8f || eatFoodTimer < 0)
        {
            eatFood.Pause();
        }
        else
        {
            // Otherwise play the waka sound
            eatFood.UnPause();
        }

        // Stop us from moving up or down - tried with a Rigidbody but this didnt work
        if (transform.position.y < 0 || transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        transform.position += Camera.main.transform.forward * speed * Time.deltaTime;
	}

    void FixedUpdate()
    {
        // Move forward based on which way we are looking
        //transform.position += Camera.main.transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    { 
        switch (other.gameObject.tag)
        {
            case "Food":
                other.gameObject.SetActive(false);
                SetScoreText(10);
                eatFoodTimer = 0f;

                if (gameManager.numberOfFood <= 1)
                    StartCoroutine(ResetLevelAfterGameComplete());
                break;

            case "Powerup":
                other.gameObject.SetActive(false);
                for (int i = 0; i < ghosts.Length; i++)
                    ghosts[i].GetComponent<Ghost>().canBeEaten = true;
                break;

            case "Cherry":
                eatFruit.Play();
                Destroy(other.gameObject);
                SetScoreText(100);
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
                    eatenGhost.Play();
                    SetScoreText(200);
                }
                // Otherwise we have died
                else
                { 
                    dead = true;

                    // Pause all the sounds
                    for (int i = 0; i < allSounds.Length; i++)
                    {
                        allSounds[i].GetComponent<AudioSource>().Pause();
                    }     

                    currentLives--;

                    // If we have lives
                    if (currentLives > -1)
                    {
                        lives[currentLives].SetActive(false);
                        StartCoroutine(PlayDeathSequence(2f));
                    }
                    else
                    {
                        GameOver();
                    }
                }
                break;
        }
    }

    IEnumerator WaitForStart(float time)
    {
        yield return new WaitForSeconds(time);
        speed = originalSpeed;
    }

    void SetScoreText(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreTextBack.text  = "SCORE "+score.ToString();
        scoreTextFront.text = "SCORE "+score.ToString();
    }

    IEnumerator PlayDeathSequence(float timeBetween)
    {
        StopEveryoneMoving();

        HardResetGhosts();

        yield return new WaitForSeconds(timeBetween / 2f);

        death.Play();

        yield return new WaitForSeconds(timeBetween);

        dead = false;

        ResetEveryonesPosition();

        if (currentLives == 0)
        {
            GameOver();
            StartCoroutine(ResetLevelAfterGameOver());
        }
        else
        {
            readyText.SetActive(true);
            yield return new WaitForSeconds(timeBetween);
            readyText.SetActive(false);
            ResetEveryonesSpeed();
        }
    }

    void StopEveryoneMoving()
    {
        // Stop all the ghosts moving
        for (int i = 0; i < ghosts.Length; i++)
            ghosts[i].GetComponent<Ghost>().speed = 0f;

        // Stop ourselves moving
        speed = 0f;
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

        // Reset our position
        transform.position = new Vector3(0f, 0f, -3.43f);
    }

    void ResetEveryonesSpeed()
    {
        // Reset our speed
        speed = originalSpeed;

        // Reset the Ghosts speed and their path node
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].GetComponent<Ghost>().speed = ghosts[i].GetComponent<Ghost>().movingSpeed;
            ghosts[i].GetComponent<Ghost>().currentNode = 0;
        }
    }

    void GameOver()
    {
        // Set ourselves to dead, so everything to do with pacman being dead happens
        dead = true;

        gameOverText.SetActive(true);

        // Pause all the sounds
        for (int i = 0; i < allSounds.Length; i++)
        {
            allSounds[i].GetComponent<AudioSource>().Pause();
        }      
    }

    IEnumerator ResetLevelAfterGameOver()
    {
        // Give user adjust time
        yield return new WaitForSeconds(2f);

        gameOverText.SetActive(false);

        // Reset the score
        score = 0;
        SetScoreText(0);

        // Reset the lives
        currentLives = 3;  

        SetAllLivesActive();

        SetAllPowerupsActive();

        // Play the intro music again, and wait until that has finished to carry on
        gameManager.startGameSound.Play();
        yield return new WaitForSeconds(gameManager.startGameSound.clip.length);

        SetAllFoodActive(); 

        readyText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        readyText.SetActive(false);

        dead = false;   

        HardResetGhosts();

        ResetEveryonesPosition();

        ResetEveryonesSpeed();
    }

    IEnumerator ResetLevelAfterGameComplete()
    {
        StopEveryoneMoving();

        levelComplete.Play();
        yield return new WaitForSeconds(levelComplete.clip.length);

        //yield return new WaitForSeconds(3f);

        SetAllFoodActive();

        SetAllPowerupsActive();

        HardResetGhosts();

        ResetEveryonesPosition();

        readyText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        readyText.SetActive(false);

        ResetEveryonesSpeed();
    }

    void HardResetGhosts()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].GetComponent<Ghost>().HardReset();
        }
    }

    void SetAllFoodActive()
    {
        for (int i = 0; i < gameManager.foods.Length; i++)
        {
            gameManager.foods[i].SetActive(true);
        }
    }

    void SetAllPowerupsActive()
    {
        for (int i = 0; i < powerups.Length; i++)
        {
            powerups[i].SetActive(true);
        }
    }

    void SetAllLivesActive()
    {
        for (int i = 0; i < currentLives; i++)
        {
            lives[i].SetActive(true);
        }
    }
}
