using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    public static TrainManager instance;

    private void Awake()
    {
        instance = this;
    }

    public List<TrainData> trainDataList; // List of TrainData assets
    public bool hasSpawned;


    public void SpawnTrain(int trainIndex) // Spawn a train with QuestObjective
    {

        // Create a rotation quaternion with a 90-degree rotation around the y-axis
        Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);

        GameObject newTrain = Instantiate(trainDataList[trainIndex].trainPrefab, trainDataList[trainIndex].spawnPosition, rotation);
        TrainMover trainMover = newTrain.GetComponent<TrainMover>();

        trainMover.questObjective = true; // Set questObjective to true for the spawned train


    }
    public void SpawnTrainFiller(int trainIndex) // Spawn a train without questObjective
    {
        
        Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);

        GameObject newTrain = Instantiate(trainDataList[trainIndex].trainPrefab, trainDataList[trainIndex].spawnPosition, rotation);
        TrainMover trainMover = newTrain.GetComponent<TrainMover>();
        trainMover.questObjective = false; // Set questObjective to false for the spawned train

        // If needed, remove certain components from the train here to avoid confusing for the user.
        // For example, remove signs, or add a "Out of order" sign on the train.

    }


}

