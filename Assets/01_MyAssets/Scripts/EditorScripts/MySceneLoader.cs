using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Loading next scene");

    }
    public void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ReloadCurrentScene()
    {
        // Get the current scene's index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene using its index
        SceneManager.LoadScene(currentSceneIndex);
    }
}
