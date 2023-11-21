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

    public GameObject[] handsPhysicsObject;

    public GameObject playerObject;
    private void Start()
    {
       SpawnControlPanel();
      
       
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

        for (int i = 0; i < handsPhysicsObject.Length; i++)
        {
            handsPhysicsObject[i].SetActive(false);
        }

        hasCheckedIn = false;
        if(QuestManager.instance.currentQuest != null)
        {
            QuestManager.instance.currentQuest = null;
        }
       
        QuestManager.instance.AcceptQuest();
        playerObject.transform.position = spawnIndgang.position;

      StartCoroutine(EnableHandPhysicsAfterDelay());

    }
    public void SpawnIndgang()
    {

        for (int i = 0; i < handsPhysicsObject.Length; i++)
        {
            handsPhysicsObject[i].SetActive(false);
        }

        playerObject.transform.position = spawnIndgang.position;

        StartCoroutine(EnableHandPhysicsAfterDelay());

    }
    public void SpawnControlPanel()
    {
        for (int i = 0; i < handsPhysicsObject.Length; i++)
        {
            handsPhysicsObject[i].SetActive(false);
        }
        playerObject.transform.position = spawnControlPanel.position;
        StartCoroutine(EnableHandPhysicsAfterDelay());
    }
    public void CompleteQuestArea()
    {
        for (int i = 0; i < handsPhysicsObject.Length; i++)
        {
            handsPhysicsObject[i].SetActive(false);
        }
        playerObject.transform.position = completeQuestArea.position;
        StartCoroutine(EnableHandPhysicsAfterDelay());
    }
    public void QuitGame()
    {
        Application.Quit();
    }


    private IEnumerator EnableHandPhysicsAfterDelay()
    {
        // Wait for a short delay (you can adjust the duration as needed).
        yield return new WaitForSeconds(0.3f); // Adjust the duration as needed.

        for (int i = 0; i < handsPhysicsObject.Length; i++)
        {
            handsPhysicsObject[i].SetActive(true);
        }

    }
}
