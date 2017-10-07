using UnityEngine;
using System.Collections;

public class RaycastOut : MonoBehaviour 
{
    public bool hitButton;

    void Update()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 100f))
        {
            hitButton = true;
            StartCoroutine(hit.collider.GetComponent<LoadButton>().LoadScene(hit.collider.GetComponent<LoadButton>().sceneName));
        }
        else
        {
            hitButton = false;
        }
    }
}
