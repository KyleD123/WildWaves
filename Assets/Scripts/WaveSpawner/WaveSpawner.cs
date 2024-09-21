using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum WaveState
    {
        Active,
        Inactive
    }

    public WaveState waveState;

    public List<Enemy> enemies = new List<Enemy>();
    public int currWave;
    public int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    [SerializeField]
    public Transform[] spawnPoints;
    private int spawnIndex;

    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    public List<GameObject> spawnedEnemies = new List<GameObject>();

    public event EventHandler WaveChange;

    // Start is called before the first frame update
    void Start()
    {
        //GenerateWave();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerDead += OnPlayerDead;
    }

    public void OnPlayerDead(object sender, EventArgs e)
    {
        waveState = WaveState.Inactive;
    }

    void FixedUpdate()
    {
        if (waveState != WaveState.Active) return;

        if(spawnTimer <= 0)
        {
            if (enemiesToSpawn.Count > 0)
            {
                GameObject enemy = Instantiate(enemiesToSpawn[0], spawnPoints[spawnIndex].position, Quaternion.identity);
                enemiesToSpawn.RemoveAt(0);
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;
                spawnIndex = (spawnIndex + 1) % spawnPoints.Length;

            }
            else
            {
                waveTimer = 0; // end of wave
            }
        }
        else
        {
            spawnTimer -= Time.deltaTime;
            waveTimer -= Time.deltaTime;
        }

        if(waveTimer <= 0 && spawnedEnemies.Count <= 0)
        {
            currWave++;
            waveState = WaveState.Inactive;
            WaveChange?.Invoke(this, EventArgs.Empty);
            //GenerateWave();
        }
    }

    public void GenerateWave()
    {
        waveValue = currWave * 10;

        GenerateEnemies();

        spawnInterval = waveDuration / (float)enemiesToSpawn.Count;
        waveTimer = waveDuration;

    }

    public void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while(waveValue > 0 || generatedEnemies.Count < 50)
        {
            int randEnemyId = UnityEngine.Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;
            if(waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].prefab);
                waveValue -= randEnemyCost;
            }
            else if(waveValue <= 0)
            {
                break;
            }
        }
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;

        GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon>().SetNumShots(enemiesToSpawn.Count);
    }

}

[System.Serializable]
public class Enemy
{
    public GameObject prefab;
    public int cost;
}
