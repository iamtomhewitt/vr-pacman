using Ghosts;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Ghosts
{
	public class GhostPath : MonoBehaviour
	{
		[SerializeField] private List<Transform> waypoints;

		private bool used;
		private int currentWaypointIndex = 0;

		public Transform GetCurrentWaypoint()
		{
			return waypoints[currentWaypointIndex];
		}

		public List<Transform> GetWaypoints()
		{
			return waypoints;
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