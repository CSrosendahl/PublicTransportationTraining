using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


            
            saveData.playersParentInTrain = spawnEntrance;
            saveData.position = spawnEntrance.position;


           StartCoroutine(LoadScene());

        }
    }


    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3);
        GameManager.instance.EnableDisableHandsss();


        GameManager.instance.playerObject.transform.SetParent(saveData.playersParentInTrain);
        GameManager.instance.playerObject.transform.position = saveData.position;

    }


  

}
