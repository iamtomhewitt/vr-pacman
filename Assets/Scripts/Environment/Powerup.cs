using UnityEngine;

namespace Environment
{
	/// <summary>
	/// The powerup that makes the Ghosts flash.
	/// </summary>
	public class Powerup : MonoBehaviour
	{
		[SerializeField] private Vector3 speed;

		private void Update()
		{
			transform.Rotate(speed);
		}
	}
}