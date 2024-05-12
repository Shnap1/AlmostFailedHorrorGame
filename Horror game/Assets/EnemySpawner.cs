using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public PatrolPointManager patrolPointsManager;
    public void SpawnEnemies(int enemiesToSpawn)
    {

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 spawnPoint = patrolPointsManager.GetEmptyPointsToSpawn();
            Instantiate(enemy, spawnPoint, Quaternion.identity);
        }
    }
}
