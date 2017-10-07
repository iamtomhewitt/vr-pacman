using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour 
{
    public GameObject whatToLookAt;
    	
	void Update () 
    {
        transform.LookAt(whatToLookAt.transform);
	}
}
