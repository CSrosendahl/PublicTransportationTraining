using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfgangsScreen : MonoBehaviour
{
    public GameObject ScreenObject;
    public Material[] materials;
    public float materialChangeInterval = 120.0f; // 2 minutes i sekunder

    private Renderer objectRenderer;
    private int currentMaterialIndex = 0;
    public float timeSinceLastMaterialChange = 0.0f;
    public bool canSpawnTrain = false;

    
    void Start()
    {
        if (ScreenObject != null)
        {
            objectRenderer = ScreenObject.GetComponent<Renderer>();
            if (objectRenderer == null)
            {
                Debug.LogError("No Render Component.");
            }
        }
        else
        {
            Debug.LogError("Target object is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (materials.Length == 0 || objectRenderer == null)
            return;

        timeSinceLastMaterialChange += Time.deltaTime;



        if (timeSinceLastMaterialChange >= materialChangeInterval)
        {
            timeSinceLastMaterialChange = 0.0f;
            currentMaterialIndex = (currentMaterialIndex + 1) % materials.Length;
            objectRenderer.material = materials[currentMaterialIndex];


            if (!TrainSpawner.instance.hasSpawned && canSpawnTrain)
            {

                TrainSpawner.instance.Spor5_SpawnTrain(); // Spawning spor5train when materials change
                TrainSpawner.instance.hasSpawned = true;

                Debug.Log(TrainSpawner.instance.hasSpawned);
            }
        }

        Debug.Log("Material changed");
    }
}
