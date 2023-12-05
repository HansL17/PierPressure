using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
     void Awake()
     {
          Time.timeScale = 1f;
     }

   public void ExitButton() {
        Application.Quit();
        Debug.Log("Game Closed");
   }

   public void PlayPrologue()
    {
        SceneManager.LoadScene("Prologue");
    }
   public void StartGame() {
        SceneManager.LoadScene("Tutorial Level");
   }

    public void ToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("mainMenu");
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
}
