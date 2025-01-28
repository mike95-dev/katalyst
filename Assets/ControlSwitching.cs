using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class ControlSwitcher : MonoBehaviour
{
    private int controlState = 0; // 0 = W/S keys, 1 = A/D keys, 2 = Aim only
    private CubeController cubeController;
    private PlayerShootingTowardsCrosshair shootingScript;
    private CrosshairFollowMouseWithRadius crosshairScript;
    private string controlMessage = ""; // Message to display on the screen
    private float timeUntilSwitch = 10f; // Time in seconds for each switch interval
    private float countdownTimer; // Timer to show time remaining for the next switch

    // Vignette flashing effect variables
    public Texture2D vignetteTexture; // Assign the red vignette texture in the Inspector
    private bool isFlashing = false;
    private float flashSpeed = 4f; // Speed of the flashing effect

    // UI variables
    public Text control_Version;
    public Text countdown;
    public GameObject vignette;

    void Start()
    {
        GameObject player = GameObject.Find("Player");

        if (player == null)
        {
            Debug.LogError("Player object not found!");
            enabled = false; // Disable this script if player is not found
            return;
        }
        cubeController = player.GetComponent<CubeController>();

        if (cubeController == null)
        {
            Debug.LogError("CubeController component not found on the 'Player' object!");
            enabled = false; // Disable this script if CubeController is not found
            return;
        }
        shootingScript = player.GetComponent<PlayerShootingTowardsCrosshair>();

        if (shootingScript == null)
        {
            Debug.LogError("shootingScript component not found on the 'Player' object!");
            enabled = false; // Disable this script if shootingScript is not found
            return;
        }
        GameObject crosshair = GameObject.Find("CrossHair");

        if (crosshair == null)
        {
            Debug.LogError("crosshair object not found!");
            enabled = false; // Disable this script if crosshair is not found
            return;
        }
        crosshairScript = crosshair.GetComponent<CrosshairFollowMouseWithRadius>();

        if (crosshairScript == null)
        {
            Debug.LogError("crosshairScript component not found on the 'Player' object!");
            enabled = false; // Disable this script if crosshairScript is not found
            return;
        }

        countdownTimer = timeUntilSwitch; // Initialize countdown timer
        StartCoroutine(SwitchControls());
    }
    private void Update()
    {
        GUI();
    }

    IEnumerator SwitchControls()
    {
        while (true)
        {
            switch (controlState)
            {
                case 0:
                    EnableWSControls();
                    break;
                case 1:
                    EnableADControls();
                    break;
                case 2:
                    EnableAimOnly();
                    break;
            }

            // Reset countdown timer for the next switch
            countdownTimer = timeUntilSwitch;
            isFlashing = false;

            // Countdown loop
            while (countdownTimer > 0)
            {
                countdownTimer -= Time.deltaTime;

                // Start flashing effect in the last 2 seconds
                if (countdownTimer <= 3f)
                {
                    isFlashing = true;
                }

                yield return null;
            }

            controlState = (controlState + 1) % 3; // Cycle through control states
        }
    }

    void EnableWSControls()
    {
        Debug.Log("WS");
        controlMessage = "Vertical Control Mode (W/S)";
        cubeController.WS(true);
        cubeController.AD(false);
        shootingScript.Shoot(false);
        crosshairScript.Aim(false);
    }

    void EnableADControls()
    {
        Debug.Log("AD");
        controlMessage = "Horizontal Control Mode (A/D)";
        cubeController.WS(false);
        cubeController.AD(true);
        shootingScript.Shoot(false);
        crosshairScript.Aim(false);
    }

    void EnableAimOnly()
    {
        Debug.Log("AIM");
        controlMessage = "Shooting Mode";
        cubeController.WS(false);
        cubeController.AD(false);
        shootingScript.Shoot(true);
        crosshairScript.Aim(true);
    }

    void GUI()
    {
        control_Version.text = controlMessage;

        countdown.text = "Next Switch In: " + Mathf.Ceil(countdownTimer) + "s";

        if (isFlashing && vignette != null) 
        { 
            vignette.SetActive(false);
        }
    

        /*GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.white;

        // Display the control message at the top-center of the screen
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 350, 200, 40), controlMessage, style);

        // Display the countdown timer below the control message
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 300, 200, 40), "Next Switch In: " + Mathf.Ceil(countdownTimer) + "s", style);

        // Flashing vignette effect
        if (isFlashing && vignetteTexture != null)
        {
            Color vignetteColor = GUI.color;
            vignetteColor.a = Mathf.Abs(Mathf.Sin(Time.time * flashSpeed)); // Creates a flashing effect by oscillating alpha
            GUI.color = vignetteColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), vignetteTexture);
            GUI.color = Color.white; // Reset GUI color to white
        } */
    }
}