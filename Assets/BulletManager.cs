using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object the bullet collided with has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Find the "Tutorial" GameObject in the scene
            GameObject tutorialObject = GameObject.Find("Tutorial");

            if (tutorialObject != null)
            {
                // Get the TutorialTrackMovement component from the "Tutorial" GameObject
                TutorialTrackMovement trackMovement = tutorialObject.GetComponent<TutorialTrackMovement>();

                if (trackMovement != null)
                {
                    // Call the Collision method in TutorialTrackMovement
                    trackMovement.Collision();
                }
                else
                {
                    Debug.LogError("No TutorialTrackMovement component found on the 'Tutorial' object!");
                }
            }
            else
            {
                Debug.LogError("'Tutorial' GameObject not found in the scene!");
            }
        }
    }
}