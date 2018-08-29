using UnityEngine;
//#if UNITY_EDITOR
//    using UnityEditor;
//#endif

public class LevelBuilder : MonoBehaviour 
{
    public Texture2D blueprint;
    public GameObject floor;
    public ColourMap[] colourMapping;

	void Start () 
    {
        GenerateLevel();
	}

    public void DestroyLevel(Transform parent)
    {
        foreach (Transform t in parent)
        {
            DestroyImmediate(t.gameObject);
           // DestroyLevel(t);
        }
    }

    public void GenerateLevel()
    {
        //GameObject f = Instantiate(floor, new Vector3(blueprint.width/2, -1.09f, blueprint.height/2), Quaternion.identity, transform) as GameObject;
        //f.transform.localScale = new Vector3(blueprint.width, 0, blueprint.height);

        for (int x = 0; x < blueprint.width; x++)
        {
            for (int z = 0; z < blueprint.height; z++)
            {
                GenerateTile(x, z);
            }
        }
    }

    void GenerateTile(int x, int z)
    {
        Color pixelColour = blueprint.GetPixel(x, z);

        if (pixelColour.a == 0 || pixelColour == Color.white)
        {
            // Pixel is transparent
            return;
        }

        foreach (ColourMap colourMap in colourMapping)
        {
            if (colourMap.colour.Equals(pixelColour))
            {
                Vector3 position = new Vector3(x, 0, z);
                Instantiate(colourMap.prefab, position, Quaternion.identity, colourMap.parent);
            }
        }
    }
}

//#if UNITY_EDITOR
//[CustomEditor(typeof(LevelBuilder))]
//public class LevelBuilderEditor : Editor
//{
//    LevelBuilder lb = null;
//
//    void OnEnable()
//    {
//        lb = (LevelBuilder)target;
//    }
//
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();
//
//        GUILayout.Space(15f);
//
//        if (GUILayout.Button("BUILD"))
//        {
//            lb.GenerateLevel();
//            Debug.Log("TODO: Separate method and map for each of the ghost paths. Do it in photoshop?");
//        }
//
//        GUILayout.Space(5f);
//
//        if (GUILayout.Button("DESTROY"))
//        {
//            lb.DestroyLevel(lb.transform);
//        }
//    }
//}
//#endif

[System.Serializable]
public class ColourMap
{
    public string name;
    public Color colour;
    public GameObject prefab;
    public Transform parent;
}
