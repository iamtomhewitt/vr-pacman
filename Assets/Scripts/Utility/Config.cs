using SimpleJSON;
using UnityEngine;

public class Config : MonoBehaviour
{
	private JSONNode root;

	public static Config instance;

	private void Awake()
	{
		if (instance)
		{
			DestroyImmediate(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
			instance = this;
		}

		TextAsset configFile = Resources.Load<TextAsset>("config");

		if (configFile == null)
		{
			Debug.LogError("Could not load config from Resources/config.json");
		}

		root = JSON.Parse(configFile.text);
	}

	public JSONNode GetConfig()
	{
		return root;
	}
}