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
        if (objectToSpawn == null && !setupEnded)
        {
            objectToSpawn = _hit.transform.gameObject;
            if (objectToSpawn != null) setupEnded = true;
        }
    }
    public override void GetHitPoint(Vector3 hitPoint)
    {
        _hitpoint = hitPoint;

        if (setupEnded)
        {
            UseAbility();
        }

    }

    public override void GetRaycastHit(RaycastHit hit)
    {
        _hit = hit;
    }

    public override void UseAbility()
    {
        Collider col = objectToSpawn.GetComponent<Collider>();
        float height = col.bounds.extents.y + spawnExtraOffsetY;
        Vector3 spawnPosition = _hitpoint + Vector3.up * height;

        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, transform.rotation);
    }
}
