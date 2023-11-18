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
   

    private void Start()
    {
        AcceptQuest();
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

        Debug.Log("COMPLETED QUUUUUEST");
        currentQuest = null;

    }

    public void WrongQuestObjective()
    {
        // Play sound, wrong train.
        Debug.Log("Wrong train");

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


