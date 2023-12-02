using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDCommands : MonoBehaviour
{
    public float globalTime = 9000;
    public ScoreTally tally;
    public Animator popup;
    public Material newSkyboxMaterial;
    public string activeSceneName;


    void Awake()
    {
        activeSceneName = Application.loadedLevelName;
        RenderSettings.skybox = newSkyboxMaterial;
        
        tally = GameObject.Find("ScoreUpgradeTally").GetComponent<ScoreTally>();
        if (activeSceneName != "Level3" || activeSceneName != "Level5" || activeSceneName != "Prologue" || activeSceneName != "Ending1" || activeSceneName != "Ending2")
        {
        popup = GameObject.Find("Popups").GetComponent<Animator>();
        }
        Time.timeScale = 1f;
    }

    void Update(){
        if (activeSceneName != "Level3" || activeSceneName != "Level5" || activeSceneName != "Prologue" || activeSceneName != "Ending1" || activeSceneName != "Ending2")
        {
        if (popup.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
        PauseScene();
        }
        }
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