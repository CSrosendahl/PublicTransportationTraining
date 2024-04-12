using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StationNameSignPopulator : MonoBehaviour
{
    public List<TextMeshPro> stationNameSign; // Reference to the TextMeshPro objects

    // Populate the stationNameSign list with station names
    public void PopulateStationNameSign(string[] stationNames)
    {
        if (stationNameSign == null || stationNames == null)
        {
            Debug.LogWarning("stationNameSign or stationNames is null. Make sure to assign references.");
            return;
        }

        // Ensure the size of stationNameSign matches the number of stations
        while (stationNameSign.Count < stationNames.Length)
        {
            stationNameSign.Add(null); // Add null elements if needed
        }

        // Assign station names to TextMeshPro objects
        for (int i = 0; i < stationNames.Length; i++)
        {
            if (stationNameSign[i] != null)
            {
                stationNameSign[i].text = stationNames[i];
            }
        }
    }
}
