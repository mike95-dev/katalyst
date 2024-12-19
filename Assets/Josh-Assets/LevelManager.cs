// This script is meant to handle all scene management, such as activating the pause menu and switching between scenes.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    #region Variables
    // TODO: Add a menu manager to reference in this script once we have pause states and menus
    public static LevelManager instance; // the game instance
    private int currentSceneInt;
    private int previousSceneInt;

    // Remove once we have a proper menu management system...
    public MainMenuUI mainMenu;
    #endregion Variables

    private void Awake()
    {
        // Create a game object that contains the Scene manager
        // If it doesn't already exist...
        if (instance == null)
        {
            instance = this; // Set this gameObject as the instance
            DontDestroyOnLoad(gameObject); // Don't destroy the first Scene Manager in the scene

            // Activate the main menu (set as the first scene)
            SetNextScene(1); // 1 is a test number for now
            ActivateNextScene();
        }
        else
        {
            Destroy(gameObject); // destroy any extra scene managers
        }
    }

    private void Start()
    {

    }

    // Set the next scene to be loaded
    // I kept this function separate so that the player can change the next scene to be loaded before loading the scene
    public void SetNextScene(int scene) // this can easily be changed to a string value
    {
        currentSceneInt = scene;
        Debug.Log("Next scene to be loaded: " + currentSceneInt);
    }

    // Load the next scene depending on which scene number it is assigned to
    public void ActivateNextScene()
    {
        SceneManager.LoadSceneAsync(currentSceneInt);
        Debug.Log("Scene " + currentSceneInt + " loaded.");
    }
}
