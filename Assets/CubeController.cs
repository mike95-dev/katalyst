using UnityEngine;

public class CubeController : MonoBehaviour
{
    public float baseMaxSpeed = 200f; // Base max speed for the cube
    public float acceleration = 200f; // Acceleration rate
    public float rotationSpeed = 20f;
    private Vector3 currentVelocity = Vector3.zero; // Keeps track of the current velocity
    private Vector3 inputDirection = Vector3.zero; // Direction of input

    private Transform cameraTransform; // Reference to the camera
    public Vector3 cameraOffset = new Vector3(20f, 3f, -5f); // Camera offset
    public float tiltAngle = 25f; // Base tilt angle for the cube during movement
    private Quaternion targetRotation; // Target rotation for the cube
    public bool canMovement = true;      // Variable to control movement
    public bool ADmove = true;      // Variable to control movement
    public bool WSmove = true;      // Variable to control movement

    public bool allowInput = true;  // Flag to enable or disable keyboard input

    public float moveHorizontal;   // Variables to hold input values
    public float moveVertical;

    // Define the 50 meter area boundaries (assuming the area is centered at the origin)
    public float areaRadius = 50f; // Radius of the movement area

    void Start()
    {
        cameraTransform = Camera.main.transform;
        cameraTransform.position = transform.position + cameraOffset;
    }

    void Update()
    {
        if (canMovement)
        {
            // Define speed multipliers based on the movement flags
            float verticalSpeedMultiplier = WSmove ? 1f : 0.2f;
            float horizontalSpeedMultiplier = ADmove ? 1f : 0.2f;

            // Adjust max speed for each axis
            float maxVerticalSpeed = baseMaxSpeed * verticalSpeedMultiplier;
            float maxHorizontalSpeed = baseMaxSpeed * horizontalSpeedMultiplier;

            // Flag to stop keyboard input without freezing the physics
            if (allowInput)
            {
                // Get input from keyboard or controller's left stick, applying speed multipliers
                moveHorizontal = Input.GetAxis("Horizontal") * horizontalSpeedMultiplier;
                moveVertical = Input.GetAxis("Vertical") * verticalSpeedMultiplier;
            }
            else
            {
                moveHorizontal = 0f;
                moveVertical = 0f;
            }

            // Calculate the direction of the input
            inputDirection = new Vector3(moveHorizontal, moveVertical, 0f).normalized;

            // Accelerate the cube in the direction of input
            if (inputDirection != Vector3.zero)
            {
                currentVelocity += inputDirection * acceleration * Time.deltaTime;

                // Clamp the velocity to the max speed for each direction
                currentVelocity.x = Mathf.Clamp(currentVelocity.x, -maxHorizontalSpeed, maxHorizontalSpeed);
                currentVelocity.y = Mathf.Clamp(currentVelocity.y, -maxVerticalSpeed, maxVerticalSpeed);
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

            // Adjust tilt angle based on movement multipliers
            float adjustedTiltAngleX = tiltAngle * verticalSpeedMultiplier;
            float adjustedTiltAngleZ = tiltAngle * horizontalSpeedMultiplier;

            // Handle rotation based on input keys or controller stick
            if (moveVertical > 0)
                targetRotation = Quaternion.Euler(-adjustedTiltAngleX, 0f, 0f);
            else if (moveVertical < 0)
                targetRotation = Quaternion.Euler(adjustedTiltAngleX, 0f, 0f);
            else if (moveHorizontal > 0)
                targetRotation = Quaternion.Euler(0f, 0f, -adjustedTiltAngleZ);
            else if (moveHorizontal < 0)
                targetRotation = Quaternion.Euler(0f, 0f, adjustedTiltAngleZ);
            else
                targetRotation = Quaternion.Euler(0f, 0f, 0f); // Reset rotation

            // Handle diagonal movement with adjusted tilt angles
            if (moveHorizontal > 0 && moveVertical > 0)
                targetRotation = Quaternion.Euler(-adjustedTiltAngleX, 0f, -adjustedTiltAngleZ);
            else if (moveHorizontal < 0 && moveVertical > 0)
                targetRotation = Quaternion.Euler(-adjustedTiltAngleX, 0f, adjustedTiltAngleZ);
            else if (moveHorizontal > 0 && moveVertical < 0)
                targetRotation = Quaternion.Euler(adjustedTiltAngleX, 0f, -adjustedTiltAngleZ);
            else if (moveHorizontal < 0 && moveVertical < 0)
                targetRotation = Quaternion.Euler(adjustedTiltAngleX, 0f, adjustedTiltAngleZ);

            // Smooth rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Camera barriers
            Vector3 updatedCameraPos = transform.position;
            Vector2 xVariables = new Vector2(-42, 42);
            Vector2 yVariables = new Vector2(-40, 46);

            if (xVariables.y < updatedCameraPos.x)
                updatedCameraPos.x = xVariables.y;
            if (xVariables.x > updatedCameraPos.x)
                updatedCameraPos.x = xVariables.x;
            if (yVariables.y < updatedCameraPos.y)
                updatedCameraPos.y = yVariables.y;
            if (yVariables.x > updatedCameraPos.y)
                updatedCameraPos.y = yVariables.x;

            cameraTransform.position = updatedCameraPos + cameraOffset;
            cameraTransform.LookAt(updatedCameraPos);
        }
    }

    public void SetMovement(bool value)
    {
        canMovement = value;
    }
    public void AD(bool value)
    {
        ADmove = value;
    }
    public void WS(bool value)
    {
        WSmove = value;
    }
}