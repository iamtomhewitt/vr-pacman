using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour 
{
    public string gyroscopeScene;
    public string accelerometerScene;

    void OnMouseDown()
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
}
