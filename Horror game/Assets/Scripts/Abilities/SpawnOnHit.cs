using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnOnHit : Ability
{
    public GameObject objectToSpawn;
    public float spawnExtraOffsetY = 1f;
    public Vector3 _hitpoint;

    public RaycastHit _hit;

    public override void Setup()
    {//todo figure out why _hit.transform returns null
     // if (_hit.transform.gameObject == null) return;

        // if (objectToSpawn == null) return;
        // if (objectToSpawn != null)
        // {
        //     objectToSpawn = _hit.transform.gameObject;
        //     setupEnded = true;
        // }
        // TestAbility();

        setupEnded = true;//


        // if (objectToSpawn == null && !setupEnded)
        // {
        //     if (_hit.transform == null) return;
        //     objectToSpawn = _hit.transform.gameObject;
        //     if (objectToSpawn != null) setupEnded = true;
        // }
    }


    public override void GetHitPoint(Vector3 hitPoint)
    {
        _hitpoint = hitPoint;//gets incorrect data that is 0
        // TestAbility();

        // if (setupEnded)
        // {
        //     UseAbility();
        // }

    }

    public void TestAbility()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = _hit.point;
        Debug.Log("SpawnOnHit");
    }

    public override void UseAbility(RaycastHit hit)
    {
        if (isInitialized == false) Initialize();
        if (setupEnded == false) Setup();
        _hit = hit;

        objectToSpawn = _hit.transform.gameObject;
        // TestAbility();
        UseAbility();

    }

    public override void UseAbility()
    {
        if (objectToSpawn != null)
        {
            Collider col = objectToSpawn.GetComponent<Collider>();
            float height = col.bounds.extents.y + spawnExtraOffsetY;//+ spawnExtraOffsetY
            Vector3 spawnPosition = _hit.point + Vector3.up * height;// +(Vector3.up * height)

            // GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, transform.rotation);
            GameObject spawnedObject = objectToSpawn != null ? Instantiate(objectToSpawn, spawnPosition, transform.rotation) : null;


        }

        // TestAbility();
    }
}
