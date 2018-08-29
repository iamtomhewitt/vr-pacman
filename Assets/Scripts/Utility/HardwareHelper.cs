using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class HardwareHelper : MonoBehaviour 
{
    public GameObject accelerometerControls;
    public GameObject gyroscopeControls;
    public bool useGyro;

    void Start()
    {
        useGyro = SystemInfo.supportsGyroscope;

        if (useGyro)   accelerometerControls.SetActive(false);
        else           gyroscopeControls.SetActive(false);
    }

    public void Load(string scene)
    {
        accelerometerControls.SetActive(false);
        gyroscopeControls.SetActive(false);
        GameObject.FindObjectOfType<Utilities>().LoadSceneWithCountdown(scene);
    }
}
