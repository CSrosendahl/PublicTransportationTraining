using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StationNameSignPopulator : MonoBehaviour
{
    [System.Serializable]
    public class StationTextPair
    {
        public string stationName;
        public List<TextMeshPro> textElements = new List<TextMeshPro>();
    }

    public List<StationTextPair> stationNameSignPairs; // Reference to the station name and associated TextMeshPro objects
    public TrainData trainData; // Reference to the TrainData object

    private void Start()
    {
        trainData = GameManager.instance.savedData.trainDataEntered;
        PopulateStationNameSign();
    }

    public void PopulateStationNameSign()
    {
        if (stationNameSignPairs == null || trainData == null)
        {
            Debug.LogWarning("stationNameSignPairs or trainData is null. Make sure to assign references.");
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
                if (i < stationNameSignPairs.Count)
                {
                    string stationName = stationNames[startIndex + i + 1]; // Start from index after "Valby"
                    stationNameSignPairs[i].stationName = stationName;

                    foreach (TextMeshPro textElement in stationNameSignPairs[i].textElements)
                    {
                        if (textElement != null)
                        {
                            textElement.text = stationName;
                        }
                    }
                }
                else
                {
                    Debug.LogWarning("Insufficient StationTextPair elements for population.");
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("Valby station not found in the train data.");
        }
    }
}
