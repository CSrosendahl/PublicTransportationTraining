using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInd : MonoBehaviour
{

    public bool hasInteracted;

    

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding with " + collision.gameObject.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision Exit " + collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by " + other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit " + other.gameObject.name);
    }

}
