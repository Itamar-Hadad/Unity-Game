using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PleasantBehavior : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    public GameObject target;

    public GameObject Point1;
    public GameObject Point2;
    public GameObject Point3;

    private int currentPointIndex = 0;  // Keeps track of which point to go to
    private GameObject[] points;  // Array of points to stop at

    public float baseWaitTime = 50f;  // The minimum wait time at each point
    public float extraWaitTime = 15f;  // Extra random wait time

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Initialize the points array with Point1, Point2, and Point3
        points = new GameObject[] { Point1, Point2, Point3 };

        StartWalking();  // Start walking to the first point
    }

    // Update is called once per frame
    void Update()
    {
        // Check the distance to the current target point
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (!agent.isStopped && distance < 2)  // If close to the target
        {
            StopWalking();  // Stop walking and idle
            StartCoroutine(WaitAndMove());  // Wait before moving to the next point
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Manually start walking if E is pressed
            StartWalking();
        }
    }

    // Function to start walking to the next point
    void StartWalking()
    {
        // Get the current point from the array
        target.transform.position = points[currentPointIndex].transform.position;

        agent.SetDestination(target.transform.position);  // Set the new destination
        agent.isStopped = false;
        animator.SetInteger("State", 1);  // Walking animation
    }

    // Function to stop walking
    void StopWalking()
    {
        agent.isStopped = true;
        animator.SetInteger("State", 0);  // Idle animation
    }

    // Coroutine to wait for a few seconds, then move to the next point
    IEnumerator WaitAndMove()
    {
        // Wait for a random time between baseWaitTime and baseWaitTime + extraWaitTime
        float waitTime = baseWaitTime +  extraWaitTime;
        yield return new WaitForSeconds(waitTime);  // Wait for the random time

        // Move to the next point in the array
        currentPointIndex = (currentPointIndex + 1) % points.Length;  // Loop back to the start after reaching the last point

        // Start moving to the next point
        StartWalking();
    }
}
