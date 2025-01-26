using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStarMap : MonoBehaviour
{
    public GameObject starMap;
    public GameObject mainUI;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Freeze();
            mainUI.SetActive(false);
            starMap.SetActive(true);
        }
        
    }

    private void Freeze()
    {
        Time.timeScale = 0;
    }

    private void Unfreeze()
    {
        Time.timeScale = 1;
    }
}
