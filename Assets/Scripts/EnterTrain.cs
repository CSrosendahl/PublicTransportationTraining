using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrain : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject trainObject;
    public TrainData trainData;
    public Transform parent;
    public int spawnEntranceNumber;

    
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
            if(trainData.trainID == QuestManager.instance.currentQuest.trainID)
            {
                Debug.Log("Correct train, populate savedata");
                //saveData.ResetData();
                saveData.trainDataEntered = trainData;
                saveData.trainObjectEntered = trainData.trainPrefab.gameObject;

                saveData.subTasksCompleted = QuestManager.instance.subTaskCompleted;

                //GameManager.instance.playerObject.transform.position = spawnEntrance.position;

                saveData.position = other.transform.position;
                saveData.spawnEntranceNumber = spawnEntranceNumber;
            }
            else
            {
                Debug.Log("Wrong train, Do not populate savedata");
            }
           
            


        }
    }


}
