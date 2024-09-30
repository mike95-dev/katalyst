using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float maxSpeed = 200f; // Maximum speed the cube can reach
    public float acceleration = 200f; // Acceleration rate
    public float rotationSpeed = 20f;
    private Vector3 currentVelocity = Vector3.zero; // Keeps track of the current velocity
    private Vector3 inputDirection = Vector3.zero; // Direction of input

    private Transform cameraTransform; // Reference to the camera
    public Vector3 cameraOffset = new Vector3(20f, 3f, -5f); // Camera offset
    public float tiltAngle = 25f; // Angle to tilt the cube during movement
    private Quaternion targetRotation; // Target rotation for the cube

    // Define the 50 meter area boundaries (assuming the area is centered at the origin)
    public float areaRadius = 50f; // Radius of the movement area

    void Start()
    {
        cameraTransform = Camera.main.transform;
        cameraTransform.position = transform.position + cameraOffset;
    }

    void Update()
    {

        // Get the main camera
        Camera mainCamera = Camera.main;

        // Define an array to store the frustum corners
        Vector3[] frustumCorners = new Vector3[4];

        // Calculate the frustum corners on the near clipping plane
        mainCamera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), mainCamera.nearClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);

        // Convert the frustum corners to world space
        for (int i = 0; i < 4; i++)
        {
            frustumCorners[i] = mainCamera.transform.TransformPoint(frustumCorners[i]);
        }

        // Get input from WASD or Arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the direction of the input
        inputDirection = new Vector3(moveHorizontal, moveVertical, 0f).normalized;

        // Accelerate the cube in the direction of input
        if (inputDirection != Vector3.zero)
        {
            currentVelocity += inputDirection * acceleration * Time.deltaTime;

            // Clamp the velocity to not exceed maxSpeed
            if (currentVelocity.magnitude > maxSpeed)
            {
                currentVelocity = currentVelocity.normalized * maxSpeed;
            }
        }
        else
        {
            // Decelerate the cube when no input is pressed
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, acceleration * Time.deltaTime);
        }

        // Apply the movement based on current velocity
        Vector3 newPosition = transform.position + currentVelocity * Time.deltaTime * 0.15f;

        // Clamp the cube's position to be within the defined 50-meter area
        newPosition.x = Mathf.Clamp(newPosition.x, -areaRadius, areaRadius);
        newPosition.y = Mathf.Clamp(newPosition.y, -areaRadius, areaRadius);

        // Apply the new clamped position
        transform.position = newPosition;

        // Handle rotation based on input keys
        if (moveVertical > 0)
            targetRotation = Quaternion.Euler(-tiltAngle, 0f, 0f);
        else if (moveVertical < 0)
            targetRotation = Quaternion.Euler(tiltAngle, 0f, 0f);
        else if (moveHorizontal > 0)
            targetRotation = Quaternion.Euler(0f, 0f, -tiltAngle);
        else if (moveHorizontal < 0)
            targetRotation = Quaternion.Euler(0f, 0f, tiltAngle);
        else
            targetRotation = Quaternion.Euler(0f, 0f, 0f); // Reset rotation

        // Handle diagonal movement
        if (moveHorizontal > 0 && moveVertical > 0)
            targetRotation = Quaternion.Euler(-tiltAngle, 0f, -tiltAngle);
        else if (moveHorizontal < 0 && moveVertical > 0)
            targetRotation = Quaternion.Euler(-tiltAngle, 0f, tiltAngle);
        else if (moveHorizontal > 0 && moveVertical < 0)
            targetRotation = Quaternion.Euler(tiltAngle, 0f, -tiltAngle);
        else if (moveHorizontal < 0 && moveVertical < 0)
            targetRotation = Quaternion.Euler(tiltAngle, 0f, tiltAngle);

        // Smooth rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Camera Barriers

        Vector3 updatedCameraPos = transform.position;

        Vector2 xVariables = new Vector2(-42, 42);
        Vector2 yVariables = new Vector2(-40, 46);

        if (xVariables.y < updatedCameraPos.x)
        {
            updatedCameraPos.x = xVariables.y;
        }
        if (xVariables.x > updatedCameraPos.x)
        {
            updatedCameraPos.x = xVariables.x;
        }
        if (yVariables.y < updatedCameraPos.y)
        {
            updatedCameraPos.y = yVariables.y;
        }
        if (yVariables.x > updatedCameraPos.y)
        {
            updatedCameraPos.y = yVariables.x;
        }
        cameraTransform.position = updatedCameraPos + cameraOffset;

        // Make the camera look at the cube
        cameraTransform.LookAt(updatedCameraPos);
    }
}