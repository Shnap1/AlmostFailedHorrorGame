using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularBuilding : MonoBehaviour
{
    public GameObject Facade;
    public GameObject Inside;

    void Start()
    {
        Facade.GetComponent<Renderer>().material = BuildingSpawner.instance.GetRandomBuildingFacadeMaterial();
        Inside.GetComponent<Renderer>().material = BuildingSpawner.instance.GetRandomBuildingInsideMaterial();
        transform.Rotate(0, Random.Range(0, 360), 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
