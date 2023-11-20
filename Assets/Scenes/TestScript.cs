using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    public string targetSceneName = "Map"; // Specify the target scene name

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the collider is from the player's hand
        {
            SceneManager.LoadScene(targetSceneName); // Load the target scene
        }
    }
}