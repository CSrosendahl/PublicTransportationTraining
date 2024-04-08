using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioVolumeController : MonoBehaviour
{
    public AudioMixer audioMixer; // Reference to the Audio Mixer
    public AudioMixerSnapshot muteSnapshot; // Reference to the "Train Off" snapshot
    public AudioMixerSnapshot unmuteSnapshot; // Reference to the "All On" snapshot

    public BoxCollider boxCollider1;
    public BoxCollider boxCollider2;
    public BoxCollider boxCollider3;

    public AudioSource audioSource1;
    public AudioSource audioSource2;

    public float minVolume = 0.1f; // Adjust this value to set the minimum volume

    void Update()
    {
        if (boxCollider1 == null || boxCollider2 == null || boxCollider3 == null || audioSource1 == null || audioSource2 == null || audioMixer == null || muteSnapshot == null || unmuteSnapshot == null)
        {
            Debug.LogWarning("Please assign boxCollider1, boxCollider2, boxCollider3, audioSource1, audioSource2, audioMixer, muteSnapshot, and unmuteSnapshot in the Unity Editor.");
            return;
        }

        Camera mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogWarning("Main camera not found!");
            return;
        }

        bool insideCollider1 = boxCollider1.bounds.Contains(mainCamera.transform.position);
        bool insideCollider2 = boxCollider2.bounds.Contains(mainCamera.transform.position);
        bool insideCollider3 = boxCollider3.bounds.Contains(mainCamera.transform.position);

        // Adjust the volume and snapshots based on proximity
        AdjustVolumeAndSnapshots(insideCollider1, insideCollider2, insideCollider3);
       // Debug.Log(mainCamera.transform.position);
    }

    void AdjustVolumeAndSnapshots(bool insideCollider1, bool insideCollider2, bool insideCollider3)
    {
        if (insideCollider1 && !insideCollider2 && !insideCollider3)
        {
            // Player is inside collider 1, transition to the mute snapshot
            muteSnapshot.TransitionTo(0.1f); // Adjust transition time as needed
            audioSource1.volume = 1.0f;
            audioSource2.volume = 0.0f;
            Debug.Log("Muting the train");
        }
        else if (insideCollider2 && !insideCollider1 && !insideCollider3)
        {
            // Player is inside collider 2, transition to the unmute snapshot
            unmuteSnapshot.TransitionTo(0.1f); // Adjust transition time as needed
            audioSource1.volume = 0.0f;
            audioSource2.volume = 1.0f;
            //Debug.Log("Unmuting the train");
        }
        else if (!insideCollider1 && !insideCollider2 && insideCollider3)
        {
            // Player is inside collider 3, transition to the mute snapshot
            muteSnapshot.TransitionTo(0.1f); // Adjust transition time as needed
            audioSource1.volume = 1.0f;
            audioSource2.volume = 0.0f;
            Debug.Log("Muting the train");
        }
        else
        {
            // Player is outside all colliders, adjust volume based on proximity
            float distanceToCollider1 = Vector3.Distance(Camera.main.transform.position, boxCollider1.bounds.ClosestPoint(Camera.main.transform.position));
            float distanceToCollider2 = Vector3.Distance(Camera.main.transform.position, boxCollider2.bounds.ClosestPoint(Camera.main.transform.position));
            float maxDistance = Mathf.Max(distanceToCollider1, distanceToCollider2);

            // Calculate normalized volume based on proximity with a minimum volume
            float volume1 = Mathf.Clamp01(1f - (distanceToCollider1 / maxDistance)) * (1 - minVolume) + minVolume;
            float volume2 = Mathf.Clamp01(1f - (distanceToCollider2 / maxDistance)) * (1 - minVolume) + minVolume;

            // Set the volume of the audio sources based on proximity
            audioSource1.volume = volume1;
            audioSource2.volume = volume2;

            // Transition to the unmute snapshot
            unmuteSnapshot.TransitionTo(0.1f); // Adjust transition time as needed
            Debug.Log("Unmuting the train");
        }
    }
}