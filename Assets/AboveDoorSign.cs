using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AboveDoorSign : MonoBehaviour
{
    public TrainData trainData; // TrainData object to populate quad objects and TextMeshPro objects
    public List<MeshRenderer> quadRenderers; // List of quad mesh renderers
    public List<TextMeshPro> trainNameTexts; // List of TextMeshPro objects for displaying train names

    // Start is called before the first frame update
    void Start()
    {
        // Assign texture and train name from trainData object to each quad renderer and TextMeshPro object
        foreach (var quadRenderer in quadRenderers)
        {
            SetTrainData(trainData, quadRenderer);
        }
        foreach (var trainNameText in trainNameTexts)
        {
            SetTrainName(trainData, trainNameText);
        }
    }

    // Method to set the texture for a single trainData object
    private void SetTrainData(TrainData trainData, MeshRenderer quadRenderer)
    {
        // Set texture to the quad
        if (trainData.texture != null && quadRenderer != null)
        {
            quadRenderer.material.mainTexture = trainData.texture;
        }
    }

    // Method to set the train name for a single trainData object
    private void SetTrainName(TrainData trainData, TextMeshPro trainNameText)
    {
        // Set train name to the TextMeshPro object
        if (!string.IsNullOrEmpty(trainData.trainName) && trainNameText != null)
        {
            trainNameText.text = trainData.trainName;
        }
    }
}
