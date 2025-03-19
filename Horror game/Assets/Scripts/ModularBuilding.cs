using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModularBuilding : MonoBehaviour
{
    public GameObject Facade;
    public GameObject Inside;

    void Start()
    {
        if (Facade != null && Inside != null)
        {
            // Facade.GetComponent<Renderer>().material = BuildingSpawner.instance.GetRandomBuildingFacadeMaterial();
            // Inside.GetComponent<Renderer>().material = BuildingSpawner.instance.GetRandomBuildingInsideMaterial();
        }
    }
}
