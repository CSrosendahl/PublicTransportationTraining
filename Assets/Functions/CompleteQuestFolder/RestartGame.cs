using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Skolekort"))
        {
            GameManager.instance.StartGame();
        }
        Debug.Log("Trigger");
    }
}
