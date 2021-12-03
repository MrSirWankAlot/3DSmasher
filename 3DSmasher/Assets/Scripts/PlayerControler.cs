using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handelt nur die Inputs und Sachen die der PLayer besitzt 
public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    float lookSensitivity = 5f;
    [SerializeField]
    Transform isGroundedTransform;
    [SerializeField]
    LayerMask whatIsGround;

    private PlayerMotor motor;
    private bool isGrounded;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        //Calculate movement velocity as a 3D vector
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");
        bool isJumpPressed = Input.GetButton("Jump");
        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        //final movement vector and GroundedCalc
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;
        if (isGroundedTransform != null)
        {
            isGrounded = Physics.OverlapSphere(isGroundedTransform.position, 0.2f, whatIsGround).Length > 0;

            //apply movement
            motor.Jump(isJumpPressed && isGrounded);
        }

        motor.Move(_velocity);

        float yRotation = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, yRotation, 0f) * lookSensitivity;

        motor.Rotate(rotation);

        float xRotation = Input.GetAxisRaw("Mouse Y");

        float camRotationX = xRotation * lookSensitivity;

        motor.RotateCamera(camRotationX);
    }
}
