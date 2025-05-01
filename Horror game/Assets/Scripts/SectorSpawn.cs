using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> sectorPlaceholder = new List<Transform>();
    public List<GameObject> sectors = new List<GameObject>();
    public bool finishedSpawning = false;
    void Start()
    {
        // SpawnRandomSectors();
        finishedSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool SpawnRandomSectors()
    {
        for (int i = 0; i < sectorPlaceholder.Count; i++)
        {
            GameObject randomSector = sectors[Random.Range(0, sectors.Count)];
            Instantiate(randomSector, sectorPlaceholder[i].position, sectorPlaceholder[i].rotation);
            sectorPlaceholder[i].gameObject.SetActive(false);
            if (i == sectorPlaceholder.Count)
            {
                finishedSpawning = true;
                return finishedSpawning;
            }
            else if (i >= sectorPlaceholder.Count)
            {
                finishedSpawning = false;
                return finishedSpawning;

            }
        }
        return false;
    }

    public void ReferenceSTate()
    {

    }
}
