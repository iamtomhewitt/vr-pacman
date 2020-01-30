using UnityEditor;
using UnityEngine;
using Ghosts;

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
					ghostPath.GetWaypoints().Add(child);
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
			ghostPath.GetWaypoints().Clear();
		}
	}
}
