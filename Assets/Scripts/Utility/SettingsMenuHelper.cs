using UnityEngine;
using Utility;

/// <summary>
/// Helper class for actions performed in the main menu.
/// </summary>
public class SettingsMenuHelper : MonoBehaviour
{
	[SerializeField] private GameObject halloweenUi;
	[SerializeField] private GameObject christmasUi;
	[SerializeField] private GameObject normalParticleSystem;

	private void Start()
	{
		if (Utilities.isOctober())
		{
			halloweenUi.SetActive(true);
			normalParticleSystem.SetActive(false);
		}

		if (Utilities.isDecember())
		{
			christmasUi.SetActive(true);
			normalParticleSystem.SetActive(false);
		}
	}
}