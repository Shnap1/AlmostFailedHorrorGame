using System.Collections;
using System.Collections.Generic;
//using Unity.Entities.UniversalDelegates;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public PatrolPointManager patrolPointsManager;
    public List<GameObject> enemiesLVL1 = new List<GameObject>();
    public List<GameObject> enemiesLVL2 = new List<GameObject>();
    public List<GameObject> enemiesLVL3 = new List<GameObject>();

    public void SpawnEnemies(int numberOfEnemiesToSpawn)
    {

        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Vector3 spawnPoint = patrolPointsManager.GetEmptyRandomPointsToSpawn();
            var s = Instantiate(enemy, spawnPoint, Quaternion.identity);
            s.GetComponent<ZombieStateManager>().enabled = true;

            if (i == 0 || i >= 4)
            {
                s.GetComponent<ZombieStateManager>().SetPatrollingType(Zombie_Patrolling_State.Patrollers.randomPointFollower);
                // Debug.Log("SetPatrollingType(randomPointFollower)");
                // break;
            }
            else if (i == 1)
            {
                s.GetComponent<ZombieStateManager>().SetPatrollingType(Zombie_Patrolling_State.Patrollers.lastTriggeredPointFollower);
                // Debug.Log("SetPatrollingType(lastTriggeredPointFollower)");
                // break;
            }
            else if (i == 3)
            {
                s.GetComponent<ZombieStateManager>().SetPatrollingType(Zombie_Patrolling_State.Patrollers.playerFollower);
                // Debug.Log("SetPatrollingType(playerFollower)");
                // break;
            }
            // s.GetComponent<ZombieStateManager>().SetPatrollingType(Zombie_Patrolling_State.Patrollers.randomPointFollower);

        }
    }

    public void SpawnEnemiesByLevel(int numberOfEnemiesToSpawn, int levelNumber)
    {
        int enemiesSpawned = 0;
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {

            Vector3 spawnPoint = patrolPointsManager.GetEmptyRandomPointsToSpawn();
            List<GameObject> listOfEnemiesToSpawnByLVL = null;
            switch (levelNumber) //Choosing a list of enemies to spawn by level number
            {
                case 1:
                    listOfEnemiesToSpawnByLVL = enemiesLVL1;
                    break;
                case 2:
                    listOfEnemiesToSpawnByLVL = enemiesLVL2;
                    break;
                case 3:
                    listOfEnemiesToSpawnByLVL = enemiesLVL3;
                    break;
                default:
                    listOfEnemiesToSpawnByLVL = enemiesLVL2;
                    break;

            }
            GameObject enemyToSpawn = PickARandomEnemyOfLevel(listOfEnemiesToSpawnByLVL);





            // enemyToSpawn.GetComponent<ZombieStateManager>().SetPatrollingType(Zombie_Patrolling_State.Patrollers.randomPointFollower);
            // Debug.Log("SetPatrollingType(randomPointFollower)");
            // if (i <= 3 || i >= 6)
            // {
            //     enemyToSpawn.GetComponent<ZombieStateManager>().SetPatrollingType(Zombie_Patrolling_State.Patrollers.randomPointFollower);
            //     Debug.Log("SetPatrollingType(randomPointFollower)");
            //     break;
            // }
            // else if (i == 4)
            // {
            //     enemyToSpawn.GetComponent<ZombieStateManager>().SetPatrollingType(Zombie_Patrolling_State.Patrollers.lastTriggeredPointFollower);
            //     Debug.Log("SetPatrollingType(lastTriggeredPointFollower)");
            //     break;
            // }
            // else if (i == 5)
            // {
            //     enemyToSpawn.GetComponent<ZombieStateManager>().SetPatrollingType(Zombie_Patrolling_State.Patrollers.playerFollower);
            //     Debug.Log("SetPatrollingType(playerFollower)");
            //     break;
            // }









            //Setting patrollerType for each enemy. The first 3 have to be set to randomFollower, #4 to lastTriggeredPointFollower and the last #5 to playerFollower/ All above could be randomFollowers
            var test = Instantiate(enemyToSpawn, spawnPoint, Quaternion.identity);
            if (i <= 3 || i >= 6)
            {
                test.GetComponent<ZombieStateManager>().SetPatrollingType(Zombie_Patrolling_State.Patrollers.randomPointFollower);
                Debug.Log("SetPatrollingType(randomPointFollower)");
                break;
            }
            else if (i == 4)
            {
                test.GetComponent<ZombieStateManager>().SetPatrollingType(Zombie_Patrolling_State.Patrollers.lastTriggeredPointFollower);
                Debug.Log("SetPatrollingType(lastTriggeredPointFollower)");
                break;
            }
            else if (i == 5)
            {
                test.GetComponent<ZombieStateManager>().SetPatrollingType(Zombie_Patrolling_State.Patrollers.playerFollower);
                Debug.Log("SetPatrollingType(playerFollower)");
                break;
            }

            //TODO also add code to also Instantiate 2 enemies above the current level

            enemiesSpawned++;
        }

        if (levelNumber + 1 <= 3 && enemiesSpawned <= numberOfEnemiesToSpawn + 2)
        {
            SpawnEnemiesByLevel(2, levelNumber + 1); //Added +2 to ensure there will be at least 2 enemies above the current level spawned
        }
        else if (levelNumber + 1 > 3 && enemiesSpawned <= numberOfEnemiesToSpawn + 2)
        {
            SpawnEnemiesByLevel(2, levelNumber);
        }
    }

    GameObject PickARandomEnemyOfLevel(List<GameObject> listByLevel)
    {
        GameObject enemyToSpawn = null;

        switch (listByLevel.Count)
        {
            case 0: //Doing it to prevent error when list is empty
                Debug.Log($"List called {nameof(listByLevel)} - is empty. No enemy to spawn");
                break;
            case 1: //Doing it to prevent error when the range is 0 to 0 - will cause an out of bounds error
                enemyToSpawn = listByLevel[0];
                break;
            default:
                enemyToSpawn = listByLevel[Random.Range(0, listByLevel.Count)];
                break;
        }
        return enemyToSpawn;
    }
}
