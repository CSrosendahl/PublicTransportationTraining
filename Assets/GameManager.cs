using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    // Singleton pattern used for GameManager. This means that there can only be one instance of the GameManager class
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Debug.Log("GameManager Awake");
    }

    public bool hasCheckedIn; // Boolean to check if the player has checked in
    public float trainSpawnInterval; // Time between each train spawn
    public GameObject playerObject; // Reference to the player gameObject
    public GameObject playerObjectIK; // Reference to the playersIK gameObject
    public bool canSpawnTrains = true;

     public AudioMixer audioMixer;
    [HideInInspector] public Material onButton;
    [HideInInspector] public Material offButton;
    [HideInInspector] public GameObject NPCButton;
    [HideInInspector] public GameObject SoundButton;
    public GameObject[] NPCState;
    public bool NPCDisabled;
    public bool soundDisabled;
 
    public GameObject AudioMixerGameObject;
    [HideInInspector] public Transform spawnIndgang;
    [HideInInspector] public Transform spawnControlPanel;
    public Transform completeQuestArea;
    public Transform failQuestarea;

    public GameObject[] handsPhysicsObject;
    public GameObject restrictedAreaGameObject;
    public GameData savedData;


    public GameObject[] insideTrains;
   
    private void Start()
    {
        playerObject.GetComponent<DynamicMoveProvider>().moveSpeed = 2;

        if (SceneManager.GetSceneByName("Map").isLoaded)
        {
            // Do stuff
            savedData.ResetData();
            SpawnControlPanel();
        }

        if (SceneManager.GetSceneByName("TrainTrip").isLoaded)
        {
            EnableCorrectTrain();

            if (savedData.NPCDisabled)
            {
                for (int i = 0; i < NPCState.Length; i++)
                {
                    NPCState[i].SetActive(false);
                }
                
            }
            if (savedData.SoundDisabled)
            {

                AudioListener.volume = 0f;
               

            }
          
           // SceneTransitionManager.instance.fadeScreen.FadeIn();

        }

      
        AudioListener.volume = 1f;

    }
    public void EnableCorrectTrain()
    {
        for (int i = 0; i < insideTrains.Length; i++)
        {
            TrainTrackTracker trainTrackTracker = insideTrains[i].GetComponent<TrainTrackTracker>();

            if (trainTrackTracker != null)
            {
   

                if (trainTrackTracker.track == savedData.trainDataEntered.track)
                {
                    insideTrains[i].SetActive(true); // Enable the GameObject
                    
                }
                else
                {
                    insideTrains[i].SetActive(false); // Disable the GameObject
                }
            }
       
        }
       
    }

    // Disable NPC button
    public void DisableNPC()
    {
      
        for (int i = 0; i < NPCState.Length; i++)
        {
            NPCState[i].SetActive(!NPCState[i].activeSelf);

            if (NPCState[i].activeSelf == true)
            {
                NPCDisabled = true;
                savedData.NPCDisabled = true;
                NPCButton.GetComponent<Renderer>().material = offButton;
            }
            else
            {
                savedData.NPCDisabled = false;
                NPCDisabled = false;
                NPCButton.GetComponent<Renderer>().material = onButton;
            }
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
            savedData.SoundDisabled = true;
            soundDisabled = true;
            SoundButton.GetComponent<Renderer>().material = offButton;
        }
        else
        {
            savedData.SoundDisabled = false;
            soundDisabled = false;
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
        playerObject.transform.position = spawnIndgang.position; // Spawn the player at the spawn point

    //  StartCoroutine(EnableHandPhysicsAfterDelay());

    }
    // Method for teleporting our player to the play area. (Valby st. entrance)
    public void SpawnEntrance()
    {

        //for (int i = 0; i < handsPhysicsObject.Length; i++)
        //{
        //    handsPhysicsObject[i].SetActive(false);
        //}

        playerObject.transform.position = spawnIndgang.position;
        playerObject.transform.rotation = completeQuestArea.rotation;

        // StartCoroutine(EnableHandPhysicsAfterDelay());

    }

    // Method for spawning our player in the control panel area.
    public void SpawnControlPanel()
    {
        //for (int i = 0; i < handsPhysicsObject.Length; i++)
        //{
        //    handsPhysicsObject[i].SetActive(false);
        //}

        playerObject.transform.position = spawnControlPanel.position;
        playerObject.transform.rotation = completeQuestArea.rotation;

        //StartCoroutine(EnableHandPhysicsAfterDelay());
    }

    // Method for spawning our player in the complete quest area.
    public void CompleteQuestArea()
    {
        //for (int i = 0; i < handsPhysicsObject.Length; i++)
        //{
        //    handsPhysicsObject[i].SetActive(false);
        //}

        playerObject.transform.position = completeQuestArea.position;
        playerObject.transform.rotation = completeQuestArea.rotation;

     //   StartCoroutine(EnableHandPhysicsAfterDelay());
    }
    public void FailQuestArea()
    {
        playerObject.transform.position = failQuestarea.position;
    }


    // This methods purpose is to make sure our physical hands do not get stuck on any objects when teleporting.
    public IEnumerator EnableHandPhysicsAfterDelay() 
    {
        // Wait for a short delay.
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
