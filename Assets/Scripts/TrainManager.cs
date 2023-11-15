using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    public static TrainManager instance;

    public GameObject fillerTrain;
    private void Awake()
    {
        instance = this;
    }


    public List<TrainData> trainDataList; // List of TrainData assets
  //  public int desiredIndex = 1; // Make this variable public
    public bool hasSpawned;


    private void Start()
    {
      //  StartCoroutine(SpawnTrainPeriodically());
    }

    //IEnumerator SpawnTrainPeriodically()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(10);
    //        SpawnTrain(desiredIndex);
    //    }
    //}

    public void SpawnTrain(int trainIndex)
    {

        // Create a rotation quaternion with a 90-degree rotation around the y-axis
        Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
      
        // Instantiate the train with the desired rotation
        Instantiate(trainDataList[trainIndex].trainPrefab, trainDataList[trainIndex].spawnPosition, rotation);
    }

  
}

