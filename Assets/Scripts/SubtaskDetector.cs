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
                if (!QuestManager.instance.completedWatchScreen)
                {
                    QuestManager.instance.SubTaskWatchScreen();
                }
               
            }
            if (this.gameObject.name == "SubTask_CorrectTrack")
            {
                if (!QuestManager.instance.completedCorrectTrack)
                {
                    QuestManager.instance.SubTaskCorrectTrack(trainTrack);
                }


            }
 
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
         
            if (this.gameObject.name == "SubTask_CorrectTrack")
            {
                if(QuestManager.instance.completedCorrectTrack)
                {
                    QuestManager.instance.completedCorrectTrack = false;
                    QuestManager.instance.subTaskCompleted--;
                    QuestManager.instance.subText_CorrectTrack.color = Color.white;
                    QuestManager.instance.checkmark_CorrectTrack.SetActive(false);
                }
                
            }
        }
    }
}
