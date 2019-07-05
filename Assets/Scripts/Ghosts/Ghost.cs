using UnityEngine;
using System.Collections;
using Manager;

namespace Ghosts
{
    public class Ghost : MonoBehaviour
    {
        public bool edible = false;
        public bool eaten = false;
        public bool runningHome = false;
        bool startedFlashCoroutine = false;

        public float speed;
        public float movingSpeed;
        public float flashingSpeed;
        public float eatenSpeed;

        [Header("Pathfinding")]
        public Transform[] pathNodes;
        public int currentNode;

        [Space()]
        public Material originalColour;

        [HideInInspector]
        public MeshRenderer bodyColour;

        Rigidbody rb;

        [HideInInspector]
        public CapsuleCollider capsuleCollider;

        void Start()
        {
            rb              = GetComponent<Rigidbody>();
            capsuleCollider = GetComponent<CapsuleCollider>();

            bodyColour = GameObject.Find(gameObject.name + "/Model/Body").GetComponent<MeshRenderer>();

            //InvokeRepeating("CheckToResetGhost", 5f, .1f);
        }

        void FixedUpdate()
        {
            Move();
        }

        void Move()
        {
            // If we are not at the current node
            if (transform.position != pathNodes[currentNode].position)
            {
                // Calculate moving from where we are to the next node
                Vector3 p = Vector3.MoveTowards(transform.position, pathNodes[currentNode].position, speed);

                // Look at the next node
                transform.LookAt(pathNodes[currentNode].position);

                // Move towards the current node
                rb.MovePosition(p);
            }
            else
            {
                // Increase the count i.e the next node
                currentNode = (currentNode + 1) % pathNodes.Length;
            }
        }

        public void BecomeEdible()
        {
            edible  = true;
            speed   = flashingSpeed;

            StartCoroutine(Flash(Color.blue, Color.white));
        }

        IEnumerator Flash(Color one, Color two)
        {
            StartCoroutine(WaitUntilInEdible());
            while (edible)
            {
                bodyColour.material.color = one;
                yield return new WaitForSeconds(.2f);
                bodyColour.material.color = two;
                yield return new WaitForSeconds(.2f);
            }

            // Bit of leeway so coroutines dont overlap and lose colour
            yield return new WaitForSeconds(.1f);
            ChangeToOriginalColour();
        }

        IEnumerator WaitUntilInEdible()
        {
            // Wait until we are not able to be eaten again
            yield return new WaitForSeconds(Constants.POWERUP_DURATION);        

            edible                  = false;
            startedFlashCoroutine   = false;

            if (!runningHome)   
                speed               = movingSpeed;

            AudioManager.instance.Pause("Ghost Edible");
        }

        public void ChangeToOriginalColour()
        {
            bodyColour.material = originalColour;
        }

        void OnTriggerEnter(Collider o)
        {
            if (o.name == "Ghost Home")
            {
                GameManager.instance.ResetGhost(this);
            }
        }
    }
}