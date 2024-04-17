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
        var PatPointWithLeastEnemes = patrolPointsList.OrderBy(obj => obj.numberOfEnemies).First().transform.position;
        return PatPointWithLeastEnemes;
        // var PatPointWithLeastEnemes = patrolPointsList[Random.Range(0, patrolPointsList.Count)].transform.position;
    }
}
