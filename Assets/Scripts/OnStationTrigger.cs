using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStationTrigger : MonoBehaviour
{
    // Det er vigtigt stationname er det samme som Questname 
    public string stationName;
    private TrainTripMover trainTripMover;

    private void Start()
    {
        trainTripMover = GameObject.FindGameObjectWithTag("Train").GetComponent<TrainTripMover>();
        Debug.Log(trainTripMover.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered " + stationName);
            //trainTripMover.isMoving = false;
            //GameManager.instance.playerObject.transform.SetParent(this.gameObject.transform);
            //GameManager.instance.playerObjectIK.transform.SetParent(this.gameObject.transform);

            QuestStatus();
            // Fade to black ? 
        }
    }

    public void QuestStatus()
    {
        if(QuestManager.instance.currentQuest.questName == stationName)
        {
            QuestManager.instance.CompleteQuest(QuestManager.instance.currentQuest);

        }else
        {
            QuestManager.instance.ExitOnTheWrongStation();
        }
    }

}
