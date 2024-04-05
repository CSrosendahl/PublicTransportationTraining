using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class QuestObjective : MonoBehaviour
{
    public Transform parent;
    public Animator animator;
    

    private void Awake()
    {
       
       animator = GetComponentInParent<Animator>();
    }
    private void Start()
    {
     
    }

    private void OnTriggerEnter(Collider other)
    {
       // OldMethod(other);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Triggered");
          
          
            QuestManager questManager = QuestManager.instance;
            // Check if the player has a quest
            if(!parent.GetComponent<TrainMover>().isMoving) // Only check if the train is not moving
            {
                if (questManager.currentQuest != null)
                {

                    if (questManager.currentQuest.trainID == parent.GetComponent<TrainMover>().trainData.trainID && GameManager.instance.hasCheckedIn)

                    {
                        Debug.Log("Correct train,  you are checked in");
                      
                        animator.SetTrigger("doorButtonPressed");
                        GameManager.instance.restrictedAreaGameObject.SetActive(false);
                        // Play open sound here


                    }
                    else if (questManager.currentQuest.trainID == parent.GetComponent<TrainMover>().trainData.trainID && !GameManager.instance.hasCheckedIn)
                    {
                        Debug.Log("Correct train, but you are not checked in");
                       
                      
                        animator.SetTrigger("doorButtonPressed");
                        GameManager.instance.restrictedAreaGameObject.SetActive(false);
                    }
                    else
                    {
                        animator.SetTrigger("doorButtonPressed");
                        Debug.Log("Wrong train");
                        GameManager.instance.restrictedAreaGameObject.SetActive(false);
                        // Maybe turn the button red to indicate it is the wrong train?
                    }

                    // Run fade to black here and load the next scene

                    GameManager.instance.playerObject.GetComponent<DynamicMoveProvider>().moveSpeed = 0;



                    // HER PAW�
                    SceneTransitionManager.instance.GoToScene(1);
                
                      
              
                   

                }
            }
          
        }
      
    }

    public void OldMethod(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Get a reference to the QuestManager component
            QuestManager questManager = QuestManager.instance;
            // Check if the player has a quest
            if (!parent.GetComponent<TrainMover>().isMoving) // Only check if the train is not moving
            {
                if (questManager.currentQuest != null)
                {
                    if (parent.GetComponent<TrainMover>().questObjective) // Check if the train has a quest objective
                    {
                        if (questManager.currentQuest.trainID == parent.GetComponent<TrainMover>().trainData.trainID && GameManager.instance.hasCheckedIn)

                        {
                            // Complete the quest
                            Debug.Log("Correct train and you are checked in");
                            questManager.CompleteQuest(questManager.currentQuest);

                        }
                        else if (questManager.currentQuest.trainID == parent.GetComponent<TrainMover>().trainData.trainID && !GameManager.instance.hasCheckedIn)
                        {
                            Debug.Log("Correct train, but you are not checked in");
                            questManager.CompleteQuest(questManager.currentQuest);
                            // TODO: Make new function for this.
                        }
                        else
                        {
                            questManager.WrongQuestObjective();
                            Debug.Log("Wrong train");
                        }
                    }
                    // Check if the player's quest is the same as the objective's quest

                }
            }

        }
    }
}
