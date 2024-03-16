using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_CHASE_STATE : BaseStateNEW
{
    public ENEMY_CHASE_STATE(ENEMY_STATE_MANAGER sm) : base(sm)
    { }

    public override void CheckSwitchStates()
    {
        if(StateManager.playerInAttackRange && StateManager.playerInSightRange)
        {
            StateManager.SwitchStates(StateManager.Attack());
        }
        else if (!StateManager.playerInSightRange && !StateManager.playerInAttackRange)
        {
            StateManager.SwitchStates(StateManager.Patroling());
        }

    }

    public override void EnterState()
    {
        Debug.Log("CHASE state");
    }

    public override void ExitState()
    {
        //
    }

    public override void UpdateState()
    {
        ChasePlayer();
        CheckSwitchStates();
    }

    private void ChasePlayer()
    {
        StateManager.agent.SetDestination(StateManager.player.position);
    }
}
