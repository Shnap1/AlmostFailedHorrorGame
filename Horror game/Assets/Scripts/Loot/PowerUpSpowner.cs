using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static LootSpawner;

public class LootSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] public GameObject staminaPowerUp;
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
    }
    void Start()
    {
        //var hp = healthPowerUp.GetComponent<IPowerUp>();
        //hp.DoAction(2);

        //thisPUSpawner = gameObject.GetComponent<PowerUpSpawner>();
        FreeTransformsForSpawn = new List<Transform>(LocationsTrasnforms);
        instance = this;
    }

    // public void RandomSpawnPowerUp()
    // {
    //     if (AlreadySpawnedTransforms.Count == LocationsTrasnforms.Count)
    //     {
    //         AlreadySpawnedTransforms.Clear();
    //     }

    //     GameObject randomGameObject = PowerUpList[UnityEngine.Random.Range(0, PowerUpList.Count)];
    //     Transform randomTransform = LocationsTrasnforms[UnityEngine.Random.Range(0, LocationsTrasnforms.Count)];
    //     if (AlreadySpawnedTransforms.Contains(randomTransform))
    //     {
    //         for (int i = 0; i < AlreadySpawnedTransforms.Count; i++)
    //         {
    //             randomTransform = LocationsTrasnforms[UnityEngine.Random.Range(0, LocationsTrasnforms.Count)];
    //             if (!AlreadySpawnedTransforms.Contains(randomTransform))
    //             {
    //                 Instantiate<GameObject>(randomGameObject, randomTransform);
    //                 AlreadySpawnedTransforms.Add(randomTransform);
    //                 break;
    //             }
    //         }
    //     }
    //     else if (!AlreadySpawnedTransforms.Contains(randomTransform))
    //     {
    //         Debug.Log($" randomTransform = {randomTransform.position.ToString()}");

    //         Instantiate<GameObject>(randomGameObject, randomTransform);
    //         Debug.Log($"{randomGameObject.name} spawned in {randomTransform.position.ToString()}");
    //         AlreadySpawnedTransforms.Add(randomTransform);
    //     }


    // }

    public static void PowerUpSpawn(int amountOfPowerUps, LootType lootType)
    {
        // Debug.Log($"PowerUpSpawn(amountOfPowerUps = {amountOfPowerUps}, LootType = {lootType})");
        for (int i = 0; i < amountOfPowerUps; i++)
        {
            // instance.RandomSpawnPowerUp();
            // Debug.Log($"PowerUpSpawn() LootType - {lootType} i: ({i})");
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
            // Debug.Log($"FreeTransformsForSpawn.Count = {FreeTransformsForSpawn.Count}, so FreeTransformsForSpawn = new List<Transform>(LocationsTrasnforms) ");
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
        // Debug.Log($"Instantiate() lootToSpawn = {lootToSpawn} , randomFreeTransform = {randomFreeTransform}");
        FreeTransformsForSpawn.Remove(randomFreeTransform);
        // Debug.Log($"FreeTransformsForSpawn.Remove(randomFreeTransform = {randomFreeTransform}); FreeTransformsForSpawn.Count = {FreeTransformsForSpawn.Count}");

        //Debug.Log($" randomTransform = {randomFreeTransform.position.ToString()}");
    }

    public void AddPowerUp(GameObject thisPU)
    {
        //var spawner = PowerUpSpowner.ReturnPUSPowner();

        if (!PowerUpList.Contains(thisPU))
        {
            Debug.Log($"{thisPU.name} added to LootList");
            //PowerUpSpowner.ReturnPUSPowner().LootList.Add(thisPU);
            PowerUpList.Add(thisPU);
        }
        //if (!spawner.LootDict.ContainsValue(thisPU))
        //{
        //    Debug.Log($"{thisPU.name} added to LootDict");
        //    PowerUpSpowner.ReturnPUSPowner().LootDict.Add(typeOfLoot, thisPU);
        //}
    }

}

public interface IPowerUp
{
    public void DoAction(int amount, GameObject gameObject);
}
