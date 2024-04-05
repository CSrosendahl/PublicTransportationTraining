using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMover : MonoBehaviour
{
    public static EnvironmentMover instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
      
    }
    public Transform destination;
    public GameObject valbyStationGO;

    public bool moveForward; // Controls whether the train moves to the forward or backward destination

    [Header("Movement:")]
    public float maxSpeed = 1.0f;
    public float acceleration = 0.5f;
    public float decelerationSpeed = 2.0f;
    public float decelerationDistance = 5.0f;
    public float decelerationDuration = 5.0f;
    public float accelerationDuration = 5.0f;
    public float waitTime = 10.0f;

    private float currentSpeed = 0f;
    //Stupid Flags!!
    private bool isMoving = false;
    private bool isAtStation = false;

    [Header("Spawning:")]
    public float SpawnTimer = 10f;
    private float spawnTimerCountdown = 0f; // Timer countdown for spawning
    private float nextDestroyTime = 0f;
    public float destroyInterval = 30f; // Interval in seconds between each destruction

    private int currentChildIndexToActivate = 2; // Index of the child to activate next

    //private Coroutine decelerationCoroutine;


    void Start()
    {
        nextDestroyTime = Time.time + destroyInterval;
        spawnTimerCountdown = SpawnTimer;

        // Set the initial destination and start moving
        SetDestination(destination);
        isMoving = true;
        StartCoroutine(DisableValbyStationGameObject(45));
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTrain();
        }

        // Update spawn timer countdown
        spawnTimerCountdown -= Time.deltaTime;
        if (spawnTimerCountdown <= 0)
        {
            ActivateNextChild();
            spawnTimerCountdown = SpawnTimer;
        }

        if (Time.deltaTime >= nextDestroyTime)
        {
            DestroyFirstChild();
            nextDestroyTime = Time.deltaTime + destroyInterval;
        }
    }

    void MoveTrain()
    {
        Transform destination = null;

        if (isAtStation)
        {
            destination = this.destination; // If at the station, move towards the destination
        }
        else if (isMoving)
        {
            destination = this.destination; // If moving forward, move towards the destination
        }
        else
        {
            destination = null; // If not at the station and not moving, no destination
        }

        if (destination != null)
        {
            float distanceToDestination = Vector3.Distance(transform.position, destination.position);

            Vector3 movementDirection = moveForward ?
                (destination.position - transform.position).normalized :
                (transform.position - destination.position).normalized;

            if (currentSpeed < maxSpeed && distanceToDestination >= decelerationDistance)
            {
                currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
            }

            transform.position += movementDirection * currentSpeed * Time.deltaTime;
        }
    }


    public void SetDestination(Transform destination)
    {
        // Set the destination
        if (destination != null)
        {
            Vector3 directionToDestination = destination.position - transform.position;
            directionToDestination.y = 0f; // Ensure no rotation in the y-axis to prevent tilting

            // Calculate movement direction based on moveForward variable
            Vector3 movementDirection = moveForward ? directionToDestination.normalized : -directionToDestination.normalized;

            // Move the object towards the destination or away from it based on movement direction
            transform.position += movementDirection * Time.deltaTime * maxSpeed;
        }
    }


    void ActivateNextChild()
    {
        if (!isAtStation && currentChildIndexToActivate < transform.childCount)
        {
            // Activate the next child
            int childIndex = moveForward ? currentChildIndexToActivate : transform.childCount - 1 - currentChildIndexToActivate;
            transform.GetChild(childIndex).gameObject.SetActive(true);
            currentChildIndexToActivate++;
        }
    }

    void DestroyFirstChild()
    {
        if (!isAtStation)
        {
            // Check if the object has any children
            if (transform.childCount > 0)
            {
                // Deactivate the first or last child based on the direction
                int childIndex = moveForward ? 0 : transform.childCount - 1;
                transform.GetChild(childIndex).gameObject.SetActive(false);
            }
        }
    }

    // Event handler for when the train enters the station
    private void OnEnable()
    {
        StationCollider.OnTrainEnterStation += HandleTrainEnterStation;
    }

    private void OnDisable()
    {
        StationCollider.OnTrainEnterStation -= HandleTrainEnterStation;
    }





    private void HandleTrainEnterStation()
    {
        Debug.Log("Train has reached the station.");

        // Start deceleration coroutine
        StartCoroutine(DecelerateAndStopTrain());
    }

    IEnumerator DecelerateAndStopTrain()
    {
        Debug.Log("Train is decelerating.");
        float initialSpeed = currentSpeed; // Store initial speed
        float elapsedTime = 0f;

        // Decelerate the train
        while (elapsedTime < decelerationDuration)
        {
            currentSpeed = Mathf.Lerp(initialSpeed, 0f, elapsedTime / decelerationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the train comes to a complete stop
        currentSpeed = 0f;

        // Start waiting at the station
        StartCoroutine(WaitAtStation(waitTime));
    }

    IEnumerator WaitAtStation(float waitTime)
    {
        isMoving = false;
        isAtStation = true;
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Train is ready to leave the station.");

        currentSpeed = 0f;

        StartCoroutine(AccelerateTrain()); // Start accelerating the train

        // Set the destination for the train to resume its movement
        SetDestination(destination);
    }

    IEnumerator AccelerateTrain()
    {
        isMoving = true;
        isAtStation = false;
        Debug.Log("Train is accelerating.");
        float elapsedTime = 0f;

        // Accelerate the train
        while (elapsedTime < accelerationDuration)
        {
            currentSpeed = Mathf.Lerp(0f, maxSpeed, elapsedTime / accelerationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the train reaches its maximum speed
        currentSpeed = maxSpeed;
    }

    IEnumerator DisableValbyStationGameObject(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        valbyStationGO.SetActive(false);

    }

}