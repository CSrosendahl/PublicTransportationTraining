using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;

public class DepartureBoardScript : MonoBehaviour
{
    [System.Serializable]
    public class DepartureInfo
    {
        public string stationName;
        public int ID;
        public Texture image;
        public int trackNumber;


        public int GetID()
        {
            return ID;
        }
    }

    public List<DepartureInfo> departures; // List of departure information.
    public int maxDeparturesToShow = 6; // Maximum number of departures to display.
    public float changeInterval = 60f; // Time interval in seconds.

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

        InvokeRepeating("ChangeDepartures", 0f, 1f); // Update every second for time remaining.

       
    }

    void InitializeDepartureDisplay()
    {
        stationNameTextList = new List<TextMeshPro>();
        trackNumberTextList = new List<TextMeshPro>();
        imageRendererList = new List<Renderer>();
        timeRemainingTextList = new List<TextMeshPro>();
        currentDepartureIndices = new List<int>();

        for (int i = 0; i < maxDeparturesToShow; i++)
        {
            Transform departureObject = transform.Find("Departure" + i);
            if (departureObject != null)
            {
                stationNameTextList.Add(departureObject.Find("StationName").GetComponent<TextMeshPro>());
                trackNumberTextList.Add(departureObject.Find("TrackNumber").GetComponent<TextMeshPro>());
                imageRendererList.Add(departureObject.Find("ImageQuad").GetComponent<Renderer>());
                timeRemainingTextList.Add(departureObject.Find("Time").GetComponent<TextMeshPro>()); // Add time remaining TextMeshPro.
                currentDepartureIndices.Add(i % departures.Count); // Initialize with sequential indices.
            }
            else
            {
                Debug.LogError("Departure" + i + " not found. Make sure it is a child of the GameObject.");
            }
        }
    }

    void UpdateDepartureDisplay()
    {
        for (int i = 0; i < maxDeparturesToShow; i++)
        {
            int departureIndex = currentDepartureIndices[i];

            // Set the station name, track number, and image for each departure display element.
            stationNameTextList[i].text = departures[departureIndex].stationName;
            trackNumberTextList[i].text = departures[departureIndex].trackNumber.ToString();
            imageRendererList[i].material.mainTexture = departures[departureIndex].image;

            // Set the time remaining text.
            float timeRemaining = i * changeInterval - timer;

            if (timeRemaining < 60f)
            {
                // Display � for less than 1 min remaining.
                timeRemainingTextList[i].text = "�";
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

           

            for (int i = 0; i < maxDeparturesToShow; i++)
            {
                currentDepartureIndices[i] = (currentDepartureIndices[i] + 1) % departures.Count;
             

            }
            int topIndex = currentDepartureIndices[0];
            int topID = departures[topIndex].GetID();


            if(canSpawn)
            {
                
                TrainManager.instance.SpawnTrain(topID);
               
                StartCoroutine(SpawnDelayedTrainsAfterTopID());

             
            }

            StartCoroutine(DelayedCode(25));
            UpdateDepartureDisplay();
        }
    }

    private IEnumerator SpawnDelayedTrainsAfterTopID() // These are filler trains for immersion
    {
        yield return new WaitForSeconds(Random.Range(1f, 2f));

        int topIndex = currentDepartureIndices[0];
        int topID = departures[topIndex].GetID();

        if (topID == 0)
        {
            TrainManager.instance.SpawnTrainFiller(1);
            
        }
        else if (topID == 1)
        {
            TrainManager.instance.SpawnTrainFiller(2);
        }
        else if (topID == 2)
        {
            TrainManager.instance.SpawnTrainFiller(3);
        }
        else if (topID == 3)
        {
            TrainManager.instance.SpawnTrainFiller(4);
        }
        else if (topID == 4)
        {
            TrainManager.instance.SpawnTrainFiller(5);
        }
        else if (topID == 5)
        {
            TrainManager.instance.SpawnTrainFiller(0);
        }

    }

    private IEnumerator DelayedCode(int waitTime) // These are filler trains for immersion
    {
        yield return new WaitForSeconds(waitTime);

    }


}
