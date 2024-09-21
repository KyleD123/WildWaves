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
    public TMP_Text waveText;
    public TMP_Text bulletText;

    private int waveRecord;


    // Start is called before the first frame update
    void Start()
    {
        waveText.text = "Wave: Starting!";
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
        yield return new WaitForSeconds(3);
        Debug.Log("Starting next wave");
        waveSpawner.GenerateWave();
        waveText.text = "Wave: " + waveSpawner.currWave;
        waveSpawner.waveState = WaveSpawner.WaveState.Active;
    }

    public void OnPlayerDead(object sender, EventArgs e)
    {
        retryBtn.SetActive(true);
    }

    public void Retry()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
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
