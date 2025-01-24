using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    // Reference to the player controller
    public CubeController playerController;

    // Disable player controls first
    public void OnTriggerEnter(Collider other)
    {
        // Check if the player is passing through the end of the level trigger
        if (other.CompareTag("Player"))
        {
            // Disable the players movement
            playerController.allowInput = false;     
        }
    }

    // Move the camera after player is out of the trigger
    // This adds as slight pause between losing control and moving the camera to display stats
    public void OnTriggerExit(Collider other)
    {
        // Check if the camera is passing through the end of the level trigger
        if (other.CompareTag("MainCamera"))
        {
            Animator camAnimator = other.GetComponent<Animator>();
            camAnimator.enabled = true;

            PlayCameraEndingAnimation(camAnimator);
        }
    }


    public void PlayCameraEndingAnimation(Animator camAnimator)
    {
        camAnimator.Play("CameraEndLevel");
    }

}
