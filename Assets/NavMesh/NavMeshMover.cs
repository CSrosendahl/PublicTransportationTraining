using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMover : MonoBehaviour
{
    public List<Transform> waypoints;
    public float waitTime = 5f;  // Time to wait at each waypoint.

    private NavMeshAgent agent;
    private int currentWaypointIndex;
    private bool isMoving = true;
    public float navMeshSpeed;

    private Animator animator;




    private void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = navMeshSpeed;
        if (waypoints.Count > 0)
        {
            MoveToPoint(waypoints[0].position);
        }
    }

    private void Update()
    {
        if (isMoving && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            
            isMoving = false;  // Stop checking in this cycle
            animator.SetBool("isWalking", false);
            StartCoroutine(WaitAndMove());
            // We need to play idle animation
        }

        
    }


    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(waitTime);
       
        // Proceed to the next waypoint.
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count; // Loop back to the first waypoint if we're at the last one.
        MoveToPoint(waypoints[currentWaypointIndex].position);
    }

    public void MoveToPoint(Vector3 point)
    {
       
        agent.SetDestination(point);
        isMoving = true;
        animator.SetBool("isWalking", true);
        

        // We need to play walk animation
    }

}
