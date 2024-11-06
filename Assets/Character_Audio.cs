using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    public AudioClip[] walkingFootsteps; // Array of walking footstep sounds
    public AudioClip[] runningFootsteps; // Array of running footstep sounds
    public float walkStepInterval = 0.5f; // Interval between footsteps while walking
    public float runStepInterval = 0.3f; // Interval between footsteps while running
    private AudioSource audioSource;
    private CharacterController characterController;
    private float stepTimer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
        stepTimer = walkStepInterval; // Start with walking interval
    }

    void Update()
    {
        if (characterController.isGrounded && characterController.velocity.magnitude > 0.1f)
        {
            bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Joystick1Button1);

            // Adjust the step interval based on whether the player is running
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0)
            {
                PlayFootstepSound(isRunning);
                stepTimer = isRunning ? runStepInterval : walkStepInterval;
            }
        }
        else
        {
            stepTimer = walkStepInterval; // Reset to walking interval
        }
    }

    void PlayFootstepSound(bool isRunning)
    {
        AudioClip[] footstepClips = isRunning ? runningFootsteps : walkingFootsteps;

        if (footstepClips.Length > 0)
        {
            AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}
//code by Miguel Mensinga