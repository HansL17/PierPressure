using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDCommands : MonoBehaviour
{
    public float globalTime = 9000;
    public ScoreTally tally;
    public Animator popup = null;

    void Awake()
    {
        tally = GameObject.Find("ScoreUpgradeTally").GetComponent<ScoreTally>();
        if (tally.LvlCompCount != 2)
        {
        popup = GameObject.Find("Popups").GetComponent<Animator>();
        }
        Time.timeScale = 1f;
    }

    void Update(){
        if (tally.LvlCompCount !=2)
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