using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public Animator npcButtonAnim;
    public Animator soundButtonAnim;
   // public Animator soundButtonAnim;
       

    public Animator startButtonAnim;
    public GameObject[] NPCState;
    public bool NPCEnabled;
    public bool soundEnabled;
    private bool canPressButton;
    private bool startButton;
    public TextMeshPro npc_ONOFF_TEXT;
    public TextMeshPro sound_ONOFF_TEXT;
  

    public GameObject AudioMixerGameObject;
    [HideInInspector] public Transform spawnIndgang;
    [HideInInspector] public Transform spawnControlPanel;
    public Transform RetryRoom;
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

            canPressButton = true;
            NPCEnabled = true;
            soundEnabled = true;
            startButton = false;
            savedData.NPCEnabled = true;
            savedData.soundEnabled = true;
            AudioListener.volume = 1f;
            SpawnControlPanel();
        }

        if (SceneManager.GetSceneByName("TrainTrip").isLoaded)
        {
            EnableCorrectTrain();

            if (!savedData.NPCEnabled)
            {
                for (int i = 0; i < NPCState.Length; i++)
                {
                    NPCState[i].SetActive(false);
                }
                
            }
            else
            {
                for (int i = 0; i < NPCState.Length; i++)
                {
                    NPCState[i].SetActive(true);
                }
            }
            if (!savedData.soundEnabled)
            {

                AudioListener.volume = 0f;
               

            }else
            {
                AudioListener.volume = 1f;
            }
          
           // SceneTransitionManager.instance.fadeScreen.FadeIn();

        }

      
      

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
      
    
        
        if(canPressButton)
        {
            AudioManager.instance.PressButton(AudioManager.instance.buttonPress);
            if (NPCEnabled)
            {
                for (int i = 0; i < NPCState.Length; i++)
                {
                    NPCState[i].SetActive(false);
                }
                npc_ONOFF_TEXT.text = "OFF";
                savedData.NPCEnabled = false;
                NPCEnabled = false;
                npcButtonAnim.Play("ButtonOFF");
                canPressButton = false;
                StartCoroutine(ButtonCoolDown());

            }
            else
            {
                for (int i = 0; i < NPCState.Length; i++)
                {
                    NPCState[i].SetActive(true);
                }
                npc_ONOFF_TEXT.text = "ON";
                NPCEnabled = true;
                savedData.NPCEnabled = true;
                npcButtonAnim.Play("ButtonON");
                canPressButton = false;
                StartCoroutine(ButtonCoolDown());

            }
           
        }

        Debug.Log(canPressButton);
      

    }

    IEnumerator ButtonCoolDown()
    {
       
        yield return new WaitForSeconds(2);
        canPressButton = true;
    }

    // Disable audio button
    public void DisableAudioMixer()
    {

        //// Toggle the audio listener's volume between 0 and 1
        //AudioListener.volume = 1 - AudioListener.volume;

        //Debug.Log("Sound is " + (AudioListener.volume > 0 ? "on" : "off"));

        if(canPressButton)
        {
            AudioManager.instance.PressButton(AudioManager.instance.buttonPress);

            if (soundEnabled)
                {
                AudioListener.volume = 0f;
                savedData.soundEnabled = false;
                soundEnabled = false;
                soundButtonAnim.Play("ButtonOFF");
                sound_ONOFF_TEXT.text = "OFF";
                canPressButton = false;
                StartCoroutine(ButtonCoolDown());
            }
            else
                {
                AudioListener.volume = 1f;
                savedData.soundEnabled = true;
                soundEnabled = true;
                sound_ONOFF_TEXT.text = "ON";
                soundButtonAnim.Play("ButtonON");
                canPressButton = false;
                StartCoroutine(ButtonCoolDown());
            }
           
        }
        

       
    }

    // Start the game, set checkedIn to false, quest and spawn the player at the spawn point
    // Also disable the hands physics for a short time to prevent the player from getting stuck in potential colliders
    public void StartGame()
    {

        hasCheckedIn = false;
         
        if(QuestManager.instance.currentQuest != null) // If the player has a quest, set it to null
        {
            QuestManager.instance.currentQuest = null;
        }

        if(!startButton)
        {
            AudioManager.instance.PressButton(AudioManager.instance.buttonPress);
            startButtonAnim.Play("startButtonStart");
            startButton = true;
            StartCoroutine(WaitToStart(3));
            
        }
       
       

   

    }
    // Method for teleporting our player to the play area. (Valby st. entrance)
    public void SpawnEntrance()
    {


        playerObject.transform.position = spawnIndgang.position;
        playerObject.transform.rotation = spawnIndgang.rotation;
    
    

    }

    public void ReTryRome()
    {
        playerObject.transform.position = RetryRoom.position;
        playerObject.transform.rotation = RetryRoom.rotation;
    }

    // Method for spawning our player in the control panel area.
    public void SpawnControlPanel()
    {
    
        playerObject.transform.position = spawnControlPanel.position;
        playerObject.transform.rotation = spawnControlPanel.rotation;

      
    }

    // Method for spawning our player in the complete quest area.
    public void CompleteQuestArea()
    {


        playerObject.transform.position = completeQuestArea.position;
        playerObject.transform.rotation = completeQuestArea.rotation;

     
    }
    public void FailQuestArea()
    {
        playerObject.transform.position = failQuestarea.position;
        playerObject.transform.rotation = failQuestarea.rotation;
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

    public IEnumerator WaitToStart(float waitTime)
    {
        // Wait for a short delay.
        SceneTransitionManager.instance.FadeToBlack_OUT();
        yield return new WaitForSeconds(waitTime); // Adjust the duration as needed.
        SceneTransitionManager.instance.FadeToBlack_IN();
        QuestManager.instance.AcceptQuest(); // Accept a new quest
        SpawnEntrance();

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
