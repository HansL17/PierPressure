using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundScript : MonoBehaviour
{
    [SerializeField] private AudioMixer music;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    public AudioSource BGM;
    public AudioSource Customer;
    public AudioSource Clink;
    public AudioSource UpgBGM;
    private Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "Prologue" || currentScene.name != "Ending1" || currentScene.name != "Ending2")
        {
            if (PlayerPrefs.HasKey("musicVolume"))
            {
                LoadVolume();
            }
            else{
            SetMusicVolume();
            SetSoundVolume();
            }
        }
    }

    public void StopMusic()
    {
        BGM.Stop();
    }

    public void CustomerSound()
    {
        Customer.Play();
    }

    public void DishSound()
    {
        Clink.Play();
    }

    public void PlayBGM()
    {
        BGM.Play();
        BGM.loop = true;
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        music.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSoundVolume()
    {
        if (currentScene.name != "Prologue" || currentScene.name != "Ending1" || currentScene.name != "Ending2")
        {
        float volume = soundSlider.value;
        music.SetFloat("sound", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("soundVolume", volume);
        }
    }

    public void LoadVolume()
    {
        if (currentScene.name != "Prologue" || currentScene.name != "Ending1" || currentScene.name != "Ending2")
        {musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume");
        SetMusicVolume();
        SetSoundVolume();
        }
    }
}
