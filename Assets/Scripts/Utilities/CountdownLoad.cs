using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountdownLoad : MonoBehaviour 
{
    public int startTime;
    public TextMesh text;
    public string sceneName;

	void Start () 
    {
        InvokeRepeating("Countdown", 1f, 1f);
	}

    void Countdown()
    {
        if (startTime == 0)
        {
            text.text = "STARTING GAME...";
            CancelInvoke("Countdown");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            startTime--;
            text.text = startTime.ToString();
        }
    }
}
