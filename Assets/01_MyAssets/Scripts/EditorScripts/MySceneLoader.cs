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

    public void MainMenuLoadNextScene()
    {
        fadePlayer.StartFadeToBlack();
        Invoke("MenuLoadNext", 5f);

    }
    public void LoadFirstScene()
    {

        SceneManager.LoadScene(0); // Load scene by index

    }

    public void ReloadCurrentScene()
    {
        fadePlayer.StartFadeToBlack();
        Invoke("LoadCurrentScene", 5f);
    }

    public void LoadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene using its index
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void MenuLoadNext()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
