using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public int buttonID; // Identifikator for hver knap
    //public Animator animator;

    void Start ()
    {
      //  animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ButtonClicked(buttonID); // Kald funktionen med knappens ID
        }
    }

    void ButtonClicked(int id)
    {
        // Udfør handling baseret på knappens ID
        switch (id)
        {
            case 1:
                // Handling for knap 1
                Debug.Log("Disable Sound");
                //animator.SetTrigger("ChangeColorTrigger");
                GameManager.instance.DisableAudioMixer();
                break;
            case 2:
                // Handling for knap 2
                Debug.Log("Disable NPC");
                //  animator.SetTrigger("ChangeColorTrigger");
                GameManager.instance.DisableNPC();
                break;
            case 3:
                // Handling for knap 3
                Debug.Log("Start Game");
               // animator.SetTrigger("ChangeColorTrigger");
                GameManager.instance.StartGame();

                break;

           case 4:
               // Handling for knap 4
                Debug.Log("Exit Game");
                GameManager.instance.QuitGame();
                break;
            default:
                Debug.Log("Unhandled button click");
                break;
        }
    }

}