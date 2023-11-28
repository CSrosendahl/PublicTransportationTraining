using UnityEngine;

[CreateAssetMenu(fileName = "New Train", menuName = "Train/Train Data")]
public class TrainData : ScriptableObject
{
    public int trainID;
    public int track;
    public string trainName;
    public Material trainLineMaterial;
    public Texture texture;
    public GameObject trainPrefab;
    public Vector3 spawnPosition;


}