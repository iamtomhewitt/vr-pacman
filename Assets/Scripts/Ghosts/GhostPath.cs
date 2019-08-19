using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GhostPath : MonoBehaviour
{
	public Transform start;
	public Transform end;
	public List<Transform> waypoints;

	int currentWaypointIndex = 0;

	public Transform GetWaypoint()
	{
		return waypoints[currentWaypointIndex];
	}

	public void SetNextWaypoint()
	{
		currentWaypointIndex++;
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
				else if (child.name.Contains("Start"))
				{
					ghostPath.start = child;
				}
				else if (child.name.Contains("End"))
				{
					ghostPath.end = child;
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
	}
}
