using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanAccelerometerMovement : MonoBehaviour 
{
    public float speed;
    public float originalSpeed;
    public float acceleratorSensitivity;

    void Start () 
    {
        originalSpeed = speed;
        speed = 0f;
    }

    void Update()
    {
        transform.Rotate(0f, Input.acceleration.x * acceleratorSensitivity, 0f);

        // Stop us from moving up or down - tried with a Rigidbody but this didnt work
        if (transform.position.y < 0 || transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        transform.position += Camera.main.transform.forward * speed * Time.deltaTime;
    }

    void FixedUpdate () 
    {
        //transform.position += Camera.main.transform.forward * speed * Time.deltaTime;
    }
}
