using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    public string targetSceneName = "Map"; // The scene to load after ending the game (optional)

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player")) // Check if the collider is from the player's hand
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        // Add any necessary game-ending logic here (e.g., display a game-over UI, quit the application, etc.)
        Debug.Log("Game Ended!");

        // Optionally, load a specific scene after ending the game
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            // Or quit the application (Note: This won't work in the Unity Editor)
            Application.Quit();
        }
    }
}