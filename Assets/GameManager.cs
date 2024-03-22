using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    // Singleton pattern used for GameManager. This means that there can only be one instance of the GameManager class
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    public bool hasCheckedIn; // Boolean to check if the player has checked in
    public float trainSpawnInterval; // Time between each train spawn
    public GameObject playerObject; // Reference to the player gameObject
    public GameData savedData;
    bool hasLoadedNewScene = false;

     public AudioMixer audioMixer;
    [HideInInspector] public Material onButton;
    [HideInInspector] public Material offButton;
    [HideInInspector] public GameObject NPCButton;
    [HideInInspector] public GameObject SoundButton;
    [HideInInspector] public GameObject NPCState;
    public GameObject AudioMixerGameObject;
    [HideInInspector] public Transform spawnIndgang;
    [HideInInspector] public Transform spawnControlPanel;
    [HideInInspector] public Transform completeQuestArea;
    public GameObject[] handsPhysicsObject;
    public GameObject restrictedAreaGameObject;

   
    private void Start()
    {
       // SpawnControlPanel();
        AudioListener.volume = 1f;

        Debug.Log("Sound is on");

    }
    private void Update()
    {
        Test();
    }

    public void Test()
    {
        if(SceneManager.GetSceneByName("Map").isLoaded)
        {
            Debug.Log("Hello from map");
        }
        else if (SceneManager.GetSceneByName("TrainRide").isLoaded)
        {
            Debug.Log("Hello from trainride");
        }

    }
 

    // Disable NPC button
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
    // Disable audio button
    public void DisableAudioMixer()
    {

        // Toggle the audio listener's volume between 0 and 1
        AudioListener.volume = 1 - AudioListener.volume;

        Debug.Log("Sound is " + (AudioListener.volume > 0 ? "on" : "off"));

        

        if (AudioListener.volume == 1f)
        {
            SoundButton.GetComponent<Renderer>().material = offButton;
        }
        else
        {
            SoundButton.GetComponent<Renderer>().material = onButton;
           
        }
       
    }

    // Start the game, set checkedIn to false, quest and spawn the player at the spawn point
    // Also disable the hands physics for a short time to prevent the player from getting stuck in potential colliders
    public void StartGame()
    {

        for (int i = 0; i < handsPhysicsObject.Length; i++)
        {
            handsPhysicsObject[i].SetActive(false);
        }

        hasCheckedIn = false;
         
        if(QuestManager.instance.currentQuest != null) // If the player has a quest, set it to null
        {
            QuestManager.instance.currentQuest = null;
        }
       
        QuestManager.instance.AcceptQuest(); // Accept a new quest
        playerObject.transform.position = spawnIndgang.position; // Spawn the player at the spawn point

      StartCoroutine(EnableHandPhysicsAfterDelay());

    }

    public void TrainRideStart()
    {

    }
    // Method for teleporting our player to the play area. (Valby st. entrance)
    public void SpawnEntrance()
    {

        for (int i = 0; i < handsPhysicsObject.Length; i++)
        {
            handsPhysicsObject[i].SetActive(false);
        }

        playerObject.transform.position = spawnIndgang.position;

        StartCoroutine(EnableHandPhysicsAfterDelay());

    }

    // Method for spawning our player in the control panel area.
    public void SpawnControlPanel()
    {
        for (int i = 0; i < handsPhysicsObject.Length; i++)
        {
            handsPhysicsObject[i].SetActive(false);
        }
        playerObject.transform.position = spawnControlPanel.position;
        StartCoroutine(EnableHandPhysicsAfterDelay());
    }

    // Method for spawning our player in the complete quest area.
    public void CompleteQuestArea()
    {
        for (int i = 0; i < handsPhysicsObject.Length; i++)
        {
            handsPhysicsObject[i].SetActive(false);
        }
        playerObject.transform.position = completeQuestArea.position;
        StartCoroutine(EnableHandPhysicsAfterDelay());
    }


    // This methods purpose is to make sure our physical hands do not get stuck on any objects when teleporting.
    private IEnumerator EnableHandPhysicsAfterDelay() 
    {
        // Wait for a short delay.
        yield return new WaitForSeconds(1f); // Adjust the duration as needed.

        for (int i = 0; i < handsPhysicsObject.Length; i++)
        {
            handsPhysicsObject[i].SetActive(true);
        }

    }
    public void EnableDisableHandsss()
    {
        
        StartCoroutine(DisableEnableHands());
    
    }
    private IEnumerator DisableEnableHands()
    {
        // Wait for a short delay.
        for (int i = 0; i < handsPhysicsObject.Length; i++)
        {
            handsPhysicsObject[i].SetActive(false);
        }
        yield return new WaitForSeconds(1f); // Adjust the duration as needed.

        for (int i = 0; i < handsPhysicsObject.Length; i++)
        {
            handsPhysicsObject[i].SetActive(true);
        }

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
