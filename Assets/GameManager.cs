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
    public float trainSpawnInterval;
    public GameObject playerObject;

    [HideInInspector] public AudioMixer audioMixer;
    [HideInInspector] public Material onButton;
    [HideInInspector] public Material offButton;
    [HideInInspector] public GameObject NPCButton;
    [HideInInspector] public GameObject SoundButton;
    [HideInInspector] public GameObject NPCState;
    [HideInInspector] public GameObject AudioMixerGameObject;
    [HideInInspector] public Transform spawnIndgang;
    [HideInInspector] public Transform spawnControlPanel;
    [HideInInspector] public Transform completeQuestArea;
    [HideInInspector] public GameObject[] handsPhysicsObject;

   
    private void Start()
    {
        SpawnControlPanel();
    }

    public void DisableNPC()
    {
        NPCState.SetActive(!NPCState.activeSelf);
        if (NPCState.activeSelf == true)
        {
            
            NPCButton.GetComponent<Renderer>().material = offButton;
        }
        else
        {
            NPCButton.GetComponent<Renderer>().material = onButton;
        }

       
    }
    public void DisableAudioMixer()
    {
        AudioMixerGameObject.SetActive(!AudioMixerGameObject.activeSelf);

        if (AudioMixerGameObject.activeSelf == true)
        {
            SoundButton.GetComponent<Renderer>().material = offButton;
        }
        else
        {
            SoundButton.GetComponent<Renderer>().material = onButton;
           
        }
       
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
        yield return new WaitForSeconds(1f); // Adjust the duration as needed.

        for (int i = 0; i < handsPhysicsObject.Length; i++)
        {
            handsPhysicsObject[i].SetActive(true);
        }

    }
}
