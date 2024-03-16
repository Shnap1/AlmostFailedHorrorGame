using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_PATROLLING_STATE : BaseStateNEW
{
    public ENEMY_PATROLLING_STATE(ENEMY_STATE_MANAGER sm) : base(sm)
    {
    }

    public override void CheckSwitchStates()
    {
        if (StateManager.playerInSightRange && !StateManager.playerInAttackRange)
        {
            StateManager.SwitchStates(StateManager.Chase());
        }
        else if(StateManager.playerInAttackRange && StateManager.playerInSightRange)
        {
            StateManager.SwitchStates(StateManager.Attack());
        }
    }

    public override void EnterState()
    {
        Debug.Log("Patrolling state");
        PatrolingFunction();

    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        //PatrolingFunction();
        CheckSwitchStates();
    }

    public void PatrolingFunction()
    {
        //StateManager.transform.LookAt()
        SearchWalkPoint();
        if (!StateManager.walkPointSet) SearchWalkPoint();

        else if (StateManager.walkPointSet)
            StateManager.agent.SetDestination(StateManager.walkPoint);

        Vector3 distanceToWalkPoint = StateManager.transform.position - StateManager.walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            StateManager.walkPointSet = false;
    }
    public void SearchWalkPoint()
    {
        Debug.Log($"Reseted walkPoint to: {StateManager.walkPoint} ");
        //Calculate random point in range
        float randomZ = Random.Range(-StateManager.walkPointRange, StateManager.walkPointRange);
        float randomX = Random.Range(-StateManager.walkPointRange, StateManager.walkPointRange);

        StateManager.walkPoint = new Vector3(StateManager.transform.position.x + randomX, StateManager.transform.position.y, StateManager.transform.position.z + randomZ);

        if (Physics.Raycast(StateManager.walkPoint, -StateManager.transform.up, 2f, StateManager.whatIsGround))
            StateManager.walkPointSet = true;
    }

}
