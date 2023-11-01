using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RejskortStanderFunctions : MonoBehaviour
{
    public Light Blaatlys; // Reference til lyset p� standeren.RejsekortStanderUD -> Sphere -> Light
    public AudioSource GodkendtLyd; // Reference til Audiosource med godkendt lyd
    public AudioSource AfvistLyd; // Reference til Audiosource med afvist lyd
    public Material OkText; // Material til Godkendt
    public Material AfvistText; // Material Til afvist
    public Renderer ScreenText; // Reference til det object der skal have �ndret materiale

    private Material originalMaterial;
    private bool isTriggered = false;

    private void Start()
    {
        
        GodkendtLyd = GetComponent<AudioSource>();
        AfvistLyd = GetComponent<AudioSource>();
        // Disable AudioSource til at starte med
        GodkendtLyd.enabled = false;
        AfvistLyd.enabled = false;

        // Gem originalt materiale
        if (ScreenText != null)
        {
            originalMaterial = ScreenText.material;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Trigger n�r skolekortet rammer triggeren
        if (!other.CompareTag("Skolekort")) // Husk at check at skolekortet har en collider med trigger p� og er tagged med "Skolekort"
        {
            Blaatlys.enabled = false; // Slukker lyset ved Trigger
            GodkendtLyd.enabled = true; // Enable AudioSource med godkendt lyd.
            GodkendtLyd.Play(); // Play godkendt lyd
            isTriggered = true; // S�tter isTriggered til true 

            // Skift materiale p� ScreenText til OkText
            if (ScreenText != null && OkText != null)
            {
                ScreenText.material = OkText;
            }

        }
        else
        {
            Blaatlys.enabled = false;
            AfvistLyd.enabled = true; // Enable AudioSource med afvist lyd.
            AfvistLyd.Play(); // Play afvist lyd

            // Skift materiale p� ScreenText til AfvistText
            if (ScreenText != null && AfvistText != null)
            {
                ScreenText.material = AfvistText;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isTriggered)
        {
            StartCoroutine(ResetLightAndAudio()); //venter 5 sekunder f�r standeren er klar igen
        }
    }

    //Coroutine som resetter standeren
    private IEnumerator ResetLightAndAudio()
    {
        yield return new WaitForSeconds(5.0f); // vent  5 seconds. adjust som det passer

        Blaatlys.enabled = true; // T�nder lyset igen ved Trigger Exit
        GodkendtLyd.enabled = false; // Disable godkendts lyd
        AfvistLyd.enabled = false; // Disable afvist lyd
        isTriggered = false;


        // Skift materiale p� ScreenText til originalMaterial
        if (ScreenText != null)
        {
            ScreenText.material = originalMaterial;
        }

    }

}
