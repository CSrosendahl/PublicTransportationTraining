using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildDisable : MonoBehaviour
{
    public Transform parentObject; // Reference to the parent object whose children you want to enable/disable
    public int childrenToEnable = 2; // Number of children to enable at a time
    public float enableDelay = 15f; // Delay between enabling children
    public float disableDelay = 25f; // Delay before disabling children
    private Transform[] children; // Array to hold references to the children objects
    private int enableIndex = 1; // Index to keep track of which child to enable next
    private int disableIndex = 0; // Index to keep track of which child to disable next
    public float startDelay = 10f; // Delay before starting the enable/disable routine
    public GameObject valbyStation;

    void Start()
    {
        // Get references to all children of the parent object
        children = new Transform[parentObject.childCount];
        for (int i = 0; i < parentObject.childCount; i++)
        {
            children[i] = parentObject.GetChild(i);
        }

        // Start the delayed coroutine to enable/disable children
        StartCoroutine(DelayedStart());
    }

    IEnumerator DelayedStart()
    {
        // Wait for the specified start delay
        yield return new WaitForSeconds(startDelay);

        valbyStation.SetActive(false);
        // Start the coroutine to enable/disable children
        StartCoroutine(EnableDisableRoutine());
    }

    IEnumerator EnableDisableRoutine()
    {
        while (true)
        {
            // Enable the specified number of children
            for (int i = 0; i < childrenToEnable; i++)
            {
                int indexToEnable = (enableIndex + i) % children.Length;
                children[indexToEnable].gameObject.SetActive(true);
            }

            // Wait for enableDelay seconds
            yield return new WaitForSeconds(enableDelay);

            // Disable one child
            children[disableIndex].gameObject.SetActive(false);
            disableIndex = (disableIndex + 1) % children.Length;

            // Move to the next set of children to enable
            enableIndex = (enableIndex + childrenToEnable) % children.Length;

            // Wait for disableDelay seconds before enabling the next set of children
            yield return new WaitForSeconds(disableDelay - enableDelay);
        }
    }
}
