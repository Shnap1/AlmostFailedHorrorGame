using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie_Patrolling_State : MonoBehaviour, IStateNew
{
    ZombieStateManager SM;
    int resetTime = 5;
    bool patroling = false;

    public void InitializeSM<T>(T stateManager) where T : IStateManagerNew
    {
        SM = stateManager as ZombieStateManager;
    }

    public void EnteState()
    {
        patroling = true;
        SM.anim.SetBool("closeToAttack", false);
        SM.anim.SetBool("seePlayer", false);
        Debug.Log("Patrolling");

        StartCoroutine(ResetingPath(resetTime));
        SM.healthBar.SetActive(false);
    }
    public void UpdaterState()
    {
        PatrolToSetPoint();
        CheckSwitchState();
    }

    public void ExitState()
    {
        patroling = false;
        StopCoroutine(ResetingPath(resetTime));

    }

    public void PatrolingFunction()
    {
        //StateManager.transform.LookAt()
        SearchWalkPoint();
        if (!SM.walkPointSet) SearchWalkPoint();

        else if (SM.walkPointSet)
            SM.agent.SetDestination(SM.walkPoint);

        Vector3 distanceToWalkPoint = SM.transform.position - SM.walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            SM.walkPointSet = false;
    }
    public void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-SM.walkPointRange, SM.walkPointRange);
        float randomX = Random.Range(-SM.walkPointRange, SM.walkPointRange);

        SM.walkPoint = new Vector3(SM.transform.position.x + randomX, SM.transform.position.y, SM.transform.position.z + randomZ);

        //Debug.Log($"Reseted walkPoint to: {SM.walkPoint} ");

        if (Physics.Raycast(SM.walkPoint, -SM.transform.up, 2f, SM.whatIsGround))
        {
            SM.walkPointSet = true;
        }
        else
        {
            SearchWalkPoint();
        }

    }

    void PatrolToSetPoint()
    {
        SM.agent.SetDestination(SM.walkPoint);
    }

    public void CheckSwitchState()
    {
        if (SM.playerInSightRange && !SM.playerInAttackRange)
        {
            SM.SwitchState(SM.Chasing);
        }
        else if (SM.playerInAttackRange && SM.playerInSightRange)
        {
            SM.SwitchState(SM.Attacking);

        }
    }

    IEnumerator ResetingPath(int patrolingTime)
    {
        while(patroling)
        {
            //Debug.Log("IEnumerator ResetingPath(int )");
            SearchWalkPoint();
            yield return new WaitForSeconds(patrolingTime);

        }
    }
}
