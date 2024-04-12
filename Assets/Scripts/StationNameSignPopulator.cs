using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StationNameSignPopulator : MonoBehaviour
{
    public List<TextMeshPro> stationNameSign; // Reference to the TextMeshPro objects
    public TrainData trainData; // Reference to the TrainData object

    private void Start()
    {
        trainData = GameManager.instance.savedData.trainDataEntered;
        PopulateStationNameSign();
    }

    public void PopulateStationNameSign()
    {
        if (stationNameSign == null || trainData == null)
        {
            Debug.LogWarning("stationNameSign or trainData is null. Make sure to assign references.");
            return;
        }

        string[] stationNames = trainData.stations;
        int startIndex = -1;

        // Find the index of "Valby" station
        for (int i = 0; i < stationNames.Length; i++)
        {
            if (stationNames[i] == "Valby")
            {
                startIndex = i;
                break;
            }
        }

        // Start populating station names after "Valby", up to a maximum of 6
        if (startIndex != -1)
        {
            int numStationsToPopulate = Mathf.Min(stationNames.Length - startIndex - 1, 6);
            for (int i = 0; i < numStationsToPopulate; i++)
            {
                if (stationNameSign[i] != null)
                {
                    stationNameSign[i].text = stationNames[startIndex + i + 1]; // Start from index after "Valby"
                }
            }
        }
        else
        {
            Debug.LogWarning("Valby station not found in the train data.");
        }
    }

  
   
}
