using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using static PowerUpSpowner;

public class PowerUpSpowner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject healthPowerUp;
    //[SerializeField] public GameObject staminaPowerUp;
    public Dictionary<Loot, GameObject> LootDict = new Dictionary<Loot, GameObject>();
    public List<GameObject> LootList = new List<GameObject>();
    //public Dictionary<Locations, Transform> LocationsDict = new Dictionary<Locations, Transform>();
    public List<Transform> LocationsTrasnforms = new List<Transform>();

    public static PowerUpSpowner thisPUSpawner;
    public enum Loot
    {
        healthPU,
        staminaPU,
        defencePU
    }

    public enum Locations
    {
        gasStation,

    }

    void Start()
    {
        //var hp = healthPowerUp.GetComponent<IPowerUp>();
        //hp.DoAction(2);
        thisPUSpawner = gameObject.GetComponent<PowerUpSpowner>();
        //thisPUSpawner = this;
    }

    public static void SpawnPowerUp(Loot loot, Locations location)
    {
        var spawner = PowerUpSpowner.ReturnPUSPowner();
        //Instantiate<GameObject>(spawner.LootDict[loot], spawner.LocationsDict[location]);
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

    public static PowerUpSpowner ReturnPUSPowner()
    {
        return thisPUSpawner;
    }
}

public interface IPowerUp
{
    public void DoAction(int amount, GameObject gameObject);
} 