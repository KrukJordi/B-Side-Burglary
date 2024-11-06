using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 5f;
    public float turnSpeed = 180f;
    public float sprintMultiplier = 2f; // Multiplier for sprint speed

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the player based on horizontal input
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        // Calculate movement direction based on the player's forward direction
        float currentSpeed = speed;

        // Check if the sprint key (left shift) is held down
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Joystick1Button1);
        if (isSprinting)
        {
            currentSpeed *= sprintMultiplier;
        }

        Vector3 movDir = transform.forward * Input.GetAxis("Vertical") * currentSpeed;

        // Apply the movement using CharacterController, and add a small downward force to keep grounded
        controller.Move(movDir * Time.deltaTime - Vector3.up * 0.1f);
    }
}