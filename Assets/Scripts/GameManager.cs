using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public GameObject retryBtn;
    public GameObject quitBtn;
    public GameObject gameOverBox;
    public TMP_Text waveText;
    public TMP_Text gameOverWaveText;
    public TMP_Text bulletText;
    public GameObject nextWaveImage;

    public GameObject makeImage;
    public GameObject itImage;
    public GameObject countImage;
    public GameObject inputImage;

    private int waveRecord;


    // Start is called before the first frame update
    void Start()
    {
        if(!MusicManager.instance.IsMusicPlaying())
            MusicManager.instance.StartMusic();
        waveText.text = "Wave";
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerDead += OnPlayerDead;
        waveSpawner.WaveChange += ws_OnWaveChange;
        StartCoroutine("makeItCount");
    }

    public void ws_OnWaveChange(object sender, EventArgs e)
    {
        StartCoroutine("StartNextWave");
    }

    IEnumerator makeItCount()
    {
        inputImage.SetActive(true);
        makeImage.SetActive(true);
        yield return new WaitForSeconds(1);
        makeImage.SetActive(false);
        itImage.SetActive(true);
        yield return new WaitForSeconds(1);
        itImage.SetActive(false);
        countImage.SetActive(true);
        yield return new WaitForSeconds(1);
        countImage.SetActive(false);
        inputImage.SetActive(false);
    }

    IEnumerator StartNextWave()
    {
        if (waveSpawner.currWave > 1)
        { 
            waveText.text = "Wave";
            nextWaveImage.SetActive(true);
        }
        yield return new WaitForSeconds(3);
        nextWaveImage.SetActive(false);
        Debug.Log("Wave Started From GM");
        waveSpawner.GenerateWave();
        waveText.text = "Wave  " + waveSpawner.currWave;
        waveSpawner.waveState = WaveSpawner.WaveState.Active;
    }

    public void OnPlayerDead(object sender, EventArgs e)
    {
        MusicManager.instance.PlayerDeadSound();
        retryBtn.SetActive(true);
        quitBtn.SetActive(true);
        gameOverBox.SetActive(true);
        gameOverWaveText.gameObject.SetActive(true);
        gameOverWaveText.text = waveText.text;
        waveText.gameObject.SetActive(false);
    }

    public void Retry()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void Quit()
    {
        MusicManager.instance.StopMusic();
        SceneManager.LoadScene("Title");
    }

    // Update is called once per frame
    void Update()
    {
        if(waveSpawner.waveState == WaveSpawner.WaveState.Active)
        {
            bulletText.text = "" + GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon>().numShots;
        }
    }
}
