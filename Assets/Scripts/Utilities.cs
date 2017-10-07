using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{
    public string gyroscopeScene;
    public string accelerometerScene;

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadSceneOnSystemType()
    {
        if (SystemInfo.supportsGyroscope)
        {
            SceneManager.LoadScene(gyroscopeScene);
        }
        else
        {
            SceneManager.LoadScene(accelerometerScene);
        }
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
