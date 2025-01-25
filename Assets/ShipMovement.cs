using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    // Speed for how fast the ship travels
    public float speed;

    // Flag to start or stop ship movement
    public bool canMoveForward = true;

    // Start is called before the first frame update
    void Start()
    {
        // Multiply the speed by 10 for easier to use values
        speed *= 10;
    }

    // Update is called once per frame
    void Update()
    {
        // If ship can move, move it forward based on the speed
        if (canMoveForward)
        {
            // Get the current position
            Vector3 forward = transform.position;
            // Increment the z value
            forward.z += 10;
            // Move towards the new position
            transform.position = Vector3.MoveTowards(transform.position, forward, speed * Time.deltaTime);
        }
    }

    // Function to toggle movement flag
    public void ToggleMoveForward()
    {
        canMoveForward = !canMoveForward;
    }

}
