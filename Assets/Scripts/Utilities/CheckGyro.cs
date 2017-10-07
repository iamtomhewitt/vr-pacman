using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CheckGyro : MonoBehaviour 
{
	void Start () 
    {
        if (SystemInfo.supportsGyroscope)
        {
            GetComponent<TextMesh>().text = "Detected a gyroscope in your device. \nThe game will work! Loading now...";
            StartCoroutine(LoadAfterTime());
        }
        else
        {
            GetComponent<TextMesh>().text = "Sorry, there is no gyroscope detected in your device. \nThe game cannot be played. \nPlease use a device that has a gyroscope! :-)";
        }
	}

    IEnumerator LoadAfterTime()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
}
