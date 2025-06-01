using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static LootSpawner;

public class LootSpawner : MonoBehaviour
{
    //public Dictionary<Loot, GameObject> LootDict = new Dictionary<Loot, GameObject>();
    public List<GameObject> PowerUpList = new List<GameObject>();
    //public Dictionary<Locations, Transform> LocationsDict = new Dictionary<Locations, Transform>();

    public List<GameObject> TargetList = new List<GameObject>();
    public List<Transform> LocationsTrasnforms = new List<Transform>();

    public List<Transform> FreeTransformsForSpawn = new List<Transform>();


    public static LootSpawner instance;
    public enum LootType
    {
        healthPU,
        speedPU,
        jumpHeightPU,
        staminaPU,
        defensePU,
        target
    }

    public enum Locations
    {
        gasStation,

    }
    void Awake()
    {
        if (GameData.instance != null)
        {
            GameData.instance.lootSpawner = this;
        }
        instance = this;

    }
    void Start()
    {

        // instance = this;
    }

    public void SetFreeTransPoints()
    {
        FreeTransformsForSpawn = new List<Transform>(LocationsTrasnforms);
    }
    public static void PowerUpSpawn(int amountOfPowerUps, LootType lootType)
    {
        for (int i = 0; i < amountOfPowerUps; i++)
        {
            instance.Spawn_Specific_LOOT_in_Random_Place(lootType);
        }
    }



    public void SpawnPowerUp(GameObject loot, Transform transform)
    {
        Instantiate(loot, transform);
    }

    public void Spawn_Specific_LOOT_in_Random_Place(LootType typeOfLoot)
    {
        Transform randomFreeTransform;
        GameObject lootToSpawn = null;

        if (FreeTransformsForSpawn.Count <= 0)
        {
            FreeTransformsForSpawn = new List<Transform>(LocationsTrasnforms);
        }

        switch (typeOfLoot)
        {
            case LootType.healthPU:
                lootToSpawn = PowerUpList[UnityEngine.Random.Range(0, PowerUpList.Count)];
                break;
            case LootType.staminaPU:
                //SpawnPowerUp(healthPowerUp, LocationsTrasnforms[1]);
                break;
            case LootType.defensePU:
                //SpawnPowerUp(healthPowerUp, LocationsTrasnforms[2]);
                break;
            case LootType.target:
                lootToSpawn = TargetList[UnityEngine.Random.Range(0, TargetList.Count)];
                break;
            default:
                break;
        }
        randomFreeTransform = FreeTransformsForSpawn[UnityEngine.Random.Range(0, FreeTransformsForSpawn.Count)];
        Instantiate(lootToSpawn, randomFreeTransform);
        FreeTransformsForSpawn.Remove(randomFreeTransform);
    }

    public void AddPowerUp(GameObject thisPU)
    {
        if (!PowerUpList.Contains(thisPU))
        {
            Debug.Log($"{thisPU.name} added to LootList");
            //PowerUpSpowner.ReturnPUSPowner().LootList.Add(thisPU);
            PowerUpList.Add(thisPU);
        }

    }

}

public interface IPowerUp
{
    public void DoAction(int amount, GameObject gameObject);
}
