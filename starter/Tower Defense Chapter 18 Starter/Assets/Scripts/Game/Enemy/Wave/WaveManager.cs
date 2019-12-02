using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    //1 
    public static WaveManager Instance;
    //2
    public List<EnemyWave> enemyWaves = new List<EnemyWave>();
    //3
    private float elapsedTime = 0f;
    //4 
    private EnemyWave activeWave;
    //5
    private float spawnCounter = 0f;
    //6
    private List<EnemyWave> activatedWaves = new List<EnemyWave>();

    //1 
    void Awake()
    {
        Instance = this;
    }
    
    //2 
    void Update()
    {
         elapsedTime += Time.deltaTime; 
         SearchForWave();
         UpdateActiveWave();
    }

    private void SearchForWave()
    {
        foreach (EnemyWave enemyWave in enemyWaves)
        {
            if (!activatedWaves.Contains(enemyWave)        && enemyWave.startSpawnTimeInSeconds <= elapsedTime)
            {
                activeWave = enemyWave;
                activatedWaves.Add(enemyWave);
                spawnCounter = 0f;
                GameManager.Instance.waveNumber++;
                break;
            }
        }
    }

    private void UpdateActiveWave()
    {
        if (activeWave != null)
        {
            spawnCounter += Time.deltaTime;
            if (spawnCounter >= activeWave.timeBetweenSpawnsInSeconds)
            {
                spawnCounter = 0f; 
                if (activeWave.listOfEnemies.Count != 0)
                {
                    GameObject enemy = (GameObject)Instantiate(activeWave.listOfEnemies[0], WayPointManager.Instance.GetSpawnPosition(activeWave.pathIndex), 
                        Quaternion.identity);    
                    enemy.GetComponent<Enemy>().pathIndex = activeWave.pathIndex; 
                    activeWave.listOfEnemies.RemoveAt(0);
                }
                else
                {
                    activeWave = null; 
                    if (activatedWaves.Count == enemyWaves.Count)
                    {
                        GameManager.Instance.enemySpawningOver = true;
                    }
                }
            }
        }

    }

    public void StopSpawning()
    {
        elapsedTime = 0;
        spawnCounter = 0;
        activeWave = null;
        activatedWaves.Clear();
        enabled = false;
    }
}
