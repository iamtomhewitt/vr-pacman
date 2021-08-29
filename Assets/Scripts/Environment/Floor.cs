using UnityEngine;
using Utility;

namespace Environment
{
	public class Floor : MonoBehaviour
	{
		[SerializeField] private Material halloweenMaterial;

		private void Start()
		{
			if (!Utilities.isOctober() && !Utilities.isDecember())
			{
				return;
			}

			MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
			Material[] materials = meshRenderer.materials;

			if (Utilities.isOctober())
			{
				materials[0] = halloweenMaterial;
			}
			else if (Utilities.isDecember())
			{
				// TODO
			}

			meshRenderer.materials = materials;
		}
	}
}