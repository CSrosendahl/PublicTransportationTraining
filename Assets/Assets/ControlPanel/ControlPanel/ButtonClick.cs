using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public int buttonID; // Identifikator for hver knap
    public Animator animator;

    void Start ()
    {
        animator = GetComponent<Animator>();
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
            Debug.Log("Volume enable");
            animator.SetTrigger("ChangeColorTrigger");
            break;
        case 2:
            // Handling for knap 2
            Debug.Log("Volume disable");
            animator.SetTrigger("ChangeColorTrigger");
            break;
        case 3:
            // Handling for knap 3
            Debug.Log("NPC enable");
            animator.SetTrigger("ChangeColorTrigger");
            break;
        case 4:
            // Handling for knap 4
            Debug.Log("NPC disable");
            animator.SetTrigger("ChangeColorTrigger");
            break;
        case 5:
            // Handling for knap 5
            Debug.Log("Start Spil");
            animator.SetTrigger("ChangeColorTrigger");
            break;
        case 6:
            // Handling for knap 6
            Debug.Log("Button 6 clicked");
            break;
        case 7:
            // Handling for knap 7
            Debug.Log("Button 7 clicked");
            break;
        case 8:
            // Handling for knap 8
            Debug.Log("Button 8 clicked");
            break;
        case 9:
            // Handling for knap 9
            Debug.Log("Button 9 clicked");
            break;
        case 10:
            // Handling for knap 10
            Debug.Log("Button 10 clicked");
            break;
        default:
            Debug.Log("Unhandled button click");
            break;
    }
}

}