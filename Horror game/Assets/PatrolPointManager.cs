using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PatrolPointManager : MonoBehaviour
{
    public List<PatrolPointAI> patrolPointsList = new List<PatrolPointAI>();
    public List<PatrolPointAI> triggeredPatrolPointsList = new List<PatrolPointAI>();
    public List<PatrolPointAI> emptySpawnedPointsList = new List<PatrolPointAI>
    ();
    PatrolPointAI pointToSpawn;
    void Start()
    {
        emptySpawnedPointsList = patrolPointsList;
    }

    void Update()
    {

    }
    public Vector3 GetTriggeredPatrolPointPos()
    {
        return triggeredPatrolPointsList[Random.Range(0, triggeredPatrolPointsList.Count)].transform.position;
    }
    public Vector3 GetEmptyPatrolPointPos()
    {
        var PatPointWithLeastEnemies = patrolPointsList.OrderBy(obj => obj.numberOfEnemies).First().transform.position;
        return PatPointWithLeastEnemies;
        // var PatPointWithLeastEnemies = patrolPointsList[Random.Range(0, patrolPointsList.Count)].transform.position;
    }

    public Vector3 GetEmptyPointsToSpawn()
    {

        if (emptySpawnedPointsList.Count == 0)
        {
            emptySpawnedPointsList = patrolPointsList;
        }
        pointToSpawn = emptySpawnedPointsList[Random.Range(0, emptySpawnedPointsList.Count)];
        emptySpawnedPointsList.Remove(pointToSpawn);
        return pointToSpawn.transform.position;
    }
}
