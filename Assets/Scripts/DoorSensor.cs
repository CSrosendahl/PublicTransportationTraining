using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSensor : MonoBehaviour
{
    public Animator doorAnimator;
    public bool doorOpen = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered door sensor");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited door sensor");
            StartCoroutine(CloseDoorAfterDelay());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is in door sensor");
            if (!doorOpen)
            {
                doorAnimator.SetBool("Open", true);
                doorOpen = true;
            }
        }
    }

    IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(2);
        doorAnimator.SetBool("Open", false);
        doorOpen = false;
    }

    
}
