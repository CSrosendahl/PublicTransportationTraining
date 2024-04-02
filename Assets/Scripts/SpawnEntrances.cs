using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEntrances : MonoBehaviour
{
    public Transform[] spawnEntrances;
    public GameObject test;

    private void Awake()
    {
      
        Debug.Log(" Awake Spawn player " + GameManager.instance.savedData.spawnEntranceNumber);
    }

    private void Start()
    {
        for (int i = 0; i < spawnEntrances.Length; i++)
        {
            if (i == GameManager.instance.savedData.spawnEntranceNumber)
            {
                GameManager.instance.playerObject.transform.position = spawnEntrances[i].transform.position;
                GameManager.instance.playerObjectIK.transform.position = spawnEntrances[i].transform.position;
                GameManager.instance.StartCoroutine(GameManager.instance.EnableHandPhysicsAfterDelay());
                test.transform.position = spawnEntrances[i].transform.position;
                Debug.Log("Was called");
            }
        }
    }
    private void Update()
    {
        
        Debug.Log(" Update Spawn player " + GameManager.instance.savedData.spawnEntranceNumber);
    }

 
}
