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
    public GameObject NPCState;

    public Transform spawnIndgang;
    public Transform spawnControlPanel;
    public Transform completeQuestArea;

    public GameObject playerObject;
    private void Start()
    {
        SpawnControlPanel();
       // StartGame();
    }

    public void DisableNPC()
    {
       NPCState.SetActive(false);
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
        playerObject.transform.position = spawnIndgang.position;

    }
    public void SpawnIndgang()
    {
        playerObject.transform.position = spawnIndgang.position;
       
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
