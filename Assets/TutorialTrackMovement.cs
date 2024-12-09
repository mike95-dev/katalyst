using UnityEngine;
using System.Collections;

public class TutorialTrackMovement : MonoBehaviour
{
    private GameObject turn1Object;  // Reference to the object named "Turn1"
    private GameObject turn2Object;  // Reference to the object named "Turn2"
    private GameObject endObject; // Reference to the object named "End"
    private GameObject player;  // Reference to the object named "Player"
    private bool turn1 = false;      // Boolean flag to track if the message has been printed
    private bool turn2 = false;      // Boolean flag to track if the message has been printed
    private bool end = false; // Boolean flag to track if the message has been printed
    public bool canMove = true;      // Variable to control movement

    void Start()
    {
        // Find the GameObject named "Turn1"
        turn1Object = GameObject.Find("Turn1");

        // Ensure Turn1 is found
        if (turn1Object == null)
        {
            Debug.LogError("Turn1 object not found!");
        }

        turn2Object = GameObject.Find("Turn2");

        // Ensure Turn2 is found
        if (turn2Object == null)
        {
            Debug.LogError("Turn2 object not found!");
        }

        endObject = GameObject.Find("End");

        // Ensure End is found
        if (endObject == null)
        {
            Debug.LogError("End object not found!");
        }

        player = GameObject.Find("Player");

        // Ensure Player is found
        if (player == null)
        {
            Debug.LogError("Player object not found!");
        }
    }

    void Update()
    {
        if (canMove)
        {
            // Move the track along the z-axis
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.25f);

            // If Turn1 is found, check its z-position and print the message only once
            if (turn1Object != null && !turn1 && turn1Object.transform.position.z < 50f)
            {
                Debug.Log("Turn1 has reached a z-position less than 50!");

                // Set the boolean flag to true to prevent further printing
                turn1 = true;

                // Call the coroutine to smooth the rotation and position change
                StartCoroutine(SmoothRotateAndMove(-30, 1158, turn1Object.transform.position, turn1Object, -10f, 2.5f));
            }

            if (turn2Object != null && !turn2 && turn2Object.transform.position.z < 50f)
            {
                Debug.Log("Turn2 has reached a z-position less than 50!");

                // Set the boolean flag to true to prevent further printing
                turn2 = true;

                // Call the coroutine to smooth the rotation and position change
                StartCoroutine(SmoothRotateAndMove(15, 245, turn2Object.transform.position, turn2Object, 350f, 1.5f));
            }

            if (endObject != null && !end && endObject.transform.position.z < 50f)
            {
                Debug.Log("End has reached a z-position less than 50!");
                end = true;
                if (MenuManager.instance != null)
                {
                    canMove = false;
                    MenuManager.instance.ActivateEndScreenState();
                }
            }
        }
    }

    // Coroutine for smooth rotation and movement using RotateAround
    IEnumerator SmoothRotateAndMove(float targetYRotation, float targetXPosition, Vector3 rotationCenter, GameObject turn, float offset, float dur)
    {
        float duration = dur; // The duration over which to complete the movement and rotation
        float timeElapsed = 0f;

        Vector3 initialPosition = transform.position;  // Starting position
        Vector3 targetPosition = new Vector3(targetXPosition, transform.position.y, transform.position.z - offset);  // Target position

        // Smoothly move and rotate the object over the specified duration
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;

            // Interpolate the position
            transform.position = Vector3.Lerp(initialPosition, targetPosition, timeElapsed / duration);

            // Ensure rotationCenter is in world space
            rotationCenter = turn.transform.position;

            // Rotate around the rotation center smoothly
            transform.RotateAround(rotationCenter, Vector3.up, targetYRotation * (Time.deltaTime / duration));

            // Wait for the next frame
            yield return null;
        }

        // Ensure the object is set to the exact target position after the loop
        transform.position = targetPosition;
    }

    IEnumerator CollisionHandler()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(3f);

        if (turn2)
        {
            // Reset positions and rotation
            if (player != null)
            {
                player.transform.position = new Vector3(0f, 33.3f, -5.892506f); // Use 'f' for floats
            }
            transform.position = new Vector3(245f, 5.5f, -3600f); // Use 'f' for floats
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -15f, transform.eulerAngles.z);
            turn1 = true;
            turn2 = true;
        }
        else if (turn1)
        {
            // Reset positions and rotation
            if (player != null)
            {
                player.transform.position = new Vector3(0f, 0f, -5.892506f); // Use 'f' for floats
            }
            transform.position = new Vector3(1158f, 5.5f, -1975f); // Use 'f' for floats
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -30f, transform.eulerAngles.z);
            turn1 = true;
            turn2 = false;
        }
        else
        {
            // Reset positions and rotation
            if (player != null)
            {
                player.transform.position = new Vector3(0f, 0f, -5.892506f); // Use 'f' for floats
            }
            transform.position = new Vector3(62.5f, 5.5f, 200f); // Use 'f' for floats
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
            turn1 = false;
            turn2 = false;
        }

        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        canMove = true;
        CubeController cubecontroller = player.GetComponent<CubeController>();

        if (cubecontroller != null)
        {
            // Call the Collision method in CubeController
            cubecontroller.SetMovement(true); // Corrected from CubeController.SetMovement(true)
        }
        else
        {
            Debug.LogError("No CubeController component found on the 'Player' object!");
        }
        PlayerShootingTowardsCrosshair shootingscript = player.GetComponent<PlayerShootingTowardsCrosshair>();

        if (shootingscript != null)
        {
            shootingscript.Collision(false);
        }
        else
        {
            Debug.LogError("No shootingscript component found on the 'Player' object!");
        }
    }

    public void Collision()
    {
        Debug.Log("Collided!");
        canMove = false;
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        // Loop through all objects and delete those named "Lazer"
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "Lazer")
            {
                Destroy(obj);
            }
        }

        CubeController cubecontroller = player.GetComponent<CubeController>();

        if (cubecontroller != null)
        {
            // Call the SetMovement method in CubeController
            cubecontroller.SetMovement(false); // Corrected from CubeController.SetMovement(false)
        }
        else
        {
            Debug.LogError("No CubeController component found on the 'Player' object!");
        }

        PlayerShootingTowardsCrosshair shootingscript = player.GetComponent<PlayerShootingTowardsCrosshair>();

        if (shootingscript != null)
        {
            shootingscript.Collision(true); 
        }
        else
        {
            Debug.LogError("No shootingscript component found on the 'Player' object!");
        }

        StartCoroutine(CollisionHandler());
    }
}