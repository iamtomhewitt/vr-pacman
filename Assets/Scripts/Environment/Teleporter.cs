using UnityEngine;

public class Teleporter : MonoBehaviour
{
	[SerializeField] private Transform teleportTo;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals(Tags.PLAYER))
		{
			Vector3 position = new Vector3(teleportTo.position.x, other.transform.position.y, teleportTo.position.z);

			other.gameObject.transform.position= position;
			other.gameObject.transform.rotation = teleportTo.rotation;
		}
	}
}


