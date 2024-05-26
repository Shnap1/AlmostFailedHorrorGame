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
    public PatrolPointAI randomTriggeredPatrolPoint;
    void Start()
    {
        emptySpawnedPointsList = new List<PatrolPointAI>(patrolPointsList);
    }


    public Vector3 GetTriggeredPatrolPointPos()
    {
        Debug.Log("GetTriggeredPatrolPointPos");
        Vector3 point = Vector3.zero; // Assign a default value to 'point'

        switch (triggeredPatrolPointsList.Count)
        {
            case 0:
                point = GetEmptyRandomPointsToSpawn();
                Debug.Log($"point Vector3= {point} from GetEmptyRandomPointsToSpawn");
                break;
            case 1:
                point = triggeredPatrolPointsList[0].transform.position;
                break;
            default:
                randomTriggeredPatrolPoint = triggeredPatrolPointsList[Random.Range(0, triggeredPatrolPointsList.Count)];
                point = randomTriggeredPatrolPoint.transform.position;
                break;
        }
        Debug.Log($"point Vector3= {point}");
        return point;
    }

    public Vector3 GetEmptyPatrolPointPos()
    {
        var PatPointWithLeastEnemies = patrolPointsList.OrderBy(obj => obj.numberOfEnemies).First().transform.position;
        return PatPointWithLeastEnemies;
        // var PatPointWithLeastEnemies = patrolPointsList[Random.Range(0, patrolPointsList.Count)].transform.position;
    }

    public Vector3 GetEmptyRandomPointsToSpawn()
    {
        Debug.Log("GetEmptyRandomPointsToSpawn");
        if (emptySpawnedPointsList.Count == 0)
        {
            emptySpawnedPointsList = new List<PatrolPointAI>(patrolPointsList);
        }
        pointToSpawn = emptySpawnedPointsList[Random.Range(0, emptySpawnedPointsList.Count)];
        emptySpawnedPointsList.Remove(pointToSpawn);
        return pointToSpawn.transform.position;
    }
}
