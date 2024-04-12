using System.Collections;
using UnityEngine;

public class TrainTripMover : MonoBehaviour
{
    public Transform[] boardingDestinations; // The destination of the train when it is boarding
    private Transform currentDestination; // The current destination of the train

    public float maxSpeed = 10.0f; // The maximum speed of the train
    public float acceleration = 5f; // How quickly the train accelerates
    public float quickDecelerationRate = 6f; // The rate at which the train decelerates quickly within the deceleration distance
    public float decelerationDistance = 15f; // The distance from the destination at which the train starts to decelerate
    public float stopDistance = 0.1f; // Distance at which the train considers it has "arrived"
    public float waitTime = 10.0f; // Time to wait at destination before moving again

    public float currentSpeed = 0f; // Current speed of the train
    public bool isMoving = false; // Is the train moving?

    private bool playSoundOnce;

    void Start()
    {
        // Set the first destination as the current one and start moving
        currentDestination = boardingDestinations[0];
        StartCoroutine(WaitToStart(10));
        playSoundOnce = false;

    }

    void Update()
    {
        if (isMoving)
        {
            MoveTrain();
        }
    }


    void MoveTrain()
    {
        if(!playSoundOnce)
        {
            AudioManager.instance.PlayAudioClip(AudioManager.instance.trainMoveSound);
            playSoundOnce = true;
        }
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


        
        OpenOutSideDoor.instance.OpenCloseDoors(); // Open the doors
    
      //  StartCoroutine(CloseDoorWaitTime());
        yield return new WaitForSeconds(waitTime);

        // Find the index of the current destination in the boardingDestinations array
        int currentIndex = System.Array.IndexOf(boardingDestinations, currentDestination);

        // Check if the current destination is the last one in boardingDestinations
        if (currentIndex == boardingDestinations.Length - 1)
        {
            // Set the exit destination
           
           

            isMoving = false; // Stop the train from moving
        }
        else
        {
           
            // Move to the next boarding destination
            currentIndex = (currentIndex + 1) % boardingDestinations.Length;
            currentDestination = boardingDestinations[currentIndex];
            currentSpeed = 0f; // Reset speed to 0 to start acceleration from a full stop
            isMoving = true; // Allow the train to start moving again
            playSoundOnce = false;

            // Play the door opening sound

            //OpenOutSideDoor.instance.SetOpenDoors(false); // Close the doors
        }
    }

    IEnumerator WaitToStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isMoving = true;
    }

}
