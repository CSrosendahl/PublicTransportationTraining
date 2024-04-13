using System;

[Serializable]
public class QuestData
{
    public string questName;
   
    public string questDescription;
    public string stationName;
    public string exitOnStation;
    public int trainID;
    public int trainTrack;
    // Add other quest-related data as needed

    public void ResetQuest()
    {
        questName= null;
        questDescription= null;
        stationName= null;
        exitOnStation= null;
        trainID = -1;
        trainTrack = -1;

    }
}