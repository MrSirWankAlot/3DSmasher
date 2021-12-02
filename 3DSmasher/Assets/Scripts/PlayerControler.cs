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

    private PlayerMotor motor;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        //Calculate movement velocity as a 3D vector
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        //final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        //apply movement
        motor.Move(_velocity);

        float yRotation = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, yRotation, 0f) * lookSensitivity;

        motor.Rotate(rotation);

        float xRotation = Input.GetAxisRaw("Mouse Y");

        Vector3 camRotation = new Vector3(xRotation, 0f, 0f) * lookSensitivity;

        motor.RotateCamera(camRotation);
    }
}
