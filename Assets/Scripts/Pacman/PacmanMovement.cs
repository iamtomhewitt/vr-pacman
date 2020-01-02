using System.Collections;
using UnityEngine;
using Utility;
using Manager;

namespace Pacman
{
	public class PacmanMovement : MonoBehaviour
	{
		[SerializeField] private bool debug = false;

		[Space()]
		[SerializeField] private float speed;
		[SerializeField] private float originalSpeed;
		[SerializeField] private float boostSpeed;
		[SerializeField] private float sensitivity;

		private Rigidbody rb;
		private Debugger debugger;

		public static PacmanMovement instance;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			debugger = GetComponent<Debugger>();
			rb = GetComponent<Rigidbody>();

			originalSpeed = speed;
			speed = 0f;

			sensitivity = GameSettingsManager.instance ? GameSettingsManager.instance.GetSensitivity() : 7.5f;
		}

		private void Update()
		{
			transform.Rotate(0f, Input.acceleration.x * sensitivity, 0f);
			transform.position += transform.forward * speed * Time.deltaTime;

			// Reset the velocity to stop pacman drifting away
			rb.velocity = Vector3.zero;
		}

		/// <summary>
		/// Boosts Pacmans speed for an amount of time.
		/// </summary>
		public void BoostSpeed()
		{
			debugger.Info("boosting speed");
			StartCoroutine(BoostSpeedRoutine());
		}

		public IEnumerator BoostSpeedRoutine()
		{
			speed = boostSpeed;
			yield return new WaitForSeconds(Constants.POWERUP_DURATION);
			speed = originalSpeed;
		}

		/// <summary>
		/// Resets the speed to the original movement speed.
		/// </summary>
		public void ResetSpeed()
		{
			debugger.Info("resetting speed");
			speed = originalSpeed;
		}

		/// <summary>
		/// Resets the position of Pacman to the start.
		/// </summary>
		public void ResetPosition()
		{
			debugger.Info("resetting position");
			transform.position = new Vector3(0f, 0f, -3.43f);
		}

		/// <summary>
		/// Stops Pacman moving.
		/// </summary>
		public void Stop()
		{
			debugger.Info("stopping");
			speed = 0f;
		}
	}
}