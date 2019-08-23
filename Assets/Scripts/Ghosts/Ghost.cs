using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Manager;

namespace Ghosts
{
	public class Ghost : MonoBehaviour
	{
		public Material originalColour;

		[SerializeField] private bool edible = false;
		[SerializeField] private bool eaten = false;
		[SerializeField] private bool runningHome = false;

		[SerializeField] private float speed;
		[SerializeField] private float movingSpeed;
		[SerializeField] private float flashingSpeed;
		[SerializeField] private float eatenSpeed;

		private GhostPath path;
		private Rigidbody rb;
		private MeshRenderer bodyColour;

		[SerializeField]
		private string debugColour;

		private void Start()
		{
			rb = GetComponent<Rigidbody>();

			path = GetRandomPath();
			Debug("has selected path: " + path.gameObject.name);

			bodyColour = GameObject.Find(gameObject.name + "/Model/Body").GetComponent<MeshRenderer>();
		}

		private void FixedUpdate()
		{
			Move();
		}

		private void OnTriggerEnter(Collider o)
		{
			if (o.name == Constants.GHOST_HOME)
			{
				Reset();
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

		/// <summary>
		/// Makes the Ghost able to be eaten by Pacman for a specified amount of time.
		/// </summary>
		public void BecomeEdible()
		{
			StartCoroutine(BecomeEdibleRoutine());
		}

		private IEnumerator BecomeEdibleRoutine()
		{
			edible = true;
			speed = flashingSpeed;

			yield return StartCoroutine(Flash(Color.blue, Color.white));
			bodyColour.material = originalColour;

			edible = false;
			if (!runningHome)
			{
				speed = movingSpeed;
			}

			AudioManager.instance.Pause(SoundNames.GHOST_EDIBLE);
		}

		/// <summary>
		/// Makes the Ghost flash between two colours for a certain amount of time.
		/// </summary>
		private IEnumerator Flash(Color one, Color two)
		{
			float flashTimer = 0;
			float timeBetweenFlash = .2f;

			do
			{
				bodyColour.material.color = one;
				yield return new WaitForSeconds(timeBetweenFlash);

				bodyColour.material.color = two;
				yield return new WaitForSeconds(timeBetweenFlash);

				flashTimer += timeBetweenFlash * 2;
			}
			while (flashTimer < Constants.POWERUP_DURATION);

			// Wait for a little in case of overlap
			yield return new WaitForSeconds(.1f);
		}

		public void Reset()
		{
			eaten = false;
			edible = false;
			runningHome = false;
			bodyColour.enabled = true;
			speed = movingSpeed;
			bodyColour.material = originalColour;
		}

		public void RunHome()
		{
			AudioManager.instance.Play("Ghost Run");

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

		public float GetMovingSpeed()
		{
			return movingSpeed;
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
			Debug("has selected a new path: " + path.transform.name);
		}

		/// <summary>
		/// Custom method for printing to the console with specified transform name colour.
		/// </summary>
		private void Debug(string message)
		{
			print("<color=" + debugColour + "><b>" + transform.name + "</b></color> " + message);
		}
	}
}