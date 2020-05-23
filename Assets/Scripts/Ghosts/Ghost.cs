using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Manager;
using Utility;

namespace Ghosts
{
    public class Ghost : MonoBehaviour
    {
        [SerializeField] private Material originalColour;

        [SerializeField] private bool edible = false;
        [SerializeField] private bool eaten = false;
        [SerializeField] private bool runningHome = false;
        [SerializeField] private float speed;
        [SerializeField] private float movingSpeed;
        [SerializeField] private float flashingSpeed;
        [SerializeField] private float eatenSpeed;
        [SerializeField] private float speedIncrease;

        private GhostPath path;
        private Rigidbody rb;
        private MeshRenderer bodyColour;
        private Debugger debugger;
        private Coroutine flashRoutine;
        private Vector3 originalPosition;
        private AudioManager audioManager;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            debugger = GetComponent<Debugger>();
            audioManager = AudioManager.instance;

            originalPosition = transform.position;

            path = GetRandomPath();
            debugger.Info("has selected path: " + path.gameObject.name);

            bodyColour = GameObject.Find(gameObject.name + "/Model/Body").GetComponent<MeshRenderer>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnTriggerEnter(Collider o)
        {
            if (o.name.Equals(Constants.GHOST_HOME))
            {
                Reset();
                audioManager.StopGhostRunSound();
                SelectNewPath();
            }
        }

        private void Move()
        {
            // If we are not at the current node
            if (transform.position != path.GetCurrentWaypoint().position)
            {
                // Calculate moving from where we are to the next node
                Vector3 p = Vector3.MoveTowards(transform.position, path.GetCurrentWaypoint().position, speed);

                // Look at the next node
                transform.LookAt(path.GetCurrentWaypoint().position);

                // Move towards the current node
                rb.MovePosition(p);
            }
            else
            {
                // Increase the count i.e the next node
                path.SetNextWaypoint();
            }
        }

        public void IncreaseSpeed()
        {
            debugger.Info("Increasing speed");
            speed += speedIncrease;
        }

        /// <summary>
        /// Makes the Ghost able to be eaten by Pacman for a specified amount of time.
        /// </summary>
        public void BecomeEdible()
        {
            debugger.Info("has become edible");
            StopFlashing();
            StartCoroutine(BecomeEdibleRoutine());
        }

        public void StopFlashing()
        {
            if (flashRoutine != null)
            {
                StopCoroutine(flashRoutine);
            }
        }

        private IEnumerator BecomeEdibleRoutine()
        {
            edible = true;
            speed = flashingSpeed;

            flashRoutine = StartCoroutine(Flash(Color.blue, Color.white));
            yield return flashRoutine;
            bodyColour.material = originalColour;

            edible = false;

            if (!runningHome)
            {
                speed = movingSpeed;
            }
        }

        /// <summary>
        /// Makes the Ghost flash between two colours for a certain amount of time.
        /// </summary>
        private IEnumerator Flash(Color one, Color two)
        {
            float flashTimer = 0;
            float timeBetweenFlash = .2f;

            // Calculate a duration that will line up with the sound effect, using Constants.POWERUP_DURATION
            // will cause the timer to go 0.5 seconds over
            float duration = Constants.POWERUP_DURATION - (timeBetweenFlash * 3);

            do
            {
                bodyColour.material.color = one;
                yield return new WaitForSeconds(timeBetweenFlash);

                bodyColour.material.color = two;
                yield return new WaitForSeconds(timeBetweenFlash);

                flashTimer += timeBetweenFlash * 2;
            }
            while (flashTimer < duration);

            // Wait for a little in case of overlap
            yield return new WaitForSeconds(.1f);
        }

        public void Reset()
        {
            eaten = false;
            edible = false;
            runningHome = false;
            bodyColour.enabled = true;
            bodyColour.material = originalColour;
            speed = movingSpeed;

            if (flashRoutine != null)
            {
                StopCoroutine(flashRoutine);
            }
        }

        public void ResetPosition()
        {
            transform.position = originalPosition;
        }

        public void RunHome()
        {
            debugger.Info("is running home");

            audioManager.Play(SoundNames.GHOST_RUN);

            speed = eatenSpeed;
            edible = false;
            eaten = true;
            runningHome = true;
            bodyColour.enabled = false;
        }

        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }

        public float GetSpeed()
        {
            return speed;
        }

        public float GetMovingSpeed()
        {
            return movingSpeed;
        }

        public float GetEatenSpeed()
        {
            return eatenSpeed;
        }

        public void StopMoving()
        {
            speed = 0f;
        }

        public bool IsRunningHome()
        {
            return runningHome;
        }

        public bool IsEdible()
        {
            return edible;
        }

        /// <summary>
        /// Randomly selects a GhostPath to use as the waypoint path from the paths in the scene.
        /// Selects a path that is currently not being used by another ghost.
        /// </summary>
        private GhostPath GetRandomPath()
        {
            List<GhostPath> allPaths = new List<GhostPath>(GameObject.FindObjectsOfType<GhostPath>());
            IEnumerable<GhostPath> unusedPaths = allPaths.Where(x => !x.isUsed());
            GhostPath path = unusedPaths.ElementAt(Random.Range(0, unusedPaths.Count()));
            path.SetUsed(true);
            return path;
        }

        private void SelectNewPath()
        {
            path.SetUsed(false);
            path = GetRandomPath();
            debugger.Info("has selected a new path: " + path.transform.name);
        }

        public GhostPath GetPath()
        {
            return path;
        }
    }
}