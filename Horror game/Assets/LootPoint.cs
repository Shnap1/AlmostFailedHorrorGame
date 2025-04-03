using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameData.instance.lootSpawner && gameObject.activeSelf)
        {

            GameData.instance.lootSpawner.LocationsTrasnforms.Add(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
