using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailedToExitOnStation : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Train"))
        {
            Debug.Log("Triggered");
            other.GetComponent<TrainTripMover>().enabled = false; // Stop the train from moving
            GameManager.instance.DisableAudioMixer();
            QuestManager.instance.ExitOnTheWrongStation();
        }
    }
}
