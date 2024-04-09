using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
     
    }

    public AudioClip[] audioClips;
    public List<AudioSource> targetAudioSources;




    public AudioSource allAroundAudioSource;
   

    public AudioClip openDoorSound;
    public AudioClip trainMoveSound;
    public AudioClip trainStandStillSound;
    public float waitTime = 60f;

    private void Start()
    {
        StartCoroutine(PlayRandomAudioEveryMinute());
    }

    IEnumerator PlayRandomAudioEveryMinute()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            // Choose a random index from the array
            int randomIndex = Random.Range(0, audioClips.Length);
            Debug.Log($"Randomly selected index: {randomIndex}");

            // Play the chosen audio clip on the specified AudioSources
            foreach (var audioSource in targetAudioSources)
            {
                if (audioSource != null)
                {
                    audioSource.clip = audioClips[randomIndex];
                    audioSource.Play();
                    Debug.Log($"Playing on {audioSource.gameObject.name}");
                }
                else
                {
                    Debug.LogWarning("One of the specified AudioSources is null. Check the references in the Unity Editor.");
                }
            }
        }
    }

    public void PlayAudioClip(AudioClip audioClip)
    {
        if (allAroundAudioSource.isPlaying)
        {
            allAroundAudioSource.Stop();
        }
        allAroundAudioSource.clip = audioClip;

        allAroundAudioSource.Play();
        

    }


}