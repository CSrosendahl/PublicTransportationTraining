using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtaskDetector : MonoBehaviour
{
    public int trainTrack;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(this.gameObject.name == "SubTask_WatchScreen")
            {
                QuestManager.instance.SubTaskWatchScreen();
            }
            if (this.gameObject.name == "SubTask_CorrectTrack")
            {
                QuestManager.instance.SubTaskCorrectTrack();
            }
            if (this.gameObject.name == "SubTask_CorrectCheckIn")
            {
                QuestManager.instance.SubTaskCheckIn();
            }
            if (this.gameObject.name == "SubTask_CorrectTrain")
            {
                QuestManager.instance.SubTaskCorrectTrain();
            }

        }
    }
}
