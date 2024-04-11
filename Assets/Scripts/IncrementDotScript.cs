using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementDotScript : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {

        RejseStatus[] rejseStatusScripts = other.GetComponentsInChildren<RejseStatus>();

        if(other.CompareTag("Train"))
        {
            if (rejseStatusScripts != null)
            {
                Debug.Log("Triggered");
                foreach (var rejseStatusScript in rejseStatusScripts)
                {
                    rejseStatusScript.DecrementDot(rejseStatusScript.currentDotIndex);
     
                }
              
            }
        }
    }
}
