using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
	public class Version : MonoBehaviour
	{
		private void Start()
		{
			GetComponent<Text>().text = "Version: " + Application.version;
		}
	}
}