using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    // Disable player controls first
    public void OnTriggerEnter(Collider other)
    {
        // Check if the player is passing through the end of the level trigger
        if (other.CompareTag("Player"))
        {
            CubeController playerController = other.GetComponent<CubeController>();

            if (playerController != null)
            {
                // Disable the players movement
                playerController.allowInput = false;
            }
        }
    }

    // Move the camera after player is out of the trigger
    // This adds as slight pause between losing control and moving the camera to display stats
    // Dependant on how long the collider is
    public void OnTriggerExit(Collider other)
    {
        // Check if the camera is passing through the end of the level trigger
        if (other.CompareTag("MainCamera"))
        {
            Animator camAnimator = other.GetComponent<Animator>();

            if (camAnimator != null)
            {
                // Make sure the animator is enabled
                camAnimator.enabled = true;
                // Play the animation
                PlayCameraEndingAnimation(camAnimator);
            }
        }
    }

    // Function to play the animation
    public void PlayCameraEndingAnimation(Animator camAnimator)
    {
        camAnimator.Play("CameraEndLevel");
    }

}
