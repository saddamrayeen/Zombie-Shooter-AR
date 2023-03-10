using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieSpawner : MonoBehaviour
{
    // adding an event
    UnityEvent OnScriptEnable;

    // defining min and maxSpawn time
    [SerializeField]
    float minSpawnTime,
        maxSpawnTime;

    // if the zombies are spawning
    bool isSpawning;

    // assigning all zombiePrefabs
 [SerializeField]   GameObject[] enemyPrefabs;

   [SerializeField] Transform[] spawnPoints;

  private void Start()
  {
      StartSpawning();
  }

    private void StartSpawning()
    {
        // start  spawning
        isSpawning = true;
        StartCoroutine(GenarateEnemies());
    }

    IEnumerator GenarateEnemies()
    {
        while (isSpawning)
        {
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }
}
