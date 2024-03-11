using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrain : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject trainObject;
    public TrainData trainData;
    public Transform parent;
    public Transform spawnEntrance;
    
    public GameData saveData;
    void Start()
    {
        trainData = parent.GetComponent<TrainMover>().trainData;
        trainObject = parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            saveData.ResetData();
            saveData.trainDataEntered = trainData;
            saveData.trainObjectEntered = trainData.trainPrefab.gameObject;
          
            GameManager.instance.playerObject.transform.position = spawnEntrance.position;

            saveData.position = other.transform.position;


            Invoke("DelayedSceneChange", 2f);

        }
    }

    private void DelayedSceneChange()
    {
        GameManager.instance.SceneChanger();
    }

}
