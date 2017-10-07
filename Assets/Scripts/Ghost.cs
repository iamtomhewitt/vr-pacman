using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour 
{
    [Header("Bools")]
    public bool canBeEaten      = false;
    public bool eaten           = false;
    public bool runningHome     = false;
    bool startedFlashCoroutine  = false;

    [Header("Speeds")]
    public float speed;
    public float movingSpeed;
    public float flashingSpeed;
    public float eatenSpeed;

    [Header("Flash Settings")]
    public float timeToRemainFlashing;
    public float howLongToFlash;

    [Header("Pathfinding")]
    public Transform[] pathNodes;
    public int currentNode;

    [Space()]
    public Material originalColour;

    MeshRenderer ghostBodyColour;

    GameManager gameManager;

	void Start()
    {
        ghostBodyColour = GameObject.Find(gameObject.name+"/Model/Body").GetComponent<MeshRenderer>();

        gameManager     = GameObject.Find("Game Manager").GetComponent<GameManager>();

        StartCoroutine(WaitForStart(gameManager.startGameSound.clip.length));
	}
	
	void Update()
    {
        // If we have been eaten, then we cant hunt anymore and have to run home.
        if (eaten)
        {            
            RunHome();
            CheckToResetGhost();
        }

        // If pacman has hit a powerup
        if (canBeEaten)
        {
            BecomeEatable();
        }
        else
        {
            ChangeToOriginalColour();
        }
    }

    void FixedUpdate()
    {
        MoveBetweenNodes();
    }

    IEnumerator WaitForStart(float time)
    {
        // Wait for an amount of time before setting the speed in motion
        yield return new WaitForSeconds(time);
        speed = movingSpeed;
    }

    void MoveBetweenNodes()
    {
        // If we are not at the current node
        if (transform.position != pathNodes[currentNode].position)
        {
            // Calculate moving from where we are to the next node
            Vector3 p = Vector3.MoveTowards(transform.position, pathNodes[currentNode].position, speed);

            // Look at the next node
            transform.LookAt(pathNodes[currentNode].position);

            // Move towards the current node
            GetComponent<Rigidbody>().MovePosition(p);
        }
        else
        {
            // Increase the count i.e the next node
            currentNode = (currentNode + 1) % pathNodes.Length;
        }
    }

    void CheckToResetGhost()
    {
        // If we are inside the ghost home. Have to manually do this as the collider has been deactivated once eaten
        if ((transform.position.x < 3f && transform.position.x > -3f) &&
            (transform.position.z < 0.4f && transform.position.z > -0.8f))
        {
            eaten = false;
            canBeEaten = false;
            runningHome = false;

            ChangeToOriginalColour();

            ghostBodyColour.enabled = true;

            speed = movingSpeed;

            GetComponent<CapsuleCollider>().enabled = true;
        }
        else
        {
            return;
        }
    }

    public void HardReset()
    {
        eaten       = false;
        canBeEaten  = false;
        runningHome = false;

        ChangeToOriginalColour();

        ghostBodyColour.enabled = true;

        //speed = movingSpeed;

        GetComponent<CapsuleCollider>().enabled = true;

        //currentNode = 0;
    }

    void RunHome()
    {
        GetComponent<CapsuleCollider>().enabled = false;

        speed       = eatenSpeed;

        canBeEaten  = false;
        eaten       = true;
        runningHome = true;

        ghostBodyColour.enabled = false;
    }

    void BecomeEatable()
    {
        speed = flashingSpeed;

        // Start flashing if we havent already done so
        if (!startedFlashCoroutine)
        {
            // Flash until we cannot be eaten again      
            StartCoroutine(Flash(Color.blue, Color.white));
            StartCoroutine(WaitUntilCannotBeEaten(timeToRemainFlashing));
            startedFlashCoroutine = true;
        }
    }

    IEnumerator Flash(Color one, Color two)
    {
        // Basically while the power up is active
        while (canBeEaten)
        {
            // Flash
            ghostBodyColour.material.color = one;
            yield return new WaitForSeconds(.2f);
            ghostBodyColour.material.color = two;
            yield return new WaitForSeconds(.2f);
        }
    }

    IEnumerator WaitUntilCannotBeEaten(float time)
    {
        // Wait until we are not able to be eaten again
        yield return new WaitForSeconds(time);        

        ChangeToOriginalColour();

        canBeEaten              = false;
        startedFlashCoroutine   = false;

        speed = movingSpeed;
    }

    void ChangeToOriginalColour()
    {
        ghostBodyColour.material = originalColour;
    }
}
