using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    // Singleton pattern used for QuestManager. This means that there can only be one instance of the QuestManager class
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public List<QuestData> questList; // List of quest with different train IDs
    public QuestData currentQuest; // The player's current quest
    public TextMeshPro questText; // Text description on the phone
    public MeshRenderer questInfoOnPhone; // This will display the image of the trains line on the phone (f. eks "H" or "F" etc. train)
    public TextMeshPro questCompletedOrFail;

    public Material[] linjeMaterial; // Optimize thiiiiiiis!

    public AudioClip questFailedSound;
    public AudioClip questCompleteSound;
    public AudioSource audioSource;

    private void Start()
    {
        //AcceptQuest();
  
     
        questText.text = "";
        questInfoOnPhone.enabled = false;


    }
    public void AcceptQuest()
    {
        // Assign a random quest to the player at the start of the game
        currentQuest = GetRandomQuest();
        DisplayQuestInfo(currentQuest);
       

    }

    public void CompleteQuest(QuestData quest)
    {
        // Complete quest, and end the game
        // Fade out/Blackscreen/Sound/Prompt....
        // Wait 2 second, fade to black into new scene.
      
        GameManager.instance.CompleteQuestArea();
        audioSource.clip = questCompleteSound;
        audioSource.Play();
        questCompletedOrFail.text = "Tillykke!\r\nDu tog det rigtige tog";
        Debug.Log("Quest Complete");
      //  questInfoOnPhone.enabled = false;
        questText.text = "";
      
        currentQuest = null;
        

    }

    public void WrongQuestObjective()
    {
        // Play sound, wrong train.
       
        //audioSource.clip = questFailedSound;
        //audioSource.Play();
        GameManager.instance.CompleteQuestArea();
        questCompletedOrFail.text = "Desværre!\r\nDu tog det forkerte tog";
        questText.text = "";
        currentQuest = null;
        Debug.Log("Quest failed, wrong train");

    }

    private QuestData GetRandomQuest()
    {
        questInfoOnPhone.enabled = true;
        // Randomly select a quest template from the list
        int randomIndex = Random.Range(0, questList.Count); // Get a random quest based on our questList
        return questList[randomIndex];
    }

    private void DisplayQuestInfo(QuestData quest)
    {
        // Update the UI to display the quest information, including the train ID
       
        if(currentQuest != null)
        {
            questInfoOnPhone.enabled = true;
            questText.text = quest.questDescription + "\n";
        }
        else
        {
            
            questInfoOnPhone.enabled = false;
            
        }

        // OPTIMIZE THIS ! Get the data from TrainData instead of the quest
        if(currentQuest.trainID == 0)
        {
            questInfoOnPhone.material = linjeMaterial[0];
        }
        else if(currentQuest.trainID == 1)
        {
            questInfoOnPhone.material = linjeMaterial[1];
        }
        else if (currentQuest.trainID == 2)
        {
            questInfoOnPhone.material = linjeMaterial[2];
        }
        else if (currentQuest.trainID == 3)
        {
            questInfoOnPhone.material = linjeMaterial[3];
        }
        else if (currentQuest.trainID == 4)
        {
            questInfoOnPhone.material = linjeMaterial[4];
        }
        else if (currentQuest.trainID == 5)
        {
            questInfoOnPhone.material = linjeMaterial[5];
        }
     

    }


}


