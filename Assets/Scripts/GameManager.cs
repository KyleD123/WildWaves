using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public WaveSpawner waveSpawner;

    // Start is called before the first frame update
    void Start()
    {
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
        waveSpawner.waveState = WaveSpawner.WaveState.Active;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
