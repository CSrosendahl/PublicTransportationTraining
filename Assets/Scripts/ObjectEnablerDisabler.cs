using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEnablerDisabler : MonoBehaviour
{

    public GameObject[] objectsToEnable;
    public GameObject[] objectsToDisable;
  

    public void DisableObjects()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
    }
    public void EnableObjects()
    {
        foreach (GameObject obj in objectsToEnable)
        {
            obj.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            EnableObjects();
            DisableObjects();
        }
    }
}
