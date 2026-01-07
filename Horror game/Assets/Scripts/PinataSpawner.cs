using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinataSpawner : MonoBehaviour
{

    public List<GameObject> Loot = new List<GameObject>();
    public bool spawnedAll = false;
    [ContextMenu("Spawn Loot")]
    public void SpawnLoot()
    {
        if (!spawnedAll)
        {
            foreach (GameObject loot in Loot)
            {
                Instantiate(loot, transform.position, transform.rotation);
            }
            spawnedAll = true;
        }
    }

}
