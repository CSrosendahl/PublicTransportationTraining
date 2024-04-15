using UnityEngine;
using System.Collections;

public class TrainSoundController : MonoBehaviour
{
    //public AudioSource movingSoundSource;
    //public AudioSource acceleratingSoundSource;
    //public AudioSource deceleratingSoundSource;

    //private Vector3 lastPosition;
    //private Vector3 lastVelocity;
    //private float smoothedAcceleration;

    //public float accelerationSmoothing = 0.1f;
    //public float soundFadeSpeed = 2f;
    //public float cooldownDuration = 1f; // Adjust this value based on your needs
    //private bool isCooldownActive = false;

    //private void Start()
    //{
    //    lastPosition = transform.position;
    //    lastVelocity = Vector3.zero;
    //    smoothedAcceleration = 0f;
    //    PlayMovingSound();
    //}

    //private void Update()
    //{
    //    Vector3 currentPosition = transform.position;
    //    Vector3 currentVelocity = (currentPosition - lastPosition) / Time.deltaTime;

    //    if (!isCooldownActive)
    //    {
    //        if (currentVelocity.magnitude > 0.1f)
    //        {
    //            float acceleration = Vector3.Dot(currentVelocity.normalized, currentVelocity - lastVelocity);

    //            smoothedAcceleration = Mathf.Lerp(smoothedAcceleration, acceleration, accelerationSmoothing);

    //            if (smoothedAcceleration > 0.1f)
    //            {
    //                Debug.Log("Train is accelerating");
    //                StartCoroutine(TriggerSoundWithCooldown(acceleratingSoundSource));
    //            }
    //            else if (smoothedAcceleration < -0.1f)
    //            {
    //                Debug.Log("Train is decelerating");
    //                StartCoroutine(TriggerSoundWithCooldown(deceleratingSoundSource));
    //            }
    //            else
    //            {
    //                Debug.Log("Train is moving at a constant speed");
    //                StartCoroutine(TriggerSoundWithCooldown(movingSoundSource));
    //            }
    //        }
    //        else
    //        {
    //            Debug.Log("Train is standing still");
    //            StartCoroutine(TriggerSoundWithCooldown(deceleratingSoundSource));
    //        }
    //    }

    //    lastPosition = currentPosition;
    //    lastVelocity = currentVelocity;
    //}

    //private IEnumerator TriggerSoundWithCooldown(AudioSource targetSound)
    //{
    //    isCooldownActive = true;

    //    FadeToSound(targetSound);

    //    yield return new WaitForSeconds(cooldownDuration);

    //    isCooldownActive = false;
    //}

    //private void FadeToSound(AudioSource targetSound)
    //{
    //    StopAllCoroutines();
    //    StartCoroutine(FadeSound(targetSound));
    //}

    //private IEnumerator FadeSound(AudioSource targetSound)
    //{
    //    while (targetSound.volume > 0f)
    //    {
    //        targetSound.volume -= Time.deltaTime * soundFadeSpeed;
    //        yield return null;
    //    }

    //    StopAllSounds();
    //    targetSound.volume = 1f;
    //    targetSound.Play();
    //}

    //private void StopAllSounds()
    //{
    //    movingSoundSource.Stop();
    //    acceleratingSoundSource.Stop();
    //    deceleratingSoundSource.Stop();
    //}

    //private void PlayMovingSound()
    //{
    //    StopAllSounds();
    //    movingSoundSource.Play();
    //}
}