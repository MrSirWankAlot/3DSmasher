using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    LayerMask playerMask;

    [SerializeField]
    Transform groundCheckTransform;
    Rigidbody playerBody;
    float horizontalAxis;
    float verticalAxis;
    bool jumpedButtonPressed;

    bool isGrounded;

    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        jumpedButtonPressed = Input.GetKey(KeyCode.Space);
        isGrounded = Physics.OverlapSphere(groundCheckTransform.position, 0.4f, playerMask).Length > 0;
    }

    void FixedUpdate() {
        playerBody.velocity = new Vector3(horizontalAxis * speed, playerBody.velocity.y , verticalAxis * speed);

        if (isGrounded && jumpedButtonPressed && playerBody.velocity.y == 0)
        {
            playerBody.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jumpedButtonPressed = false;
        }
    }
}
