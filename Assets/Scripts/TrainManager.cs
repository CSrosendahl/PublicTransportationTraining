using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    public static TrainManager instance;

    // Singleton pattern used for TrainManager. This means that there can only be one instance of the TrainManager class
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


   
    #region SpawnTrainFiller method. Currently not in use, but can be used to spawn trains without questObjective. These trains does not stop at the station.
    public void SpawnTrainFiller(int trainIndex) // Spawn a train without questObjective
    {
        
        Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);

        GameObject newTrain = Instantiate(trainDataList[trainIndex].trainPrefab, trainDataList[trainIndex].spawnPosition, rotation);
        TrainMover trainMover = newTrain.GetComponent<TrainMover>();
        trainMover.questObjective = false; // Set questObjective to false for the spawned train
       // trainMover.signText[0].text = "Out of order";

        // If needed, remove certain components from the train here to avoid confusing for the user.
        // For example, remove signs, or add a "Out of order" sign on the train.

    }
    #endregion


}

