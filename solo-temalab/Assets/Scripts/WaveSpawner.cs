using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public EnemyType[] enemies;
        public float spawnRate;
    }

    [System.Serializable]
    public class EnemyType
    {
        public int enemyCount;
        public Transform enemy;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;


    public float timeBetweenWaves = 5f;
    private float waveCountdown = 0f;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points referenced!");
        }

        if (waves.Length == 0)
        {
            Debug.LogError("No waves created!");
        }
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if(!EnemyIsAlive())
            {
                WaveCompleted();
            } else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            } 
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave completed!");
       
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(!(nextWave + 1 > waves.Length - 1))
        {
            nextWave++;
        } else
        {
            Debug.Log("Completed all waves -> INFINITY MODE");
        }   
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
                return false;

            return true;
        }

        return true;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning wave: " + wave.waveName);
        state = SpawnState.SPAWNING;
        for (int i = 0; i < wave.enemies.Length; i++)
        {
            for(int j = 0; j < wave.enemies[i].enemyCount; j++)
            {
                SpawnEnemy(wave.enemies[i].enemy);
                yield return new WaitForSeconds(1f / wave.spawnRate);
            }              
        }
        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Debug.Log("Spawning enemy: " + enemy.name);
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, sp.position, sp.rotation);
    }
}
