using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadButton : MonoBehaviour 
{
    public Material green, red;

    public string sceneName;

    public IEnumerator LoadScene(string s)
    {
        ChangeColour(green);

        yield return new WaitForSeconds(.75f);

        SceneManager.LoadScene(s);
    }

    public void ChangeColour(Material newColour)
    {
        GetComponent<Renderer>().material = newColour;
    }
}
