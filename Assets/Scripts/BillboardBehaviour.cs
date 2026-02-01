using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardBehaviour : MonoBehaviour
{
    public GameObject playerCamera; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position - playerCamera.transform.forward );
    }
}
