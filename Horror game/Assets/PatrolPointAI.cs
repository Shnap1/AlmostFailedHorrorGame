using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointAI : MonoBehaviour
{
    public PatroolPointManager PatroolPointManager;
    public bool pointTriggeredByPlayer = false;
    public bool pointClosestToPlayer = false;
    public int numberOfEnemies = 0;
    void Start()
    {
        PatroolPointManager.PatrolPointsList.Add(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            pointTriggeredByPlayer = true;
            PatroolPointManager.triggeredPatrolPointsList.Add(this);
        }
        else if(other.gameObject.tag == "Enemy")
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
            PatroolPointManager.triggeredPatrolPointsList.Remove(this);
        }
        else if (other.gameObject.tag == "Enemy")
        {
            numberOfEnemies = Mathf.Clamp(numberOfEnemies -1, 0, 5);
        }
    }

    void OnDisable()
    {
        PatroolPointManager.PatrolPointsList.Remove(this);
    }

}
