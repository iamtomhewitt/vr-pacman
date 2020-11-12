using UnityEngine;

public class Teleporter : MonoBehaviour
{
	[SerializeField] private Transform teleportTo;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag.Equals(Tags.PLAYER))
		{
			other.gameObject.transform.position = teleportTo.position;
			other.gameObject.transform.rotation = teleportTo.rotation;
		}
	}
}


