using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PatrolPointManager : MonoBehaviour
{
    public List<PatrolPointAI> patrolPointsList = new List<PatrolPointAI>();
    public List<PatrolPointAI> triggeredPatrolPointsList = new List<PatrolPointAI>();
    void Start()
    {
        
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
}
