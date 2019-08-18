using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script to control the pacman icon on the minimap.
/// </summary>
public class MinimapPacmanIcon : MonoBehaviour
{
	public Transform pacman;
	public Vector3 transformOffset;
	public Vector3 rotationOffset;

	void Update()
	{
		transform.position = pacman.position + transformOffset;
		transform.rotation = Quaternion.Euler(rotationOffset.x, pacman.rotation.eulerAngles.y + rotationOffset.y, rotationOffset.z);
	}
}
