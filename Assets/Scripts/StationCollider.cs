using UnityEngine;
using System;

public class StationCollider : MonoBehaviour
{
    public static event Action OnTrainEnterStation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Train"))
        {
            OnTrainEnterStation?.Invoke();
        }
    }
}
