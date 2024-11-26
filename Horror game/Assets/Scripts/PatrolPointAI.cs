using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointAI : MonoBehaviour
{
    public PatrolPointManager PatrolPointManager;
    public bool pointTriggeredByPlayer = false;
    public bool pointClosestToPlayer = false;
    public int numberOfEnemies = 0;
    void Start()
    {
        PatrolPointManager.patrolPointsList.Add(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pointTriggeredByPlayer = true;
            PatrolPointManager.triggeredPatrolPointsList.Add(this);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            //numberOfEnemies += 1;
            numberOfEnemies = Mathf.Clamp(numberOfEnemies + 1, 0, 5);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pointTriggeredByPlayer = false;
            PatrolPointManager.triggeredPatrolPointsList.Remove(this);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            numberOfEnemies = Mathf.Clamp(numberOfEnemies - 1, 0, 5);
        }
    }

    void OnDisable()
    {
        PatrolPointManager.patrolPointsList.Remove(this);
    }

}
