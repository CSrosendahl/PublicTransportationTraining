using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public List<QuestData> questTemplates; // List of quest templates with different train IDs
    public QuestData currentQuest; // The player's current quest
   // public Text questInfoText; // Reference to the Text UI element
    public TextMeshPro questText;

    public AudioClip questFailedSound;
    public AudioClip questCompleteSound;
    public AudioSource audioSource;

    public float fadeDuration = 2.0f; // Duration of the fade in seconds
    public Material fadeToBlackMaterial;



    private void Start()
    {
        //AcceptQuest();
      




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
        // DisplayQuestInfo(currentQuest);

        // Wait 2 second, fade to black into new scene.
      
        GameManager.instance.CompleteQuestArea();
        audioSource.clip = questCompleteSound;
        audioSource.Play();

        Debug.Log("COMPLETED QUUUUUEST");
        AcceptQuest();
      //currentQuest = null;

    }

    public void WrongQuestObjective()
    {
        // Play sound, wrong train.
        Debug.Log("Wrong train");
        audioSource.clip = questFailedSound;
        audioSource.Play();

    }

    private QuestData GetRandomQuest()
    {
        // Randomly select a quest template from the list
        int randomIndex = Random.Range(0, questTemplates.Count);
        return questTemplates[randomIndex];
    }

    private void DisplayQuestInfo(QuestData quest)
    {
        // Update the UI to display the quest information, including the train ID
        questText.text = quest.questDescription + "\n";
                          
    }


   

}


