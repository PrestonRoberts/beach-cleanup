using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    // Player movement
    public float playerSpeed = 10f;
    public float sprintSpeed = 15f;
    public float gravity = -9.81f;

    // Ground check
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    Vector3 velocity;

    // Movement enabled
    public bool movementEnabled;

    private void Start()
    {
        DisableMovement();
    }

    // Update is called once per frame
    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            // Reset velocity
            velocity.y = -2f;
        }

        // Check if movement is enabled
        if (!movementEnabled) return;

        // Get input
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // Move player
        Vector3 move = transform.right * x + transform.forward * z;

        // Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }
        else
        {
              controller.Move(move * playerSpeed * Time.deltaTime);
        }

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2f * gravity * 1.5f);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Enable movement
    public void EnableMovement()
    {
        movementEnabled = true;
        controller.enabled = true;
    }

    // Disable movement
    public void DisableMovement()
    {
        movementEnabled = false;
        controller.enabled = false;
    }
}
