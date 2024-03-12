using UnityEngine;

[CreateAssetMenu(fileName = "New Train", menuName = "Train/Train Data")]
public class TrainData : ScriptableObject
{
    // Train data, that contains all the information about the train

    public int trainID; // Unique ID for the train
    public int track; // the track number, used in the DepartureBoardScript to display on the screen which track the train is on.
    public string trainName; // The name of the train, used in the DepartureBoardScript to display on the screen which train is on the track.
    public Material trainLineMaterial; // the material of the train line (for example, H train, F train etc.)
    public Texture texture; // Does the same as trainLineMaterial, just in a different way - should be optimized
    public GameObject trainPrefab; // The train prefab, that is spawned when the train is spawned, each train has its own prefab
    public Vector3 spawnPosition; // The position of the train, when it is spawned


}