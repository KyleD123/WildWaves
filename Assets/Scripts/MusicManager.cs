using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static MusicManager instance;
    public AudioSource audioSource1;
    public AudioSource audioSource2;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    public bool IsMusicPlaying()
    {
        return audioSource1.isPlaying;
    }

    public void PlayerDeadSound()
    {
        audioSource1.Pause();
        audioSource2.Play();
        StartCoroutine("Unpause");
    }

    IEnumerator Unpause()
    {
        yield return new WaitForSeconds(1.5f);
        audioSource1.UnPause();
    }

    public void StartMusic()
    {
        audioSource1.Play();
    }

    public void StopMusic()
    {
        audioSource1.Stop();
    }

}
