using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    Camera myCamera;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    bool isJumpPressed = false;
    private Rigidbody rb;

    private float rotationCameraX = 0f;

    // Important to keep Track of the curretnt rotation etc otherwise its always 0
    private float currentCameraRotation = 0f;

    [SerializeField]
    private float cameraRotationLimit = 85f;

    [SerializeField]
    private float jumpForce = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Gets movement  vector
    public void Move(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void Rotate(Vector3 rotation)
    {
        this.rotation = rotation;
    }

    public void RotateCamera(float rotationCameraX)
    {
        this.rotationCameraX = rotationCameraX;
    }

    public void Jump(bool isJumpPressed)
    {
        this.isJumpPressed = isJumpPressed;
    }

    //Run every physics iteration
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    private void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (myCamera != null)
        {
            // Rotation not clampable 
            //camera.transform.Rotate(-rotationCamera);.

            currentCameraRotation -= rotationCameraX;
            currentCameraRotation = Mathf.Clamp(currentCameraRotation, -cameraRotationLimit, cameraRotationLimit);
            myCamera.transform.localEulerAngles = new Vector3(currentCameraRotation, 0f, 0f);
        }
    }

    //Perform movement based on velocity variable
    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if (isJumpPressed && rb.velocity.y == 0)
        {
            Vector3 jumpForce = Vector3.up * this.jumpForce;
            rb.AddForce(jumpForce, ForceMode.VelocityChange);
            isJumpPressed = false;
        }
    }
}
