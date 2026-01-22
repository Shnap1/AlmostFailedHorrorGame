using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnOnHit : Ability
{
    public GameObject objectToSpawn;
    public float spawnExtraOffsetY = 0.5f;
    public Vector3 _hitpoint;

    public RaycastHit _hit;

    public override void Setup()
    {//todo figure out why _hit.transform returns null
     // if (_hit.transform.gameObject == null) return;
        if (objectToSpawn == null) return;

        if (objectToSpawn != null)
        {
            objectToSpawn = _hit.transform.gameObject;
            setupEnded = true;
        }
        TestAbility();



        // if (objectToSpawn == null && !setupEnded)
        // {
        //     if (_hit.transform == null) return;
        //     objectToSpawn = _hit.transform.gameObject;
        //     if (objectToSpawn != null) setupEnded = true;
        // }
    }

    public void TestAbility()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = _hitpoint;
        Debug.Log("SpawnOnHit");
    }
    public override void GetHitPoint(Vector3 hitPoint)
    {
        _hitpoint = hitPoint;
        TestAbility();

        // if (setupEnded)
        // {
        //     UseAbility();
        // }

    }

    public override void GetRaycastHit(RaycastHit hit)
    {
        // _hit = hit;
        // objectToSpawn = hit.transform.gameObject;
        TestAbility();

    }

    public override void UseAbility()
    {
        // if (objectToSpawn == null) return;
        // Collider col = objectToSpawn.GetComponent<Collider>();
        // float height = col.bounds.extents.y + spawnExtraOffsetY;
        // Vector3 spawnPosition = _hitpoint + Vector3.up * height;

        // GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, transform.rotation);
        TestAbility();
    }
}
