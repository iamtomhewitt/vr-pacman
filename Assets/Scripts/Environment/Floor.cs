using UnityEngine;
using Utility;

namespace Environment
{
	public class Floor : MonoBehaviour
	{
		[SerializeField] private Material halloweenMaterial;
		[SerializeField] private Material christmasMaterial;

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
				materials[0] = christmasMaterial;
			}

			meshRenderer.materials = materials;
		}
	}
}