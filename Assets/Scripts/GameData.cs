using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Custom/GameData", order = 1)]
public class GameData : ScriptableObject
{

    
    // Add variables you want to save here
    public TrainData trainDataEntered;
    public GameObject trainObjectEntered;
    public Transform spawnTrainEntranceEntered;

    public Vector3 position;

    // Example method to reset variables
    public void ResetData()
    {
        trainDataEntered = null;
        trainObjectEntered = null;
        spawnTrainEntranceEntered = null;
        position = Vector3.zero;
    }
}
