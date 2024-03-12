using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
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
         //   DontDestroyOnLoad(gameObject);
           
        }
      
    }

    public bool hasCheckedIn; // Boolean to check if the player has checked in
    public float trainSpawnInterval; // Time between each train spawn
    public GameObject playerObject; // Reference to the player gameObject
    public GameData savedData; // Reference to the GameData asset

    private bool playerInstantiated = false;



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
       // SpawnControlPanel();
        AudioListener.volume = 1f;
        StartGame();
        Debug.Log("Sound is on");
       
    }

    public void InitGameManager()
    {
     
        audioMixer = Resources.Load<AudioMixer>("AudioMixer");
        onButton = Resources.Load<Material>("OnButton");
        offButton = Resources.Load<Material>("OffButton");
        NPCButton = GameObject.Find("NPCButton");
        SoundButton = GameObject.Find("SoundButton");
        NPCState = GameObject.Find("NPCState");
        AudioMixerGameObject = GameObject.Find("AudioMixer");
        spawnIndgang = GameObject.Find("SpawnIndgang").transform;
        spawnControlPanel = GameObject.Find("SpawnControlPanel").transform;
        completeQuestArea = GameObject.Find("CompleteQuestArea").transform;
        handsPhysicsObject = GameObject.FindGameObjectsWithTag("HandsPhysics");
    
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

        //for (int i = 0; i < handsPhysicsObject.Length; i++)
        //{
        //    handsPhysicsObject[i].SetActive(false);
        //}

        hasCheckedIn = false;
         
        if(QuestManager.instance.currentQuest != null) // If the player has a quest, set it to null
        {
            QuestManager.instance.currentQuest = null;
        }
       
        QuestManager.instance.AcceptQuest(); // Accept a new quest
      //  playerObject.transform.position = spawnIndgang.position; // Spawn the player at the spawn point

    //  StartCoroutine(EnableHandPhysicsAfterDelay());

    }

    public void DisableHands()
    {

        for (int i = 0; i < handsPhysicsObject.Length; i++)
        {
            handsPhysicsObject[i].SetActive(false);
        }

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

    public void SceneChanger()
    {
        SceneManager.LoadScene("TrainRide");

        // Check the spawn point's position

        // Set the player's position to the spawnTrainEntranceEntered position
     
       
        Debug.Log("Test SceneChanger called");
    }


    private void Update()
    {
        // Check if the "TrainRide" scene is loaded and the player has not been instantiated yet
        if (SceneManager.GetSceneByName("TrainRide").isLoaded && !playerInstantiated)
        {
            // Instantiate the player prefab
            playerObject.transform.position = savedData.position;
            playerInstantiated = true; // Set the flag to true to prevent further instantiation
          
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SetPlayerPosition(Vector3 position)
    {

        if (playerObject != null)
        {
            playerObject.transform.position = position;
        }
        else
        {
            Debug.LogError("Player object reference is missing!");
        }
    }
  
 

}
