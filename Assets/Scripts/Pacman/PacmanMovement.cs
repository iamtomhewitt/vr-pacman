using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour 
{
    public float speed;
    public float originalSpeed;

	void Start () 
    {
        originalSpeed = speed;
        speed = 0f;
	}

    void Update()
    {
        // Stop us from moving up or down - tried with a Rigidbody but this didnt work
        if (transform.position.y < 0 || transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
	
	void FixedUpdate () 
    {
        transform.position += Camera.main.transform.forward * speed * Time.deltaTime;
	}
}
