using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementDotScript : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        RejseStatus rejseStatusScript = other.GetComponent<RejseStatus>();

        if(other.CompareTag("Train"))
        {
            if (rejseStatusScript != null)
            {
                Debug.Log("Triggered");
                rejseStatusScript.DecrementDot(rejseStatusScript.startBlinkingFromIndex);
            }
        }
    }
}
