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
		private bool startedFlashCoroutine = false;

		public float speed;
		public float movingSpeed;
		public float flashingSpeed;
		public float eatenSpeed;

		public GhostPath ghostPath;
		private Rigidbody rb;

		public Material originalColour;

		[HideInInspector]
		public MeshRenderer bodyColour;

		[HideInInspector]
		public CapsuleCollider capsuleCollider;

		void Start()
		{
			rb = GetComponent<Rigidbody>();
			capsuleCollider = GetComponent<CapsuleCollider>();

			bodyColour = GameObject.Find(gameObject.name + "/Model/Body").GetComponent<MeshRenderer>();
		}

		void FixedUpdate()
		{
			Move();
		}

		void Move()
		{
			// If we are not at the current node
			if (transform.position != ghostPath.GetCurrentWaypoint().position)
			{
				// Calculate moving from where we are to the next node
				Vector3 p = Vector3.MoveTowards(transform.position, ghostPath.GetCurrentWaypoint().position, speed);

				// Look at the next node
				transform.LookAt(ghostPath.GetCurrentWaypoint().position);

				// Move towards the current node
				rb.MovePosition(p);
			}
			else
			{
				// Increase the count i.e the next node
				ghostPath.SetNextWaypoint();
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
			}
		}
	}
}