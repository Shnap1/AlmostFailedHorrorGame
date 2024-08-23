using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> buildingsList = new List<GameObject>();
    public List<GameObject> foliageList = new List<GameObject>();
    public List<Transform> BuildingTrasnforms = new List<Transform>();
    public List<Transform> FreeBuildingTrasnforms = new List<Transform>();
    public List<Material> AllBuildingFacadeMaterials = new List<Material>();

    public List<Material> AllBuildingInsideMaterials = new List<Material>();

    public int amountOfBuildings;


    public static BuildingSpawner instance;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
    void Start()
    {
        FreeBuildingTrasnforms = new List<Transform>(BuildingTrasnforms);
        // if (amountOfBuildings >= 0) amountOfBuildings = 5;
        BuildingSpawn(amountOfBuildings);
    }

    public static void BuildingSpawn(int mountOfBuildings)
    {
        // Debug.Log($"PowerUpSpawn(amountOfPowerUps = {amountOfPowerUps}, LootType = {lootType})");
        for (int i = 0; i < mountOfBuildings; i++)
        {
            // instance.RandomSpawnPowerUp();
            // Debug.Log($"PowerUpSpawn() LootType - {lootType} i: ({i})");
            instance.RandomBuildingSpawn();
        }
    }

    void RandomBuildingSpawn()
    {

        GameObject buildingToSpawn = buildingsList[Random.Range(0, buildingsList.Count)];
        if (FreeBuildingTrasnforms.Count <= 0) return;
        Transform randomFreeTransform = FreeBuildingTrasnforms[Random.Range(0, FreeBuildingTrasnforms.Count)];
        var spawnedBuilding = Instantiate(buildingToSpawn, randomFreeTransform.position, randomFreeTransform.rotation);
        FreeBuildingTrasnforms.Remove(randomFreeTransform);
        // spawnedBuilding.GetComponent<MeshRenderer>().material = AllBuildingMaterials[Random.Range(0, AllBuildingMaterials.Count)];

    }

    public Material GetRandomBuildingFacadeMaterial()
    {
        return AllBuildingFacadeMaterials[Random.Range(0, AllBuildingFacadeMaterials.Count)];
    }

    public Material GetRandomBuildingInsideMaterial()
    {
        return AllBuildingInsideMaterials[Random.Range(0, AllBuildingInsideMaterials.Count)];
    }

}
