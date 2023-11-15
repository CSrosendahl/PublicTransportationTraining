using UnityEngine;

[CreateAssetMenu(fileName = "New Train", menuName = "Train/Train Data")]
public class TrainData : ScriptableObject
{
    public int trainID;
    public GameObject trainPrefab;
    public Vector3 spawnPosition;


}