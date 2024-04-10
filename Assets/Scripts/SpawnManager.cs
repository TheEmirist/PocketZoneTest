using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();
    public GameObject enemyPrefab;
    public int numberOfEnemies = 5;

    void Awake()
    {
        Spawn();
    }

    void Spawn()
    {
        if (spawnPoints.Count >= numberOfEnemies && enemyPrefab != null)
        {
            List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

            for (int i = 0; i < numberOfEnemies; i++)
            {
                int randomIndex = Random.Range(0, availableSpawnPoints.Count);
                Transform spawnPoint = availableSpawnPoints[randomIndex];

                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

                availableSpawnPoints.RemoveAt(randomIndex);
            }
        }
        else
        {
            Debug.LogWarning("Not enough spawn points or enemy prefab not set!");
        }
    }
}
