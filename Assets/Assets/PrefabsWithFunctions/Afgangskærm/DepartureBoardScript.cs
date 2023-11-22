using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using Unity.Collections;

public class DepartureBoardScript : MonoBehaviour
{
    // [System.Serializable]
    //public class DepartureInfo
    //{
    //    public string stationName;
    //    public int ID;
    //    public Texture image;
    //    public int trackNumber;


    //    public int GetID()
    //    {
    //        return ID;
    //    }
    //}

    //public List<DepartureInfo> departures; // List of departure information.
    // public int maxDeparturesToShow = 6; // Maximum number of departures to display.
  //  [ReadOnly]
    private float changeInterval = 60f; // Change the time in gamemanager
    public int lastSpawnedTrainID;

    private List<TextMeshPro> stationNameTextList;
    private List<TextMeshPro> trackNumberTextList;
    private List<Renderer> imageRendererList;
    private List<TextMeshPro> timeRemainingTextList; // List for time remaining on each row.
    private List<int> currentDepartureIndices;
    private float timer;
    public bool canSpawn;
   

    void Start()
    {
        InitializeDepartureDisplay();
        UpdateDepartureDisplay();

        changeInterval = GameManager.instance.trainSpawnInterval;

        InvokeRepeating("ChangeDepartures", 0f, 1f); // Update every second for time remaining.
   
    }

    void InitializeDepartureDisplay()
    {
        stationNameTextList = new List<TextMeshPro>();
        trackNumberTextList = new List<TextMeshPro>();
        imageRendererList = new List<Renderer>();
        timeRemainingTextList = new List<TextMeshPro>();
        currentDepartureIndices = new List<int>();

        for (int i = 0; i < TrainManager.instance.trainDataList.Count; i++)
        {
            Transform departureObject = transform.Find("Departure" + i);
            if (departureObject != null)
            {
                stationNameTextList.Add(departureObject.Find("StationName").GetComponent<TextMeshPro>());
                trackNumberTextList.Add(departureObject.Find("TrackNumber").GetComponent<TextMeshPro>());
                imageRendererList.Add(departureObject.Find("ImageQuad").GetComponent<Renderer>());
                timeRemainingTextList.Add(departureObject.Find("Time").GetComponent<TextMeshPro>()); // Add time remaining TextMeshPro.
                currentDepartureIndices.Add(i % TrainManager.instance.trainDataList.Count); // Initialize with sequential indices.
              
            }
            else
            {
                Debug.LogError("Departure" + i + " not found. Make sure it is a child of the GameObject.");
            }
        }
    }

    void UpdateDepartureDisplay()
    {
        for (int i = 0; i < TrainManager.instance.trainDataList.Count; i++)
        {
            int departureIndex = currentDepartureIndices[i];
            Debug.Log(departureIndex + " Departure index");

            // Set the station name, track number, and image for each departure display element.

            stationNameTextList[i].text = TrainManager.instance.trainDataList[departureIndex].trainName;
            trackNumberTextList[i].text = TrainManager.instance.trainDataList[departureIndex].track.ToString();
            imageRendererList[i].material.mainTexture = TrainManager.instance.trainDataList[departureIndex].texture;

            //  stationNameTextList[i].text = departures[departureIndex].stationName;
            //    trackNumberTextList[i].text = departures[departureIndex].trackNumber.ToString();
            //  imageRendererList[i].material.mainTexture = departures[departureIndex].image;

            // Set the time remaining text.
            float timeRemaining = i * changeInterval - timer;

            if (timeRemaining < 60f)
            {
                // Display � for less than 1 min remaining.
                timeRemainingTextList[i].text = "½min.";
            }
            else
            {
                // Display whole numbers with no seconds.
                int minutes = Mathf.FloorToInt(timeRemaining / 60);
                timeRemainingTextList[i].text = $"{minutes}min.";
            }
        }
    }


    void ChangeDepartures()
    {
        timer += 1f;

        if (timer >= changeInterval)
        {
            timer = 0f;
         
            
                // Get the index of the train to spawn (the top index)
                int topIndex = currentDepartureIndices[0];

                // Increment the top index to point to the next train
                topIndex = (topIndex + 1) % TrainManager.instance.trainDataList.Count;

                // Find the ID of the train to be spawned
                int trainID = TrainManager.instance.trainDataList[topIndex].trainID;
                lastSpawnedTrainID = trainID;

                // Update the departure indices with the new top index
                for (int i = 0; i < currentDepartureIndices.Count; i++)
                {
                    currentDepartureIndices[i] = topIndex;

                    // Increment the top index for the next iteration
                    topIndex = (topIndex + 1) % TrainManager.instance.trainDataList.Count;
                }

                // Spawn the train at the new top index
                if(canSpawn)
                {
                    TrainManager.instance.SpawnTrain(currentDepartureIndices[0]);
                }
              // Spawn the train at index 0
              //  StartCoroutine(SpawnDelayedTrainsAfterTopID());

            

            // Update the departure display
            UpdateDepartureDisplay();
           
        }
        changeInterval = GameManager.instance.trainSpawnInterval;
    }


    private IEnumerator SpawnDelayedTrainsAfterTopID() // These are filler trains for immersion
    {
        yield return new WaitForSeconds(Random.Range(1f, 2f));


        if (lastSpawnedTrainID == 0)
        {
            TrainManager.instance.SpawnTrainFiller(2);

        }
        else if (lastSpawnedTrainID == 1)
        {
            TrainManager.instance.SpawnTrainFiller(3);
        }
        else if (lastSpawnedTrainID == 2)
        {
            TrainManager.instance.SpawnTrainFiller(5);
        }
        else if (lastSpawnedTrainID == 3)
        {
            TrainManager.instance.SpawnTrainFiller(4);
        }
        else if (lastSpawnedTrainID == 4)
        {
            TrainManager.instance.SpawnTrainFiller(1);
        }
        else if (lastSpawnedTrainID == 5)
        {
            TrainManager.instance.SpawnTrainFiller(0);
        }

    }

    private IEnumerator DelayedCode(int waitTime) // These are filler trains for immersion
    {
        yield return new WaitForSeconds(waitTime);

    }


}
