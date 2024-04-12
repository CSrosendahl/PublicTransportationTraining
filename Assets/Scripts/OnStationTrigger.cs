using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStationTrigger : MonoBehaviour
{
    // Det er vigtigt stationname er det samme som Questname 
    public string stationName;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered " + stationName);
            // Fade to black ? 
        }
    }

    public void QuestStatus()
    {
        if(QuestManager.instance.currentQuest.questName == stationName)
        {

        }
    }

}
