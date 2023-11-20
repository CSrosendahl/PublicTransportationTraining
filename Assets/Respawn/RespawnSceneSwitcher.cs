using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnSceneSwitcher : MonoBehaviour
{
    public string targetSceneName = "Map"; // Specify the target scene name

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider is from the player's hand
        {
            GameManager.instance.SpawnIndgang(); // Load the target scene
        }
    }
}