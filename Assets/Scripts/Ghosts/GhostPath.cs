using Ghosts;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Ghosts
{
	public class GhostPath : MonoBehaviour
	{
		public List<Transform> waypoints;

		private bool used;
		private int currentWaypointIndex = 0;

		public Transform GetCurrentWaypoint()
		{
			return waypoints[currentWaypointIndex];
		}

		public void SetNextWaypoint()
		{
			currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
		}

		public void ResetCurrentWaypointIndex()
		{
			currentWaypointIndex = 0;
		}

		public bool isUsed()
		{
			return used;
		}

		public void SetUsed(bool used)
		{
			this.used = used;
		}
	}
}

[CustomEditor(typeof(GhostPath))]
public class GhostPathEditor : Editor
{
	GhostPath ghostPath = null;

	void OnEnable()
	{
		ghostPath = (GhostPath)target;
	}


	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		if (GUILayout.Button("Assign using all child objects (Must be named 'Waypoint')"))
		{
			foreach (Transform child in ghostPath.transform)
			{
				if (child.name.Contains("Waypoint"))
				{
					ghostPath.waypoints.Add(child);
				}
			}
		}

		if (GUILayout.Button("Rename to 'Waypoint'"))
		{
			foreach (Transform child in ghostPath.transform)
			{
				if (!child.name.Contains("Waypoint"))
				{
					string name = child.name;
					name = name.Replace("Node", "Waypoint");
					child.transform.name = name;
				}
			}
		}

		if (GUILayout.Button("Reset"))
		{
			ghostPath.waypoints.Clear();
		}
	}
}
