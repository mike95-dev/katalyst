using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    private void Start()
    {
        LevelManager.instance.mainMenu = this;
    }

    // Change the next scene 
    public void ChangeToGameplay()
    {
        LevelManager.instance.SetNextScene(0);
        LevelManager.instance.ActivateNextScene();
    }
}
