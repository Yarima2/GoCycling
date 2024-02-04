using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float normalMovementForce = 1.0f;
    public float fastMovementForce = 2.0f;
    public float maxVelocity = 100f;
    public Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleKeyboardInput();
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
    }

    private void HandleKeyboardInput()
    {
        Vector3 newPosition = transform.position;
        float movementForce;
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            movementForce = fastMovementForce;
        }
        else {
            movementForce = normalMovementForce;
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && rigidBody.velocity.z < maxVelocity)
        {
            rigidBody.AddForce(transform.forward * movementForce, ForceMode.Force);
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && rigidBody.velocity.z > -maxVelocity)
        {
            rigidBody.AddForce(-transform.forward * movementForce, ForceMode.Force);
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && rigidBody.velocity.x < maxVelocity)
        {
            rigidBody.AddForce(transform.right * movementForce, ForceMode.Force);
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && rigidBody.velocity.x > -maxVelocity)
        {
            rigidBody.AddForce(-transform.right * movementForce, ForceMode.Force);
        }
    }
}
