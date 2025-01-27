using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairFollowMouseWithRadius : MonoBehaviour
{
    public Texture2D crosshairTexture; // Assign a crosshair texture in the inspector
    private Vector2 crosshairSize = new Vector2(32, 32); // Size of the crosshair
    private float radius = 150f; // Radius of 150 pixels
    private Vector2 crosshairPos; // Store the crosshair position
    public float stickSensitivity = 3f; // Sensitivity for the right stick control
    public bool canAim = true;      // Variable to control movement
    public bool collision = false;      // Variable to control movement

    public PlayerControls playerControls;
    public float horizontalVector; // Ensure that "HorizontalAim" is set up in PlayerControls InputAction
    public float verticalVector; // Ensure that "VerticalAim" is set up in PlayerControls InputAction

    void Start()
    {
        // Get player contorls
        playerControls = new PlayerControls();

        // Hide the default system cursor
        Cursor.visible = false;

        // Set the initial crosshair position to the center of the screen
        crosshairPos = new Vector2(Screen.width / 2, Screen.height / 2);
    }

    void Update()
    {
        if (canAim && !collision)
        {
            UpdateCrosshairPositionWithStick();
        }
    }

    void UpdateCrosshairPositionWithStick()
    {
        // Get the right stick input for controlling the crosshair
        float rightStickX = horizontalVector; // Ensure "RightStickHorizontal" is set up in Input Manager
        float rightStickY = verticalVector;   // Ensure "RightStickVertical" is set up in Input Manager

        // Move the crosshair based on right stick input and sensitivity
        crosshairPos += new Vector2(rightStickX, rightStickY) * stickSensitivity;

        // Get the center of the screen
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

        // Calculate the offset from the center
        Vector2 offset = crosshairPos - screenCenter;

        // Clamp the crosshair position within the defined radius
        if (offset.magnitude > radius)
        {
            offset = offset.normalized * radius;
            crosshairPos = screenCenter + offset;
        }
    }

    void OnGUI()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);


        // Define a rect where the crosshair will be drawn, centered on the clamped position
        Rect crosshairRect = new Rect(crosshairPos.x - crosshairSize.x / 2,
                                      crosshairPos.y - crosshairSize.y / 2,
                                      crosshairSize.x,
                                      crosshairSize.y);

        // Draw the crosshair texture at the clamped position
        GUI.DrawTexture(crosshairRect, crosshairTexture);
    }

    public Vector2 GetCrosshairPosition()
    {
        // Return the crosshair's screen position
        return crosshairPos;
    }

    public void Collision(bool value)
    {
        collision = value;
    }

    public void Aim(bool value)
    {
        canAim = value;
    }
    
    // InputAction HorizontalAim
    public void OnHorizontalAim(InputAction.CallbackContext ctx)
    {
        float moveValue = ctx.ReadValue<float>();
        horizontalVector = moveValue;
    }

    // InputAction VerticalAim
    public void OnVerticalAim(InputAction.CallbackContext ctx)
    {
        float moveValue = ctx.ReadValue<float>();
        verticalVector = moveValue;
    }
}