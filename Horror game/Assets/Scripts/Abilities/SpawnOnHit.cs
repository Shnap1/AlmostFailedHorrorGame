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

    //new
    public float cooldown = 2f;
    private bool isOnCooldown = false;
    private bool hasStoredObject => objectToSpawn != null;

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

        if (isOnCooldown) return;

        _hit = hit;


        // STEP 1: Store object if we don't have one yet
        if (!hasStoredObject)
        {
            objectToSpawn = hit.transform.gameObject;
            StartCoroutine(CooldownRoutine());
            Debug.Log("Stored object: " + objectToSpawn.name);
            return;
        }

        // STEP 2: Spawn stored object
        SpawnStoredObject();
        StartCoroutine(CooldownRoutine());

        // objectToSpawn = _hit.transform.gameObject;
        // UseAbility();

    }

    private void SpawnStoredObject()
    {
        Collider col = objectToSpawn.GetComponent<Collider>();
        float height = col.bounds.extents.y + spawnExtraOffsetY;
        Vector3 spawnPosition = _hit.point + Vector3.up * height;

        Instantiate(objectToSpawn, spawnPosition, transform.rotation);

        Debug.Log("Spawned object: " + objectToSpawn.name);

        // Optional: clear stored object so ability repeats
        objectToSpawn = null;
    }

    private IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
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
    }
}
