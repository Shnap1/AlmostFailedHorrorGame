using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventBusSO", menuName = "EventBusSO", order = 0)]
public class EventBusSO : ScriptableObject
{
    [Tooltip("ADD SELF to LISTS")]
    public Action<GameObject> addMeToPowerUpList;
    public Action<GameObject> addMeToLootList;
    public Action<GameObject> addMeToBuildingList;
    public Action<GameObject> addMeToSpawnPointList;
    public Action<GameObject> addMeToEnemyList;


    [Tooltip("SPAWN")]
    public Action<GameObject, int> spawnXObjectXtimes;
    public Action<GameObject, GameObject, int> replaceGameObjectWithGameObjectXtimes;
    public Action<int> spawnXPowerUps;
    // public Action<int, PUType> spawnXPowerUps;
    public Action<int> spawnXEnemies;
    // public Action<int, EnemyType> spawnXPowerUps;
    public Action<GameObject> getRandomSpawnPoint;
    public Action<GameObject> getTriggeredByPlayerSpawnPoint;


}

