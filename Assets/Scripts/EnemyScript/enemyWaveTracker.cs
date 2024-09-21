using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWaveTracker : MonoBehaviour
{

    public void OnDestroy()
    {
        if(GameObject.FindGameObjectWithTag("WaveSpawner") != null)
        {
            Debug.Log("enemy Destroyed");
            GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>().spawnedEnemies.Remove(gameObject);
        }
    }
}
