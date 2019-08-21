using UnityEngine;
using System.Collections;
using Manager;
using System.Collections.Generic;
using System.Linq;

namespace Ghosts
{
	public class Ghost : MonoBehaviour
	{
		public bool edible = false;
		public bool eaten = false;
		public bool runningHome = false;
		private bool startedFlashCoroutine = false;

		public float speed;
		public float movingSpeed;
		public float flashingSpeed;
		public float eatenSpeed;

		public Material originalColour;

		private GhostPath path;
		private Rigidbody rb;
		private MeshRenderer bodyColour;

		[SerializeField]
		private string debugColour;

		void Start()
		{
			rb = GetComponent<Rigidbody>();

			path = GetRandomPath();
			Debug("has selected path: " + path.gameObject.name);

			bodyColour = GameObject.Find(gameObject.name + "/Model/Body").GetComponent<MeshRenderer>();
		}

		void FixedUpdate()
		{
			Move();
		}

		void Move()
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

		public void BecomeEdible()
		{
			edible = true;
			speed = flashingSpeed;

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

			edible = false;
			startedFlashCoroutine = false;

			if (!runningHome)
				speed = movingSpeed;

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
				SelectNewPath();
			}
		}

		public MeshRenderer GetBodyColour()
		{
			return this.bodyColour;
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

		public void SelectNewPath()
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