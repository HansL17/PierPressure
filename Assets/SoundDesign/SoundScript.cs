using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    public AudioSource BGM;
    public AudioSource Customer;
    public AudioSource Clink;
    public AudioSource UpgBGM;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
