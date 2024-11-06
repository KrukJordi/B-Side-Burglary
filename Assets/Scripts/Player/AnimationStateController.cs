using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController_2 : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int animSpeedHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        animSpeedHash = Animator.StringToHash("animSpeed");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w") || Input.GetAxis("Vertical") > 0;
        bool backwardPressed = Input.GetKey("s") || Input.GetAxis("Vertical") < 0;
        bool turnPressed = Input.GetKey("a") || Input.GetKey("d") || Input.GetAxis("Horizontal") != 0;
        bool runPressed = Input.GetKey("left shift") || Input.GetKey(KeyCode.Joystick1Button1);

        // Handle walking
        if (!isWalking && (forwardPressed || backwardPressed || turnPressed))
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (isWalking && !forwardPressed && !backwardPressed && !turnPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }

        // Handle running
        if (!isRunning && ((forwardPressed || backwardPressed || turnPressed) && runPressed))
        {
            animator.SetBool(isRunningHash, true);
        }

        if (isRunning && (!(forwardPressed || backwardPressed || turnPressed) || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }

        // Set animation speed based on direction and running state
        if ((forwardPressed || turnPressed) && !runPressed)
        {
            animator.SetFloat(animSpeedHash, 1.0f); // Normal walking speed
        }
        else if (backwardPressed && !runPressed)
        {
            animator.SetFloat(animSpeedHash, -1.0f); // Reverse walking speed
        }
        else if ((forwardPressed || turnPressed) && runPressed)
        {
            animator.SetFloat(animSpeedHash, 1.0f); // Normal running speed
        }
        else if (backwardPressed && runPressed)
        {
            animator.SetFloat(animSpeedHash, -1.0f); // Reverse running speed
        }
    }
}
