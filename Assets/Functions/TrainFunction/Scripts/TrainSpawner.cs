using UnityEngine;
using System.Collections;

public class TrainSpawner : MonoBehaviour
{
    public static TrainSpawner instance;
    public float timeBetweenSpawns;

   
    private void Awake()
    {
        instance = this;
    }


    public GameObject spor5_Train;
    public GameObject spor6_Train;

    public Transform spor5_Spawn; // Location where the train will be spawned
    public Transform spor6_Spawn; // Location where the train will be spawned
                                  // public Transform spawnLocation; // Location where the train will be spawned

    public bool hasSpawned;

    // Start is called before the first frame update
    void Start()
    {
       
        StartCoroutine(SpawnTrainEveryXSeconds());
    }

    // Coroutine to spawn a train every 60 seconds
    IEnumerator SpawnTrainEveryXSeconds()
    {
        while (true) // Infinite loop to keep spawning trains
        {
            Spor6_SpawnTrain();
            yield return new WaitForSeconds(timeBetweenSpawns); // Wait for X seconds
        }
    }

    public void Spor5_SpawnTrain()
    {
        // Calculate the new rotation with an additional 90 degrees on the y-axis
        Quaternion spawnRotation = Quaternion.Euler(spor5_Spawn.rotation.eulerAngles.x, spor5_Spawn.rotation.eulerAngles.y + 90, spor5_Spawn.rotation.eulerAngles.z);

        // Instantiate a new train GameObject at the spawn location with the new rotation
        Instantiate(spor5_Train, spor5_Spawn.position, spawnRotation);
    }

    public void Spor6_SpawnTrain()
    {
        // Calculate the new rotation with an additional 90 degrees on the y-axis
        Quaternion spawnRotation = Quaternion.Euler(spor6_Spawn.rotation.eulerAngles.x, spor6_Spawn.rotation.eulerAngles.y + 90, spor6_Spawn.rotation.eulerAngles.z);

        // Instantiate a new train GameObject at the spawn location with the new rotation
        Instantiate(spor6_Train, spor6_Spawn.position, spawnRotation);

    }

}
