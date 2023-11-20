using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public bool hasCheckedIn;

    public AudioMixer audioMixer;
    public GameObject disableNPCs;

    public Transform spawnIndgang;
    public Transform spawnControlPanel;
    public Transform completeQuestArea;

    public GameObject playerObject;
    private void Start()
    {
        SpawnControlPanel();
    }

    public void DisableNPC()
    {
       disableNPCs.SetActive(false);
    }
    public void DisableAudioMixer()
    {
        audioMixer.SetFloat("MasterVolume", -80f);
    }

    public void StartGame()
    {

        hasCheckedIn = false;
        if(QuestManager.instance.currentQuest != null)
        {
            QuestManager.instance.currentQuest = null;
        }
        
        QuestManager.instance.AcceptQuest();

    }
    public void SpawnIndgang()
    {
        playerObject.transform.position = spawnIndgang.position;
        StartGame();
    }
    public void SpawnControlPanel()
    {
        playerObject.transform.position = spawnControlPanel.position;
    }
    public void CompleteQuestArea()
    {
        playerObject.transform.position = completeQuestArea.position;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
   


}
