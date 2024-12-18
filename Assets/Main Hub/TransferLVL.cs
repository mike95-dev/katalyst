using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferLVL : MonoBehaviour
{
    bool iscurrentlyloading;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (iscurrentlyloading == true)
        {
            //LoadSceneMode.Single = OutdoorsScene;
        }
    }
}
