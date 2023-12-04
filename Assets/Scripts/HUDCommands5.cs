using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDCommands5 : MonoBehaviour
{
    public float globalTime = 9000;
    public ScoreTally tally;
    public string activeSceneName;


    void Awake()
    {
        activeSceneName = Application.loadedLevelName;

        tally = GameObject.Find("ScoreUpgradeTally").GetComponent<ScoreTally>();
        Time.timeScale = 1f;
    }

    void Update()
    {

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