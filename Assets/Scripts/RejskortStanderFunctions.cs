using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RejskortStanderFunctions : MonoBehaviour
{
    public Light Blaatlys; // Reference til lyset p� standeren.RejsekortStanderUD -> Sphere -> Light

    public AudioSource audioSource;
    public AudioClip godkendtClip;
    public AudioClip afvistClip;

    public Material OkText; // Material til Godkendt
    public Material AfvistText; // Material Til afvist
    public Renderer ScreenText; // Reference til det object der skal have �ndret materiale
  

    private Material originalMaterial;
   
    public bool canInteract;
    private bool isCoroutineRunning = false;

    private void Start()
    {

        canInteract = true;

        // Gem originalt materiale
        if (ScreenText != null)
        {
            originalMaterial = ScreenText.material;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
       // if(other.CompareTag("Player"))
        //{
          //  return;
        //}

      

        // Trigger n�r skolekortet rammer triggeren
        if (other.CompareTag("Skolekort") && canInteract) // Husk at check at skolekortet har en collider med trigger p� og er tagged med "Skolekort"
        {
            audioSource.clip = godkendtClip;
            Blaatlys.enabled = false; // Slukker lyset ved Trigger
           // GodkendtLyd.enabled = true; // Enable AudioSource med godkendt lyd.
            audioSource.Play();
            //.Play(); // Play godkendt lyd
            //isTriggered = true; // S�tter isTriggered til true
            canInteract = false;

            Debug.Log("Godkendt");

            // Skift materiale p� ScreenText til OkText
            if (ScreenText != null && OkText != null)
            {
                ScreenText.material = OkText;
            }

            StartCoroutine(ResetLightAndAudio());

        }
        //else
        //{
        //    if (canInteract)
        //    {
        //        Blaatlys.enabled = false;
        //        // AfvistLyd.enabled = true; // Enable AudioSource med afvist lyd.
        //        // AfvistLyd.Play(); // Play afvist lyd
        //        audioSource.clip = afvistClip;
        //        audioSource.Play();
        //        canInteract = false;
        //        Debug.Log("Afvist");
                

        //        // Skift materiale p� ScreenText til AfvistText
        //        if (ScreenText != null && AfvistText != null)
        //        {
        //            ScreenText.material = AfvistText;
        //        }

        //        StartCoroutine(ResetLightAndAudio());
        //    }

        //}
    }

    private void OnTriggerExit(Collider other)
    {
       
            StartCoroutine(ResetLightAndAudio()); //venter 3 sekunder f�r standeren er klar igen
       
    }

    //Coroutine som resetter standeren
    private IEnumerator ResetLightAndAudio()
    {
        if (!isCoroutineRunning)
    {
        isCoroutineRunning = true; // Set the flag to indicate the coroutine is running

        yield return new WaitForSeconds(4.0f);

        Blaatlys.enabled = true;
        
        canInteract = true;

        if (ScreenText != null)
        {
            ScreenText.material = originalMaterial;
        }

        isCoroutineRunning = false; // Reset the flag when the coroutine is done
    }

    }

}
