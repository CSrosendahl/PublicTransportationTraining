using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeController : MonoBehaviour
{
public BoxCollider boxCollider1;
    public BoxCollider boxCollider2;
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    public float minVolume = 0.1f; // Adjust this value to set the minimum volume

    void Update()
    {
        if (boxCollider1 == null || boxCollider2 == null || audioSource1 == null || audioSource2 == null)
        {
            Debug.LogWarning("Please assign boxCollider1, boxCollider2, audioSource1, and audioSource2 in the Unity Editor.");
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

        // Adjust the volume of both audio sources based on proximity
        AdjustVolumeBasedOnProximity(insideCollider1, insideCollider2);
    }

    void AdjustVolumeBasedOnProximity(bool insideCollider1, bool insideCollider2)
    {
        if (insideCollider1 && !insideCollider2)
        {
            // Player is inside collider 1, make audioSource1 audible and audioSource2 inaudible
            audioSource1.volume = 1.0f;
            audioSource2.volume = 0.0f;
        }
        else if (insideCollider2 && !insideCollider1)
        {
            // Player is inside collider 2, make audioSource2 audible and audioSource1 inaudible
            audioSource1.volume = 0.0f;
            audioSource2.volume = 1.0f;
        }
        else
        {
            // Player is outside both colliders, adjust volume based on proximity with a minimum volume
            float distanceToCollider1 = Vector3.Distance(Camera.main.transform.position, boxCollider1.bounds.ClosestPoint(Camera.main.transform.position));
            float distanceToCollider2 = Vector3.Distance(Camera.main.transform.position, boxCollider2.bounds.ClosestPoint(Camera.main.transform.position));
            float maxDistance = Mathf.Max(distanceToCollider1, distanceToCollider2);

            // Calculate normalized volume based on proximity with a minimum volume
            float volume1 = Mathf.Clamp01(1f - (distanceToCollider1 / maxDistance)) * (1 - minVolume) + minVolume;
            float volume2 = Mathf.Clamp01(1f - (distanceToCollider2 / maxDistance)) * (1 - minVolume) + minVolume;

            // Set the volume of the audio sources based on proximity
            audioSource1.volume = volume1;
            audioSource2.volume = volume2;
        }
    }
}