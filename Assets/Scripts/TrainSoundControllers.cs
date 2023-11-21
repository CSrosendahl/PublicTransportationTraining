using UnityEngine;

public class VelocitySoundController : MonoBehaviour
{
    public AudioSource movingSound;
    public AudioSource notMovingSound;

    private Transform parentObject;
    private Vector3 lastPosition;
    private float velocityThreshold = 0.1f; // Adjust the threshold as needed
    private float fadeDuration = 1.0f; // Adjust the fade duration as needed

    private bool isMoving;
    void Start()
    {
        // Assuming the parent object is the immediate parent of the train prefab
        parentObject = transform.parent;
        lastPosition = parentObject.position;
        isMoving = false;
    }

    void Update()
    {
        // Calculate current velocity
        Vector3 displacement = parentObject.position - lastPosition;
        float currentVelocity = Vector3.Dot(displacement, parentObject.forward) / Time.deltaTime;

        // Determine whether the object is moving
        bool isMovingNow = Mathf.Abs(currentVelocity) > velocityThreshold;

        // Handle state transitions
        if (isMovingNow != isMoving)
        {
            isMoving = isMovingNow;

            if (isMoving)
            {
                // Start fading in the moving sound
                StartCoroutine(FadeIn(movingSound, fadeDuration));
                StartCoroutine(FadeOut(notMovingSound, fadeDuration));
              //  Debug.Log("Object is moving - Velocity: " + currentVelocity);
            }
            else
            {
                // Start fading in the not moving sound
                StartCoroutine(FadeIn(notMovingSound, fadeDuration));
                StartCoroutine(FadeOut(movingSound, fadeDuration));
               // Debug.Log("Object is not moving - Velocity: " + currentVelocity);
            }
        }

        // Update last position
        lastPosition = parentObject.position;
    }

    System.Collections.IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        float timer = 0f;
        audioSource.volume = 0f;
        audioSource.Play();

        while (timer < duration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, 1f, timer / duration);
            yield return null;
        }
    }

    System.Collections.IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(1f, 0f, timer / duration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = 1f; // Reset volume to avoid audio glitches
    }
}