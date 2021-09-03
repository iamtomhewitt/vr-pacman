using UnityEngine;
using Utility;

namespace Environment
{
	public class Wall : MonoBehaviour
	{
		[SerializeField] private Material halloweenMaterial;
		[SerializeField] private Material halloweenTopMaterial;
		[SerializeField] private Material christmasMaterial;
		[SerializeField] private Material christmasTopMaterial;

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
				materials[1] = halloweenTopMaterial;
			}
			else if (Utilities.isDecember())
			{
				materials[0] = christmasMaterial;
				materials[1] = christmasTopMaterial;
			}

			meshRenderer.materials = materials;
		}
	}
}