using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RejseStatus : MonoBehaviour
{
    public string startingStationName = "Herlev"; // Starting station
    public TrainData trainData; 
    public float blinkDuration = 2f; // Duration each dot is visible during blinking
    public float blinkInterval = 0.2f; // Interval between blinks
    public float waitOnStation = 5f; // Time to wait on each station
    public List<StationElement> stationElements;


    private int startBlinkingFromIndex = -1;

    void Start()
    {
        PopulateStationNames();
        if (startBlinkingFromIndex != -1)
        {
            StartCoroutine(BlinkDots(startBlinkingFromIndex));
        }
    }

    //Get all the StationNames into the text element
    void PopulateStationNames()
    {
        if (trainData == null)
        {
            Debug.LogError("TrainData has gone bye bye RIP");
            return;
        }

        // Find the index of the starting station
        for (int i = 0; i < trainData.stations.Length; i++)
        {
            if (trainData.stations[i] == startingStationName)
               // Debug.Log("Yay we start at" startingStationName);
            {
                startBlinkingFromIndex = i;
                break;
            }
        }

        // Disable dots in stations before the starting station - No Dots for you
        for (int i = 0; i < stationElements.Count; i++)
        {
            stationElements[i].StationName.text = trainData.stations[i];

            // Check if the current station is before the starting station
            if (i < startBlinkingFromIndex)
            {
                foreach (var dot in stationElements[i].dots)
                {
                    dot.SetActive(false);
                }
            }
        }
    }

    //Blink Blink - waitforseconds can all be set in the inspector ! yay!
    IEnumerator BlinkDots(int startIndex)
    {
        for (int i = startIndex; i < stationElements.Count; i++)
        {
            var stationElement = stationElements[i];
            foreach (var dot in stationElement.dots)
            {
                Renderer renderer = dot.GetComponent<Renderer>();
                if (renderer != null)
                {
                    float startTime = Time.time;
                    while (Time.time - startTime < blinkDuration)
                    {
                        renderer.enabled = !renderer.enabled;
                        yield return new WaitForSeconds(blinkInterval);
                    }
                    renderer.enabled = false; // Ensure the dot goes bye bye after blinking - its tired ZzZ
                }
                else
                {
                    Debug.LogWarning("Renderer component not found on the current object.");
                }
            }
            yield return new WaitForSeconds(waitOnStation); // Wait time between stations
        }
    }
}

[System.Serializable]
public class StationElement
{
    public TextMeshPro StationName;
    public List<GameObject> dots;
}
