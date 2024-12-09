using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public GameObject startScreenStateObject;
    public GameObject endScreenStateObject;
    public GameObject gameplayStateObject;

    // Start is called before the first frame update
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        Debug.Log("Starting menu manager!");
        DeactivateAllStates();
        ActivateGameplayState();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateAllStates()
    {
        // update this function with any other screen objects we add
        startScreenStateObject.SetActive(false);
        endScreenStateObject.SetActive(false);
        gameplayStateObject.SetActive(false);
    }

    public void ActivateStartScreenState()
    {
        startScreenStateObject.SetActive(true);
    }

    public void ActivateEndScreenState()
    {
        endScreenStateObject.SetActive(true);
    }
    public void ActivateGameplayState()
    {
        gameplayStateObject.SetActive(true);
    }

}
