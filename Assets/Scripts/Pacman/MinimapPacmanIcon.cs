using UnityEngine;

namespace Pacman
{
	/// <summary>
	/// A script to control the pacman icon on the minimap.
	/// </summary>
	public class MinimapPacmanIcon : MonoBehaviour
	{
		[SerializeField] private Transform pacman;
		[SerializeField] private Vector3 transformOffset;
		[SerializeField] private Vector3 rotationOffset;

		private void Update()
		{
			transform.position = pacman.position + transformOffset;
			transform.rotation = Quaternion.Euler(rotationOffset.x, pacman.rotation.eulerAngles.y + rotationOffset.y, rotationOffset.z);
		}
	}
}
