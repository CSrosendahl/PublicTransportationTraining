using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceTrigger : MonoBehaviour
{
    
  public Camera mainCamera;
    public Collider ambienceCollider;
    public AudioSource indoorAmbience;
    public AudioSource outdoorAmbience;

    private bool isInsideCollider = false;
    public float transitionSpeed = 1.0f; // Adjust this value to control the transition speed

    void Update()
    {
        // Check if the camera is inside the collider
        isInsideCollider = ambienceCollider.bounds.Contains(mainCamera.transform.position);

        // Trigger ambience based on the camera's location
        if (isInsideCollider)
        {
            // Camera is inside the collider, smoothly transition to indoor ambience
            SmoothTransition(indoorAmbience, transitionSpeed);
            SmoothTransition(outdoorAmbience, 0.0f);
        }
        else
        {
            // Camera is outside the collider, smoothly transition to outdoor ambience
            SmoothTransition(outdoorAmbience, transitionSpeed);
            SmoothTransition(indoorAmbience, 0.0f);
        }
    }

    // Helper method for smooth volume transitions
    void SmoothTransition(AudioSource audioSource, float targetVolume)
    {
        audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, Time.deltaTime * transitionSpeed);
        if (audioSource.volume == 0.0f && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else if (audioSource.volume > 0.0f && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}