using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //update numGoldCoins in PersistantObjectManager
            PersistentObjectManager.SetGoldCoins(CoinBehaviour.numCoins);
            SceneManager.LoadScene(1);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            PersistentObjectManager.SetGoldCoins(CoinBehaviour.numCoins);
            SceneManager.LoadScene(0);
        }
    }
}
