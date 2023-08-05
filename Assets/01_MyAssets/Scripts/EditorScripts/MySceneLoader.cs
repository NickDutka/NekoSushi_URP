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
}
