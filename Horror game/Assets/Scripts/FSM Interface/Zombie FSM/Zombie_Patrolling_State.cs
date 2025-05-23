using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Zombie_Patrolling_State : MonoBehaviour, IStateNew
{
    ZombieStateManager SM;
    int resetTime = 10;

    public bool patrolling = false;

    [HideInInspector]
    public enum Patrollers
    {
        lastTriggeredPointFollower,
        playerFollower,
        randomPointFollower
    }

    public static event Action<Patrollers> onPatrollingTypeSet;
    public Patrollers thisEnemyPatrollerType;
    // public Patrollers dropdownSelection;

    public void InitializeSM<T>(T stateManager) where T : IStateManagerNew
    {
        SM = stateManager as ZombieStateManager;
    }
    void OnEnable()
    {
        GameLoopManager.OnGameUpdate += FollowPlayerOnLootCollected;

    }

    void OnDisable()
    {
        GameLoopManager.OnGameUpdate += FollowPlayerOnLootCollected;
        StopAllCoroutines();
    }

    public void EnterState()
    {

        patrolling = true;
        SM.anim.SetBool("closeToAttack", false);
        SM.anim.SetBool("seePlayer", false);
        //Debug.Log("Patrolling");

        StartCoroutine(ResettingPath(resetTime, thisEnemyPatrollerType));

        //SM.healthBar.SetActive(false);
        onPatrollingTypeSet?.Invoke(thisEnemyPatrollerType);

    }
    public void UpdateState()
    {
        //PatrolToSetPoint();
        CheckSwitchState();

    }

    public void ExitState()
    {
        patrolling = false;
        StopCoroutine(ResettingPath(resetTime, thisEnemyPatrollerType));
        //SM.agent.ResetPath();

    }

    public void PatrollingFunction()
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
        float randomZ = UnityEngine.Random.Range(-SM.walkPointRange, SM.walkPointRange);
        float randomX = UnityEngine.Random.Range(-SM.walkPointRange, SM.walkPointRange);

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

    void FollowPlayerOnLootCollected(GameLoopManager.GameState gameState)
    {
        if (gameState == GameLoopManager.GameState.LootCollected) SetPatrollingType(Patrollers.playerFollower);
    }

    public void SetPatrollingType(Patrollers patrollerType)
    {

        if (SM == null) return; // Check if SM is null

        thisEnemyPatrollerType = patrollerType;
        StopCoroutine(ResettingPath(resetTime, thisEnemyPatrollerType));
        onPatrollingTypeSet?.Invoke(thisEnemyPatrollerType);
        StartCoroutine(ResettingPath(resetTime, thisEnemyPatrollerType));

    }
    IEnumerator ResettingPath(int patrollingTime, Patrollers patrollerType)
    {
        while (patrolling)
        {
            PPcheckerLOL();
            //PatrolingFunction(); NEW NEW

            /// 
            if (thisEnemyPatrollerType == Patrollers.lastTriggeredPointFollower)
            {
                //Debug.Log("lastTriggeredPointFollower");
                EnemyGoesTo(SM.patrolPointManager.GetTriggeredPatrolPointPos()); ;
            }
            else if (thisEnemyPatrollerType == Patrollers.playerFollower)
            {
                EnemyGoesTo(SearchPlayerLocation());

            }
            else if (thisEnemyPatrollerType == Patrollers.randomPointFollower)
            {
                //Debug.Log("randomPointFollower");
                //SearchWalkPoint();
                EnemyGoesTo(SM.patrolPointManager.GetEmptyRandomPointsToSpawn());
            }

            /// NEW NEW
            yield return new WaitForSeconds(patrollingTime);
            PPcheckerLOL();
        }
    }

    void PPcheckerLOL()
    {
        // thisEnemyPatrollerType = SM.patrollerType;

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

    public void EnemyGoesTo(Vector3 destination)
    {
        SM.walkPoint = destination;
        SM.agent.SetDestination(SM.walkPoint);
        SM.walkPointSet = true;

        Vector3 distanceToWalkPoint = SM.transform.position - SM.walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 3f)
            SM.walkPointSet = false;
    }

    private Vector3 SearchPlayerLocation()
    {
        return SM.player.position; // Assuming player is accessible here
    }


    public void CheckSwitchState()
    {
        if (SM.playerInSightRange && !SM.playerInAttackRange)
        {
            SM.Switch_IState(SM.Chasing);
        }
        else if (SM.playerInAttackRange && SM.playerInSightRange)
        {
            SM.Switch_IState(SM.Attacking);

        }
    }
}
