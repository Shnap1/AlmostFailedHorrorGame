using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorBuildingSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> S_Placeholders = new List<GameObject>();
    public List<GameObject> M_Placeholders = new List<GameObject>();
    public List<GameObject> L_Placeholders = new List<GameObject>();

    public List<GameObject> S_Buildings = new List<GameObject>();
    public List<GameObject> M_Buildings = new List<GameObject>();
    public List<GameObject> L_Buildings = new List<GameObject>();
    void Start()
    {
        SpawnAllSectorBuildings();

    }

    void Update()
    {

    }
    public void SpawnAllSectorBuildings()
    {
        SpawnRandomBuildings(S_Placeholders, S_Buildings);
        SpawnRandomBuildings(M_Placeholders, M_Buildings);
        SpawnRandomBuildings(L_Placeholders, L_Buildings);
    }

    public void SpawnRandomBuildings(List<GameObject> placeholders, List<GameObject> buildings)
    {

        if (placeholders.Count > 0)
        {
            for (int i = 0; i < placeholders.Count; i++)
            {
                GameObject randomBuilding = buildings[Random.Range(0, buildings.Count)];
                Instantiate(randomBuilding, placeholders[i].transform.position, placeholders[i].transform.rotation);
                placeholders[i].gameObject.SetActive(false);
            }
        }

    }
}