using System.Collections.Generic;
using UnityEngine;
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
    public Text questInfoText; // Reference to the Text UI element
   

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
        currentQuest = null;
        DisplayQuestInfo(currentQuest);
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
        questInfoText.text = "Quest Name: " + quest.questName + "\n"
                           + "Quest Description: " + quest.questDescription + "\n";
                          
    }

  
    // make a test debug function
    public void DebugQuest()
    {
        Debug.Log("Quest Name: " + currentQuest.questName + "\n"
                                      + "Quest Description: " + currentQuest.questDescription + "\n");
    }

}


