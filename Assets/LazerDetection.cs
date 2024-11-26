using UnityEngine;

public class LazerRaycast : MonoBehaviour
{
    public float raycastDistance = 100f; // Maximum distance the raycast can detect collisions
    public LayerMask layerMask; // Set the layer mask to ignore certain layers if needed

    void Update()
    {
        // Calculate the direction of the laser
        Vector3 direction = transform.forward;

        // Perform a raycast in the forward direction
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        // Check if the ray hits something within the specified distance
        if (Physics.Raycast(ray, out hit, raycastDistance, layerMask))
        {
            // Check if the object hit has the tag "Bullet"
            if (hit.collider.CompareTag("Bullet"))
            {
                Debug.Log("hit");

                // Get the Renderer component of the hit object to access its material
                Renderer objectRenderer = hit.collider.GetComponent<Renderer>();

                if (objectRenderer != null)
                {
                    // Get the current color of the material
                    Color color = objectRenderer.material.color;

                    // Decrease the alpha (opacity) by 33%
                    color.a -= 0.33f;

                    // Apply the updated color back to the material
                    objectRenderer.material.color = color;

                    // Check if the alpha is below 10%, and destroy the object if so
                    if (color.a <= 0.1f)
                    {
                        Destroy(hit.collider.gameObject);
                    }
                }

                // Destroy the laser itself
                Destroy(gameObject);
            }
        }
    }
}