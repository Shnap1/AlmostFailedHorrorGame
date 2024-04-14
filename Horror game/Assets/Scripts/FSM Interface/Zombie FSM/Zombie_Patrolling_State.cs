using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie_Patrolling_State : MonoBehaviour, IStateNew
{
    ZombieStateManager SM;
    int resetTime = 10;

    public bool patroling = false;

    public enum Patrollers{
        lastTriggeredPointFollower,
        playerFollower,
        randomPointFollower
    }

    public Patrollers thisEnemyPatrollerType;

    

    public Patrollers dropdownSelection;

    public void InitializeSM<T>(T stateManager) where T : IStateManagerNew
    {
        SM = stateManager as ZombieStateManager;
    }

    public void EnteState()
    {
        thisEnemyPatrollerType = Patrollers.playerFollower;
        //thisEnemyPatrollerType = Patrollers.randomPointFollower;


        patroling = true;
        SM.anim.SetBool("closeToAttack", false);
        SM.anim.SetBool("seePlayer", false);
        Debug.Log("Patrolling");

        StartCoroutine(ResetingPath(resetTime, thisEnemyPatrollerType));

        SM.healthBar.SetActive(false);
    }
    public void UpdaterState()
    {
       //PatrolToSetPoint();
        CheckSwitchState();
    }

    public void ExitState()
    {
        patroling = false;
        StopCoroutine(ResetingPath(resetTime, thisEnemyPatrollerType));
        //SM.agent.ResetPath();

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



    IEnumerator ResetingPath(int patrolingTime, Patrollers patrollerType)
    {
        while(patroling)
        {
            PatrolingFunction();

            /// NEW NEW
                 if(patrollerType == Patrollers.lastTriggeredPointFollower)
                {
                    //Debug.Log("lastTriggeredPointFollower");

                }
                else if(patrollerType == Patrollers.playerFollower)
                {
                    //Debug.Log("playerFollower");
                    PlayerPatrolingFunction();

                }
                else if(patrollerType == Patrollers.randomPointFollower)
                {
                    Debug.Log("randomPointFollower");
                    SearchWalkPoint();
                }
            
            /// NEW NEW
            yield return new WaitForSeconds(patrolingTime);
        }
    }

    // public void SelectPatrollingType(Patrollers patrollerType)
    // {
    //     if(patrollerType == Patrollers.lastTriggeredPointFollower)
    //     {
    //         //Debug.Log("lastTriggeredPointFollower");

    //     }
    //     else if(patrollerType == Patrollers.playerFollower)
    //     {
    //         //Debug.Log("playerFollower");
    //         PlayerPatrolingFunction();

    //     }
    //     else if(thisEnemyPatrollerType == Patrollers.randomPointFollower)
    //     {
    //         //Debug.Log("randomPointFollower");
    //     }
    // }


public void PlayerPatrolingFunction()
{
    //StateManager.transform.LookAt()
    SearchPlayerLocation();
    if (!SM.walkPointSet) SearchPlayerLocation();

    else if (SM.walkPointSet)
        SM.agent.SetDestination(SM.walkPoint);

    Vector3 distanceToWalkPoint = SM.transform.position - SM.walkPoint;

    //Walkpoint reached
    if (distanceToWalkPoint.magnitude < 3f)
        SM.walkPointSet = false;
}

private void SearchPlayerLocation()
{
    SM.walkPoint = SM.player.position; // Assuming player is accessible here
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
}
