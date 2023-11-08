using UnityEngine;
using System.Collections;

public class TrainMover : MonoBehaviour
{
    public Transform boardingDestination;
    public Transform exitDestination;
    private Transform currentDestination;
   

    public float maxSpeed = 1.0f; // The maximum speed of the train
    public float acceleration = 0.5f; // How quickly the train accelerates
    public float quickDecelerationRate = 2.0f; // The rate at which the train decelerates quickly within the deceleration distance
    public float decelerationDistance = 5.0f; // The distance from the destination at which the train starts to decelerate
    public float stopDistance = 0.1f; // Distance at which the train considers it has "arrived"
    public float waitTime = 10.0f; // Time to wait at destination before moving again


    public float currentSpeed = 0f; // Current speed of the train
    private bool isMoving = false;

    void Start()
    {
        
        // Set the first destination as the current one and start moving
        currentDestination = boardingDestination;
        StartCoroutine(StartMoving());
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTrain();
        }
    }

    IEnumerator StartMoving()
    {
        // Wait before starting to move
        yield return new WaitForSeconds(waitTime);
        isMoving = true;
    }

    void MoveTrain()
    {
        // Calculate the distance to the current destination
        float distanceToDestination = Vector3.Distance(transform.position, currentDestination.position);

        // Check if the train is within the deceleration distance and above 1f speed
        if (distanceToDestination < decelerationDistance && currentSpeed > 1f)
        {
            // Decelerate quickly to the speed of 1f
            currentSpeed = Mathf.Max(currentSpeed - quickDecelerationRate * Time.deltaTime, 1f);
        }
        else if (currentSpeed < maxSpeed && distanceToDestination >= decelerationDistance)
        {
            // Accelerate the train until it reaches max speed
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
        }

        // Move the train towards the current destination
        transform.position = Vector3.MoveTowards(transform.position, currentDestination.position, currentSpeed * Time.deltaTime);

        // If the train is close enough to the current destination, stop and start waiting
        if (distanceToDestination <= stopDistance && currentSpeed <= 1f)
        {
            isMoving = false; // Stop the train
            StartCoroutine(WaitAtDestination());
        }
    }

    IEnumerator WaitAtDestination()
    {
        yield return new WaitForSeconds(waitTime);
        // Set the new destination and reset the speed for acceleration

        if(currentDestination == exitDestination)
        {
            TrainSpawner.instance.hasSpawned = false; // Allow the train spawner to spawn a new train
            Destroy(gameObject);
        } else
        {
            currentDestination = (currentDestination == boardingDestination) ? exitDestination : boardingDestination;
            currentSpeed = 0f; // Reset speed to 0 to start acceleration from a full stop
            isMoving = true; // Allow the train to start moving again
        }

       
    }
}
