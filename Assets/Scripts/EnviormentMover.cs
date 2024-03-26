using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnviormentMover : MonoBehaviour
{
    public List<Transform> destinations; // List of destinations that the train stops at
    private int currentDestinationIndex = 0; // Index of the current destination in the list

    public float maxSpeed = 1.0f;
    public float acceleration = 0.5f;
    public float quickDecelerationRate = 2.0f;
    public float decelerationDistance = 5.0f;
    public float stopDistance = 0.1f;
    public float waitTime = 10.0f;

    private float currentSpeed = 0f;
    private bool isMoving = false;

    public List<GameObject> prefabsToSpawn;
    public Transform spawnPoint;
    public float SpawnTimer = 60.0f;

    private float nextDestroyTime = 0f;
    public float destroyInterval = 30f; // Interval in seconds between each destruction



    void Start()
    {
        nextDestroyTime = Time.time + destroyInterval;
        if (destinations.Count > 0)
        {
            // Set the first destination as the current one and start moving
            SetDestination(0);
            isMoving = true;
        }

        StartCoroutine(SpawnObjectAfterDelay());
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTrain();
        }

        if (Time.time >= nextDestroyTime)
        {
            DestroyFirstChild();
            nextDestroyTime = Time.time + destroyInterval;
        }


    }

    void MoveTrain()
    {
        float distanceToDestination = Vector3.Distance(transform.position, destinations[currentDestinationIndex].position);

        if (distanceToDestination < decelerationDistance && currentSpeed > 1f)
        {
            currentSpeed = Mathf.Max(currentSpeed - quickDecelerationRate * Time.deltaTime, 1f);
        }
        else if (currentSpeed < maxSpeed && distanceToDestination >= decelerationDistance)
        {
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
        }

        transform.position = Vector3.MoveTowards(transform.position, destinations[currentDestinationIndex].position, currentSpeed * Time.deltaTime);

        if (distanceToDestination <= stopDistance && currentSpeed <= 1f)
        {
            isMoving = false;
            StartCoroutine(WaitAtDestination());
        }
    }

    IEnumerator WaitAtDestination()
    {
        yield return new WaitForSeconds(waitTime);
        // Move to the next destination in the list
        currentDestinationIndex = (currentDestinationIndex + 1) % destinations.Count;
        currentSpeed = 0f;
        isMoving = true;
    }

    public void SetDestination(int index)
    {
        if (index >= 0 && index < destinations.Count)
        {
            currentDestinationIndex = index;
        }
    }


    IEnumerator SpawnObjectAfterDelay()
    {
        yield return new WaitForSeconds(SpawnTimer); // Wait for 1 minute

        if (prefabsToSpawn.Count > 0 && spawnPoint != null)
        {
            // Randomly select a prefab from the list
            int randomIndex = Random.Range(0, prefabsToSpawn.Count);
            GameObject prefabToSpawn = prefabsToSpawn[randomIndex];

            // Spawn the selected prefab at the spawn point
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);

            // Parent the spawned object to the moving object
            spawnedObject.transform.parent = transform; // Assuming this script is attached to the moving object
        }
    }


    void DestroyFirstChild()
    {
        // Check if the object has any children
        if (transform.childCount > 0)
        {
            // Destroy the first child
            Destroy(transform.GetChild(0).gameObject);
        }
    }

}
