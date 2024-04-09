using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class RejseStatus : MonoBehaviour
{
    public TrainData trainData;

    [Header("Timers:")]
    public float byeDot = 15f;
    public float blinkInterval = 0.2f; // Interval between blinks
    private float waitOnStation; // Time to wait on each station
    public float WaitBeforeStart = 5f; // Time to wait before starting the blinking
  
    public List<StationElement> stationElements;
    public int track;




    public string startingStationName;

    private int startBlinkingFromIndex = -1;

    void Start()
    {
        trainData = GameManager.instance.savedData.trainDataEntered;
        startingStationName = "Valby"; // Station name to start blinking from
        PopulateStationNames();
        if (startBlinkingFromIndex != -1)
        {
            StartCoroutine(StartBlinkingWithInitialWait(startBlinkingFromIndex));
        }
    }

    void PopulateStationNames()
    {
        if (trainData == null)
        {
            Debug.LogError("TrainData reference is missing!");
            return;
        }

        // Find the index of the starting station
        for (int i = 0; i < trainData.stations.Length; i++)
        {
            if (trainData.stations[i] == startingStationName)
            {
                startBlinkingFromIndex = i;
                break;
            }
        }

        // Clear existing station names and disable text elements and dots in stations before the starting station
        for (int i = 0; i < stationElements.Count; i++)
        {
            if (i < trainData.stations.Length)
            {
                stationElements[i].StationName.text = trainData.stations[i];
            }
            else
            {
                stationElements[i].StationName.gameObject.SetActive(false); // Disable text element
                foreach (var dot in stationElements[i].dots)
                {
                    dot.SetActive(false); // Disable dots in empty elements
                }
            }

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
                    if (dot.name == "TinyCube")
                    {
                        float startTime = Time.time;
                        while (Time.time - startTime < waitOnStation)
                        {
                            renderer.enabled = !renderer.enabled;
                            yield return new WaitForSeconds(blinkInterval);
                        }
                        renderer.enabled = false; // Ensure TinyCube is hidden after blinking
                    }
                    else
                    {
                        yield return new WaitForSeconds(byeDot); // Wait for blink duration
                        dot.SetActive(false); // Disable Dots after blink duration
                    }
                }
                else
                {
                    Debug.LogWarning("Renderer component not found on the current object.");
                }
            }
            yield return new WaitForSeconds(waitOnStation); // Wait for specified time between stations
        }
    }

    IEnumerator StartBlinkingWithInitialWait(int startIndex)
    {
        // Initial wait time before starting the blinking coroutine
        yield return new WaitForSeconds(WaitBeforeStart);

        // Start the blinking coroutine
        StartCoroutine(BlinkDots(startIndex));
    }

}

[System.Serializable]
public class StationElement
{
    public TextMeshPro StationName;
    public List<GameObject> dots;
}