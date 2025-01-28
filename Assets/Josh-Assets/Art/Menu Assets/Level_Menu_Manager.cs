using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Menu_Manager : MonoBehaviour
{
    // References to each planet
    public string brown_Planet_Scene;
    public string blue_Planet_Scene;
    public string green_Planet_Scene;
    public string pink_Planet_Scene;
    public string moon_Scene;

    // Varibles deciding if a planet is unlocked
    public bool brown_Planet_Unlocked;
    public bool blue_Planet_Unlocked;
    public bool green_Planet_Unlocked;
    public bool pink_Planet_Unlocked;
    public bool moon_Planet_Unlocked;

    // Functions locking and unlocking planets


    // Load the selected planet if it is unlocked

    public void travel(string planet)
    {

    }
    public void Brown_Planet()
    {

        if (brown_Planet_Scene != null)
        {
            SceneManager.LoadScene(brown_Planet_Scene);
        }
    }
    public void Blue_Planet()
    {
        if (blue_Planet_Scene != null)
        {
            SceneManager.LoadScene(blue_Planet_Scene);
        }
    }
    public void Green_Planet()
    {
        if (green_Planet_Scene != null)
        {
            SceneManager.LoadScene(green_Planet_Scene);
        }
    }

    public void Pink_Planet()
    {
        if (pink_Planet_Scene != null)
        {
            SceneManager.LoadScene(pink_Planet_Scene);
        }
    }

    public void Moon()
    {
        if (moon_Scene != null)
        {
            SceneManager.LoadScene(moon_Scene);
        }
    }
}
