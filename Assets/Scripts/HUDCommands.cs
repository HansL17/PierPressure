using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDCommands : MonoBehaviour
{
    public float globalTime = 9000;

    void Awake()
    {
        Time.timeScale = 1f;
    }

    public void ResetScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void PauseScene()
    {
        Time.timeScale = 0f;
        Debug.Log("Scene paused");
    }

    public void ResumeScene()
    {
        Time.timeScale = 1f;
        Debug.Log("Scene resumed");
    }

    public void BackToMain() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("mainMenu");
   }
}