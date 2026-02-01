using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//implement Singleton pattern
public class PersistentObjectManager : MonoBehaviour
{

    /////////// coins //////////
    public static PersistentObjectManager instance = null;
    public static int numGoldCoins = 0;
    public Text numCoinsText;
    // public static bool [] coinsBool;
    // public GameObject [] coins;
 
    /////////// sword /////////////
    public static bool hasBowInHand = false;
    public static bool hasBowInwall = true;
    public GameObject BowAndArrow;
    public GameObject BowAndArrowInHand;
    
    ///////////// Chest //////////////////////////
    public static bool IschestOpen = false;
    
    ///////////// key //////////////////////////
    public static bool hasKeyInHand = false;
    public static bool hasKeyInwall = true;
    public GameObject key_in_hand;
    public GameObject key_on_wall;
    
    ///////////// Crown //////////////////////////
    public static bool hasCrownOnHead = false;
    public static bool hasCrownInChest = true;
    public GameObject crown_in_chest;
    public GameObject crown_on_head;
    

    
    
    private void Awake()
    {
        if (instance == null) //this is for the first time
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        numCoinsText.text = "Coins: " + numGoldCoins;
        BowAndArrowInHand.SetActive(hasBowInHand);
        BowAndArrow.SetActive(hasBowInwall);
        
        key_in_hand.SetActive(hasKeyInHand);
        key_on_wall.SetActive(hasKeyInwall);
        
        crown_in_chest.SetActive(hasCrownInChest);
        crown_on_head.SetActive(hasCrownOnHead);

       
        //keep the original istance of PersistentObjectManager
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static void SetGoldCoins(int coins)
    {
        numGoldCoins = coins;
    }

    public static void SetHasBowInHand(bool hasBow)
    {
        hasBowInHand = hasBow;
    }
    public static void SetHasBowOnWall(bool hasBow)
    {
        hasBowInwall = hasBow;
    }
    
    public static void SetHasKeyInHand(bool hasKey)
    {
        hasKeyInHand = hasKey;
    }
    public static void SetHasKeyOnWall(bool hasKey)
    {
        hasKeyInwall = hasKey;
    }

    public static void SetIsChestOpen(bool isChestOpen)
    {
        IschestOpen = isChestOpen;
    }
    
    public static void SetHasCrownOnHead(bool hasCrown)
    {
        hasCrownOnHead = hasCrown;
    }
    public static void SetHasCrownInChest(bool hasCrown)
    {
        hasCrownInChest = hasCrown;
    }
    
}
