using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneManager : MonoBehaviour
{
    public static _SceneManager instance;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        // LoadSceneWithDelay("TrainRide", 5);
    }

    private void Update()
    {
        if(SceneManager.GetSceneByName("TrainTrip").isLoaded)
        {
            Debug.Log("TrainTrip is loaded");
            GameManager.instance.canSpawnTrains = false;
        }
    }

    // Load a scene with a delay
    public void LoadSceneWithDelay(string sceneName, float delay)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName, delay));
    }

    // Coroutine to load scene after a delay
    private IEnumerator LoadSceneCoroutine(string sceneName, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Load the scene
        SceneManager.LoadScene(sceneName);
    }
    // Load a scene by name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Load a scene by index in the build settings
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Load a scene asynchronously by name
    public void LoadSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    // Load a scene asynchronously by index in the build settings
    public void LoadSceneAsync(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }

    // Unload a scene by name
    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    // Unload a scene by index in the build settings
    public void UnloadScene(int sceneIndex)
    {
        SceneManager.UnloadSceneAsync(sceneIndex);
    }

    // Get the current active scene name
    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    // Get the current active scene index
    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
