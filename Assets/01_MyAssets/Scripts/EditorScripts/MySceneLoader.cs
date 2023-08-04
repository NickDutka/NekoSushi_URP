using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene by incrementing the current scene index
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if the next scene index is within the valid range
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // If there is no next scene, you can choose to do something else, like looping back to the first scene or quitting the application.
            Debug.LogWarning("No next scene found.");
        }
    }
}
