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
    [Header("Quest Data")]
    public List<QuestData> questList; // List of quest with different train IDs
    public QuestData currentQuest; // The player's current quest

    [Header("UI Elements for phone")]
    public TextMeshPro questText0; // Text description on the phone (Tag toget i reting af)
    public TextMeshPro questText1; // Text description on the phone (Stå af på)

    public TextMeshPro subText_WatchScreen; // Text description on the phone (Watch the screen)
    public TextMeshPro subText_CorrectTrack;  // Text description on the phone (Find the correct track)
    public TextMeshPro subText_CheckIn; // Text description on the phone (Check in)
    public TextMeshPro subText_CorrectTrain; // Text description on the phone (Find the correct train)

    public GameObject checkmark_WatchScreen;
    public GameObject checkmark_CorrectTrack;
    public GameObject checkmark_CheckIn;
    public GameObject checkmark_CorrectTrain;
     
    public MeshRenderer questInfoOnPhone; // This will display the image of the trains line on the phone (f. eks "H" or "F" etc. train)
    public Material[] linjeMaterial; // Optimize thiiiiiiis! (This belongs to questInfoOnPhone)


    [Header("Random stuff")]
    public TextMeshPro questCompletedOrFail;
    public AudioClip questFailedSound;
    public AudioClip questCompleteSound;
    public AudioSource audioSource;

    private void Start()
    {
        if (SceneManager.GetSceneByName("Map").isLoaded)
        {
            // Do stuff
            AcceptQuest();
         

        }
        if (SceneManager.GetSceneByName("TrainTrip").isLoaded)
        {
            // Do stuff
            currentQuest = GameManager.instance.savedData.currentQuest;

            DisplayQuestInfo(currentQuest);
           

        }


    }
    public void AcceptQuest()
    {
        // Assign a random quest to the player at the start of the game
        currentQuest = GetRandomQuest();
        DisplayQuestInfo(currentQuest);

        GameManager.instance.savedData.currentQuest = currentQuest;


    }
    public void DisplayTrainTripQuest()
    {
        if (currentQuest != null)
        {
            questInfoOnPhone.enabled = true;
            questText0.text = currentQuest.questDescription + "\n";
            questText1.text = currentQuest.exitOnStation + "\n";
        }
      

        // OPTIMIZE THIS ! Get the data from TrainData instead of the quest
        if (currentQuest.trainID == 0)
        {
            questInfoOnPhone.material = linjeMaterial[0];

        }
        else if (currentQuest.trainID == 1)
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

    public void CompleteQuest(QuestData quest)
    {
        // Complete quest, and end the game
        // Fade out/Blackscreen/Sound/Prompt....
        // Wait 2 second, fade to black into new scene.
      
     //   GameManager.instance.CompleteQuestArea();
        audioSource.clip = questCompleteSound;
        audioSource.Play();
        Debug.Log("Quest Complete");
    

    }

    public void WrongQuestObjective()
    {
        // Play sound, wrong train.

     

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
            questText0.text = quest.questDescription + "\n";
            questText1.text = quest.exitOnStation + "\n";

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


    public void SubTaskWatchScreen()
    {

    }
    public void SubTaskCheckIn()
    {

    }
    public void SubTaskCorrectTrack()
    {

    }
    public void SubTaskCorrectTrain()
    {

    }
   


}


