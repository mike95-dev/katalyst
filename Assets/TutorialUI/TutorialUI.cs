using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    #region Public Variables
    [Header("Controls Screen Variables. \nHover to view information.")]
    [Tooltip("Key to minimize the tutorial controls screen.")]
    public KeyCode enterKey; // 
    [Tooltip("Key to reopen the tutorial controls screen.")]
    public KeyCode openTutorialKey; // 
    [Tooltip("Changes the inactive color of the UI elements.")]
    public Color inactiveColor; // 
    [Tooltip("Changes the size of the UI when inactive.")]
    public float scaleChangeInUI; // 
    [Tooltip("Checkbox to determine if this player is connected to the game.")]
    public bool isPlayerConnected; // 
    [Tooltip("Sets the Katalyst Icon to the active control.")]
    public int currentControl;
    #endregion Public Variables

    #region Sprites
    [Header("Sprite information. \nPlease drag the UI sprite needed into each parameter below.")]
    [HideInInspector] public Sprite currentKatalystIcon; // Changes the current Katalyst icon for UI
    [HideInInspector] public Sprite currentControlIcon; // Changes the current controls for UI
    public Sprite horizontalKatalyst;
    public Sprite verticalKatalyst;
    public Sprite shootingKatalyst;
    public Sprite horizontalControls;
    public Sprite verticalControls;
    public Sprite shootingControls;
    #endregion Sprites

    #region Private Variables
    private Animator animator;
    private bool isUIActive = true;
    #endregion Private Variables


    void Start()
    {
        if (isPlayerConnected)
        {
            SetPlayerActive(true);
        }
        else if (isPlayerConnected == false)
        {
            SetPlayerActive(false);
        }

        ChangeIcons();
        // Grab the animator component
        animator = transform.GetComponent<Animator>();
        // Set the default animation to idle
        animator.Play("TutorialControlsOpening");
    }

    void Update()
    {
        if (isUIActive)
        {
            if (Input.GetKey(enterKey))
            {
                OnConfirmButtonPressed();
            }
        }
        else if (!isUIActive)
        {
            if (Input.GetKeyDown(openTutorialKey))
            {
                animator.Play("TutorialControlsOpening");
                isUIActive = true;
            }
        }
    }

    public void OnConfirmButtonPressed()
    {
        animator.Play("TutorialControlsClosing");
        isUIActive = false;
    }

    // Pass "true" if PlayerX is connected to the game
    void SetPlayerActive(bool isActive)
    {
        if (!isActive)
        {
            ChangeUIColor();
            ChangeUISize(scaleChangeInUI);
        }
    }

    // Changes the opacity of the UI based on if the player is active for the panel.
    void ChangeUIColor()
    {
        Image image;
        Image katImage;
        Image controllerImage;

        image = transform.GetComponent<Image>();
        katImage = transform.GetChild(0).GetComponent<Image>();
        controllerImage = transform.GetChild(1).GetComponent<Image>();
        image.color = inactiveColor;
        katImage.color = inactiveColor;
        controllerImage.color = inactiveColor;
    }

    void ChangeUISize(float scale)
    {
        // Change THIS object's transform
        transform.parent.transform.localScale = new Vector3(scale,scale,scale);
    }

    // Changes the icons around for each player
    void ChangeIcons()
    {
        // If able, please cast this function to or from the control switching script to change controls every switch
        // 0 = vertical controls
        if (currentControl == 0)
        {
            currentControlIcon = verticalControls;
            currentKatalystIcon = verticalKatalyst;

        }
        // 1 = horizontal controls
        else if (currentControl == 1)
        {
            currentControlIcon = horizontalControls;
            currentKatalystIcon = horizontalKatalyst;
        }
        // 2 = shooting controls
        else if (currentControl == 2)
        {
            currentControlIcon = shootingControls;
            currentKatalystIcon = shootingKatalyst;
        }

        Image katImage;
        Image controllerImage;

        katImage = transform.GetChild(0).GetComponent<Image>();
        controllerImage = transform.GetChild(1).GetComponent<Image>();
        katImage.sprite = currentKatalystIcon;
        controllerImage.sprite = currentControlIcon;
    }
}
