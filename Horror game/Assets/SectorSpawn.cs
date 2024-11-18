using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> sectorPlaceholder = new List<Transform>();
    public List<GameObject> sectors = new List<GameObject>();
    void Start()
    {
        SpawnRandomSectors();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnRandomSectors()
    {
        for (int i = 0; i < sectorPlaceholder.Count; i++)
        {
            GameObject randomSector = sectors[Random.Range(0, sectors.Count)];
            Instantiate(randomSector, sectorPlaceholder[i].position, sectorPlaceholder[i].rotation);
            sectorPlaceholder[i].gameObject.SetActive(false);
        }
    }
}
