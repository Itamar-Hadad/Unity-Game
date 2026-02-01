using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{

    /////////////////// Player ////////////////////
    public GameObject player_camera; // must be conected to an object in Unity
    CharacterController controller;
    AudioSource footSteps;


    float speed = 10;
    float angular_speed = 1200;

    /////////////////// Bow and Arrow ////////////////////
    public GameObject BowAndArrow;
    public GameObject BowAndArrowInHand;
    public Text pickText;

    /////////////////// Chest //////////////////////////
    public GameObject chest_object;
    public bool hasKeyForChest = false;
    public Animator animatorForChest;
    public Text pickTextForChest;

    /////////////////// Key ////////////////////
    public GameObject key_on_wall;
    public GameObject key_in_hand;
    public Text pickTextFromKey;

    /////////////////// Crown ////////////////////
    public GameObject crown_in_chest;
    public GameObject crown_on_head;
    public Text pickCrownText;

    

    
    ////////////////////////////////////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        //connect to components in unity.
        controller = this.GetComponent<CharacterController>();
        footSteps = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // primitive motion
        // this.transform.Translate(new Vector3(0, 0, 0.1f));
        float dx, dz;
        float roration_about_Y, roration_about_X;

        // camera rotation about X axe
        roration_about_X = Input.GetAxis("Mouse Y") * angular_speed * Time.deltaTime;
        player_camera.transform.Rotate(-roration_about_X, 0, 0);


        // player rotation about Y axe
        roration_about_Y = Input.GetAxis("Mouse X") * angular_speed * Time.deltaTime;
        transform.Rotate(0, roration_about_Y, 0); //

        dx = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        dz = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        Vector3 motion = new Vector3(dx, -0.3f, dz);
        motion = transform.TransformDirection(motion); // in local coordinates
        controller.Move(motion); // in global coordinates
        if (!(Mathf.Abs(dx) < 0.01f && Mathf.Abs(dz) < 0.01f))
        {
            if (!footSteps.isPlaying)
            {
                footSteps.Play();
            }
        }

        ///////////////////// Bow /////////////////////////////
        float distanceBow = Vector3.Distance(transform.position, BowAndArrow.transform.position);
        pickText.gameObject.SetActive(false);
        if (distanceBow < 10)
        {
            RaycastHit hit;
            if (Physics.Raycast(player_camera.transform.position,
                    player_camera.transform.forward, out hit))
            {
                if (hit.collider.gameObject == BowAndArrow.gameObject)
                {
                    pickText.gameObject.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.P))
                    {
                        BowAndArrow.SetActive(false);
                        BowAndArrowInHand.SetActive(true);
                        PersistentObjectManager.SetHasBowInHand(true); //in hand
                        PersistentObjectManager.SetHasBowOnWall(false);
                    }
                }
                else
                {
                    pickText.gameObject.SetActive(false);
                }

            }
        }
        else //if the player is far enough from Bow
        {
            pickText.gameObject.SetActive(false);
        }


        ////////////////////////////////// Chest ////////////////////////////////
        float distanceFromChest = Vector3.Distance(transform.position, chest_object.transform.position);
        pickTextForChest.gameObject.SetActive(false);
        if (distanceFromChest < 10 && PersistentObjectManager.IschestOpen != true)
        {
            RaycastHit hit;
            if (Physics.Raycast(player_camera.transform.position,
                    player_camera.transform.forward, out hit))
            {
                //for chest
                if (hit.collider.gameObject == chest_object.gameObject)
                {
                    string message = " ";
                    pickTextForChest.gameObject.SetActive(true);
                    if (hasKeyForChest && animatorForChest.GetBool("PressedSpace") != true)
                    {
                        message = "Press ''Space'' To Open Chest";
                        pickTextForChest.text = message;

                    }
                    else
                    {
                        if (animatorForChest.GetBool("KeyInHand") == false)
                        {
                            message = "You need a key in hand to open chest";
                            pickTextForChest.text = message;
                        }
                        else
                        {
                            message = " ";
                            pickTextForChest.text = message;
                        }

                    }

                    // pressing space to active opening animation
                    if (Input.GetKeyDown(KeyCode.Space) && hasKeyForChest)
                    {
                        animatorForChest.SetBool("PressedSpace", true);
                        PersistentObjectManager.SetIsChestOpen(true);
                    }
                }
            }
        }
        else
        {
            pickTextForChest.gameObject.SetActive(false);
        }

        if (PersistentObjectManager.IschestOpen)
        {
            animatorForChest.SetBool("PressedSpace", true);
            animatorForChest.SetBool("KeyInHand", true);
        }


        //////////////////////////// Key /////////////////////////////////////
        float distanceFromKey = Vector3.Distance(transform.position, key_on_wall.transform.position);
        pickTextFromKey.gameObject.SetActive(false);
        if (distanceFromKey < 10 && PersistentObjectManager.IschestOpen != true)
        {
            RaycastHit hit;
            if (Physics.Raycast(player_camera.transform.position,
                    player_camera.transform.forward, out hit))
            {

                if (hit.collider.gameObject == key_on_wall.gameObject)
                {
                    pickTextFromKey.gameObject.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.P))
                    {
                        hasKeyForChest = true;
                        key_on_wall.SetActive(false);
                        key_in_hand.SetActive(true);
                        animatorForChest.SetBool("KeyInHand", true);
                        PersistentObjectManager.SetHasKeyInHand(true); //in hand
                        PersistentObjectManager.SetHasKeyOnWall(false);
                    }
                }
                else
                {
                    pickTextFromKey.gameObject.SetActive(false);
                }

            }
        }


        ///////////////////// picking Crown /////////////////////////////
        float distanceFromCrown = Vector3.Distance(transform.position, crown_in_chest.transform.position);
        pickCrownText.gameObject.SetActive(false);
        if (distanceFromCrown < 10)
        {
            RaycastHit hit;
            if (Physics.Raycast(player_camera.transform.position,
                    player_camera.transform.forward, out hit))
            {
                if (hit.collider.gameObject == crown_in_chest.gameObject)
                {
                    pickCrownText.gameObject.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        crown_in_chest.SetActive(false);
                        crown_on_head.SetActive(true);
                        PersistentObjectManager.SetHasCrownOnHead(true);
                        PersistentObjectManager.SetHasCrownInChest(false);
                    }
                }
                else
                {
                    pickCrownText.gameObject.SetActive(false);
                }

            }
        }
        else //if the player is far enough from sword
        {
            pickCrownText.gameObject.SetActive(false);
        }

    }
}

    
    
    
    
    