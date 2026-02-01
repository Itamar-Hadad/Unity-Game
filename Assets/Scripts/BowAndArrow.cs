using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BowAndArrow : MonoBehaviour
{
    LineRenderer bow_wire;
    AudioSource sound;
    public GameObject PointA; 
    public GameObject PointB; 
    public GameObject PointC;
    public GameObject Arrow;
    public GameObject ArrowInTarget;
    public GameObject eye;
    public GameObject Target;
    public KnightBehavior enemy;
    bool buttonIsPressed = false;
    int framesCounter, max_counter=40;

    float delta = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        bow_wire = GetComponent<LineRenderer>();
        sound = GetComponent<AudioSource>();
        bow_wire.SetWidth(0.01f, 0.01f);
        PointB.transform.position = new Vector3((PointA.transform.position.x+ PointC.transform.position.x)/2,
            (PointA.transform.position.y + PointC.transform.position.y) / 2, 
            (PointA.transform.position.z + PointC.transform.position.z) / 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) // true if there was click on LEFT (0) mouse button
        {
            if (Arrow.activeSelf)
            {
                buttonIsPressed = true;
                framesCounter = 0;
            }
        }
        else if(Input.GetMouseButtonUp(0) && Arrow.activeSelf) // true if left mouse button was released
        {
            buttonIsPressed = false;
            PointB.transform.position = new Vector3((PointA.transform.position.x + PointC.transform.position.x) / 2,
                (PointA.transform.position.y + PointC.transform.position.y) / 2,
                (PointA.transform.position.z + PointC.transform.position.z) / 2);
            Arrow.SetActive(false);
            sound.Play();
            RaycastHit hit;
            if(Physics.Raycast(eye.transform.position,eye.transform.forward, out hit))
            {
                ArrowInTarget.SetActive(true);
                ArrowInTarget.transform.position = hit.point;
                Target.transform.position = hit.point;

                if (hit.collider.gameObject == enemy.gameObject)
                {
                    enemy.DoDamage();
                }
                Vector3 offset = new Vector3(0, 0, 2); // in local coordinates
                offset = transform.TransformDirection(offset); // to global coordinates

                ArrowInTarget.transform.Translate(offset); // move backward
                ArrowInTarget.transform.rotation = Arrow.transform.rotation;
            }
        }
        if (Input.GetMouseButtonDown(1)) // true if there was click on RIGHT (1) mouse button
        {
            Arrow.SetActive(true);

        }

        if (buttonIsPressed && framesCounter<max_counter)
        {
            framesCounter++;
            PointB.transform.Translate(0, delta,0);
        }
    }

    // runs after all updates have finished
    private void LateUpdate()
    {
        bow_wire.SetPosition(0, PointA.transform.position);
        bow_wire.SetPosition(1, PointB.transform.position);
        bow_wire.SetPosition(2, PointC.transform.position);
        
    }
}
