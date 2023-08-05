using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneLoader : MonoBehaviour
{

    public FadePlayer fadePlayer;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("Loading next scene");

    }
    public void LoadFirstScene()
    {
        fadePlayer.StartFadeToBlack();

        Invoke("LoadNextScene", 5f);
    }

    public void ReloadCurrentScene()
    {
        // Get the current scene's index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene using its index
        SceneManager.LoadScene(currentSceneIndex);
    }
}
