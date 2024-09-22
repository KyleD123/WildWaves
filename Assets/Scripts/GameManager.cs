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

    private int waveRecord;


    // Start is called before the first frame update
    void Start()
    {
        if(!MusicManager.instance.IsMusicPlaying())
            MusicManager.instance.StartMusic();
        waveText.text = "Wave  Starting!";
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerDead += OnPlayerDead;
        waveSpawner.WaveChange += ws_OnWaveChange;
        StartCoroutine("StartNextWave");
    }

    public void ws_OnWaveChange(object sender, EventArgs e)
    {
        StartCoroutine("StartNextWave");
    }

    IEnumerator StartNextWave()
    {
        if(waveSpawner.currWave > 1)
            waveText.text = "Wave";
        yield return new WaitForSeconds(3);
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
