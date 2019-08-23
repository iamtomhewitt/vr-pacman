using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pacman
{
	/// <summary>
	/// A class for accessing the heads up display of the player.
	/// </summary>
	public class PacmanHud : MonoBehaviour
	{
		[SerializeField] private TextMesh statusText;

		public static PacmanHud instance;

		private void Awake()
		{
			instance = this;
		}

		public void SetStatusText(string message)
		{
			statusText.text = message;
		}
	}
}
