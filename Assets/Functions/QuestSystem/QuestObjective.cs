using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class QuestObjective : MonoBehaviour
{
    public Transform parent;
    public Animator animator;

    private bool doorAnimPlayed;
    private void Awake()
    {
       
       animator = GetComponentInParent<Animator>();
    }
    private void Start()
    {
        doorAnimPlayed = false;
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

                        if (!QuestManager.instance.completedCorrectTrain)
                        {
                            QuestManager.instance.SubTaskCorrectTrain();

                        }
                     
                        
                        GameManager.instance.restrictedAreaGameObject.SetActive(false);
                        GameManager.instance.playerObject.GetComponent<DynamicMoveProvider>().moveSpeed = 0;
                        SceneTransitionManager.instance.GoToScene(1);
                        // Play open sound here


                    }
                    else if (questManager.currentQuest.trainID == parent.GetComponent<TrainMover>().trainData.trainID && !GameManager.instance.hasCheckedIn)
                    {
                        Debug.Log("Correct train, but you are not checked in");


                        if (!QuestManager.instance.completedCorrectTrain)
                        {
                            QuestManager.instance.SubTaskCorrectTrain();

                        }

                       
                        GameManager.instance.restrictedAreaGameObject.SetActive(false);
                        GameManager.instance.playerObject.GetComponent<DynamicMoveProvider>().moveSpeed = 0;
                        SceneTransitionManager.instance.GoToScene(1);
                    }
                    else
                    {
                        
                        StartCoroutine(WrongTrain());
                       
                        
                        // Maybe turn the button red to indicate it is the wrong train?
                    }

                    if (!doorAnimPlayed)
                    {
                        animator.SetTrigger("doorButtonPressed");
                        doorAnimPlayed = true;
                    }

                }
            }
          
        }
      
    }

    IEnumerator WrongTrain()
    {
        GameManager.instance.playerObject.GetComponent<DynamicMoveProvider>().moveSpeed = 0;


        // Fadetoblack
        SceneTransitionManager.instance.FadeToBlack_OUT();
        yield return new WaitForSeconds(2);
        QuestManager.instance.EnteredWrongTrain();

    }

    
}
