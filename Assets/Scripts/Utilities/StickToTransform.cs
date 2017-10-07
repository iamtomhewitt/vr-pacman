using UnityEngine;
using System.Collections;

public class StickToTransform : MonoBehaviour 
{
    public GameObject whatToStickTo;
    
    void Update()
    {
        // For the overhead pacman sprite - makes its position the same as the player
        transform.position = new Vector3(whatToStickTo.transform.position.x, 5f, whatToStickTo.transform.position.z);
        transform.rotation = Quaternion.Euler(90f, whatToStickTo.transform.rotation.eulerAngles.y - 90f, 0f);
    }
}
