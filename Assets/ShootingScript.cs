using UnityEngine;
using System.Collections;

public class PlayerShootingTowardsCrosshair : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign a bullet prefab in the inspector
    public Transform bulletSpawnPoint; // Assign a spawn point (a child object of Player, typically)
    public float bulletSpeed = 160f; // Speed of the bullet
    public float fireRate = 0.5f; // Half-second delay between shots
    public LayerMask ignoreLayer; // Layer mask for layers to ignore, assign "Player" layer here in the inspector
    private Camera mainCamera;
    private bool isFiring = false; // Flag to check if the player is currently firing
    public CrosshairFollowMouseWithRadius crosshairScript; // Reference to the crosshair script
    public bool canShoot = true;      // Variable to control movement
    public bool collision = false;      // Variable to control movement

    void Start()
    {
        mainCamera = Camera.main; // Cache the main camera reference
    }

    void Update()
    {
        if (canShoot && !collision)
        {
            // Detect if the left mouse button is pressed or right trigger is engaged (assuming axis name "RightTrigger")
            bool isShooting = Input.GetMouseButton(0) || Input.GetAxis("RightTrigger") > 0.5f;

            if (isShooting && !isFiring) // Trigger shooting if not already firing
            {
                StartCoroutine(FireContinuously());
            }
        }
    }

    IEnumerator FireContinuously()
    {
        isFiring = true; // Set firing flag to true

        while ((Input.GetMouseButton(0) || Input.GetAxis("RightTrigger") > 0.5f) && (canShoot & !collision)) // Continue firing while the button or trigger is held down
        {
            Shoot(); // Call the Shoot method
            yield return new WaitForSeconds(fireRate); // Wait for the fire rate duration before shooting again
        }

        isFiring = false; // Set firing flag to false when the player releases the button or trigger
    }

    void Shoot()
    {
        // Get the crosshair position in screen space (where the crosshair is rendered)
        Vector2 crosshairScreenPosition = crosshairScript.GetCrosshairPosition();

        // Invert the Y-axis for proper screen-to-world conversion
        crosshairScreenPosition.y = Screen.height - crosshairScreenPosition.y;

        // Cast a ray from the camera through the crosshair position into the world
        Ray ray = mainCamera.ScreenPointToRay(crosshairScreenPosition);
        RaycastHit hit;

        Vector3 targetPoint;

        // Check if the ray hits something in the world, ignoring the player layer
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~ignoreLayer))
        {
            // If we hit an object, set the target point to the hit point
            targetPoint = hit.point;
        }
        else
        {
            // If no object is hit, use a default far point in the direction of the ray
            targetPoint = ray.GetPoint(1000); // 1000 units away from the camera
        }

        // Calculate the direction from the bullet spawn point to the target point
        Vector3 direction = (targetPoint - bulletSpawnPoint.position).normalized;

        // Spawn the bullet at the bullet spawn point
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.name = "Lazer";

        // Set the velocity of the bullet towards the target
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;

        // Destroy the bullet after 5 seconds
        Destroy(bullet, 1.5f);
    }

    public void Shoot(bool value)
    {
        canShoot = value;
    }
    public void Collision(bool value)
    {
        collision = value;
    }
}