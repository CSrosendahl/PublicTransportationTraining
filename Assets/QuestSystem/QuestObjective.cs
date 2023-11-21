using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjective : MonoBehaviour
{
    public Transform parent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get a reference to the QuestManager component
            QuestManager questManager = QuestManager.instance;
            // Check if the player has a quest
            if (questManager.currentQuest != null)
            {
                if(parent.GetComponent<TrainMover>().questObjective)
                {
                    if (questManager.currentQuest.trainID == parent.GetComponent<TrainMover>().trainData.trainID)

                    {
                        // Complete the quest
                        questManager.CompleteQuest(questManager.currentQuest);

                    }
                    else
                    {
                        questManager.WrongQuestObjective();
                        Debug.Log("Wrong train bip bop chokolade mand/Paw�");
                    }
                }
                // Check if the player's quest is the same as the objective's quest
             
            }
        }
        Debug.Log("Trigger entered");
    }
}
