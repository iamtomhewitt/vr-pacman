using UnityEngine;

public class Debugger : MonoBehaviour
{
	[SerializeField] private bool debug;
	[SerializeField] private string debugColour;

	public void Info(string message)
	{
		if (debug)
		{
			print("<color=" + debugColour + "><b>" + transform.name + "</b></color>: " + message);
		}
	}

	public void Error(string message)
	{
		if (debug)
		{
			print("<color=" + debugColour + "><b>" + transform.name + "</b></color>: <color=red>" + message + "</color>");
		}
	}
}
