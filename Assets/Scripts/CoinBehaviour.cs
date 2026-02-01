using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBehaviour : MonoBehaviour
{
    public static int numCoins = 0; //unique variable per CoinBehaviour 
    public GameObject player;
    public GameObject parent;
    public Text coinText;
    public GameObject[] coins;

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
        // if the player collect the coin
        if (other.gameObject == player.gameObject)
        {
            numCoins++;
            coinText.text = "Coins: " + numCoins;
            gameObject.SetActive(false);
            AudioSource sound = parent.GetComponent<AudioSource>();
            sound.Play();

        }

        // for (int i = 0; i < coins.Length; i++)
        // {
        //     if (coins[i].gameObject == gameObject)
        //     {
        //         PersistentObjectManager.coinsBool[i] = false;
        //     }
        //    
        // }

    }
}
