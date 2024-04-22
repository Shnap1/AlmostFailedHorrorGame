using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static PowerUpSpawner;

public class PowerUpSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject healthPowerUp;
    //[SerializeField] public GameObject staminaPowerUp;
    //public Dictionary<Loot, GameObject> LootDict = new Dictionary<Loot, GameObject>();
    public List<GameObject> LootList = new List<GameObject>();
    //public Dictionary<Locations, Transform> LocationsDict = new Dictionary<Locations, Transform>();

    public List<GameObject> TargetCollectable = new List<GameObject>();
    public List<Transform> LocationsTrasnforms = new List<Transform>();

    public List<Transform> AlreadySpawnedTransforms = new List<Transform>();


    public static PowerUpSpawner thisPUSpawner;
    public enum LootType
    {
        healthPU,
        staminaPU,
        defensePU,
        targetCollectable
    }

    public enum Locations
    {
        gasStation,

    }

    void Start()
    {
        //var hp = healthPowerUp.GetComponent<IPowerUp>();
        //hp.DoAction(2);
        thisPUSpawner = gameObject.GetComponent<PowerUpSpawner>();
        //thisPUSpawner = this;
    }

    public void RandomSpawnPowerUp()
    {
        if(AlreadySpawnedTransforms.Count == LocationsTrasnforms.Count)
        {
            AlreadySpawnedTransforms.Clear();
        }

        GameObject randomGameObject = LootList[UnityEngine.Random.Range(0, LootList.Count)];
        Transform randomTransform = LocationsTrasnforms[UnityEngine.Random.Range(0, LocationsTrasnforms.Count)];
        if(AlreadySpawnedTransforms.Contains(randomTransform))
        {
            for(int i = 0; i < AlreadySpawnedTransforms.Count; i++)
            {
                randomTransform = LocationsTrasnforms[UnityEngine.Random.Range(0, LocationsTrasnforms.Count)];
                if (!AlreadySpawnedTransforms.Contains(randomTransform))
                {
                    Instantiate<GameObject>(randomGameObject, randomTransform);
                    AlreadySpawnedTransforms.Add(randomTransform);
                    break;
                }
            }
        }
        else if(!AlreadySpawnedTransforms.Contains(randomTransform))
        {
            Debug.Log($" randomTransform = {randomTransform.position.ToString()}");

            Instantiate<GameObject>(randomGameObject, randomTransform);
            Debug.Log($"{randomGameObject.name} spawned in {randomTransform.position.ToString()}" );
            AlreadySpawnedTransforms.Add(randomTransform);
        }


    }



    public void SpawnPowerUp(GameObject loot, Transform transform)
    {
        Instantiate(loot, transform);
    }

    public void SpawnSpecificPUInRAndomPlace(LootType typeOfLoot)
    {
        GameObject PowerUp = null;
        switch (typeOfLoot)
        {
            case LootType.healthPU:
                PowerUp = LootList[UnityEngine.Random.Range(0, LootList.Count)];
                break;
            case LootType.staminaPU:
                //SpawnPowerUp(healthPowerUp, LocationsTrasnforms[1]);
                break;
            case LootType.defensePU:
                //SpawnPowerUp(healthPowerUp, LocationsTrasnforms[2]);
                break;
            case LootType.targetCollectable:
                //SpawnPowerUp(TargetCollectable, LocationsTrasnforms[3]);
                break;
            default:
                break;
        }
        Transform randomTransform = LocationsTrasnforms[UnityEngine.Random.Range(0, LocationsTrasnforms.Count)];

        Instantiate(PowerUp, randomTransform);
    }

    public void AddPowerUp(GameObject thisPU)
    {
        //var spawner = PowerUpSpowner.ReturnPUSPowner();

        if (!LootList.Contains(thisPU))
        {
            Debug.Log($"{thisPU.name} added to LootList");
            //PowerUpSpowner.ReturnPUSPowner().LootList.Add(thisPU);
            LootList.Add(thisPU);
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
