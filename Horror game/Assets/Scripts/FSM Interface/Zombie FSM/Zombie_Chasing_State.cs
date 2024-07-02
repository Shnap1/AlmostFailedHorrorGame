using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Chasing_State : MonoBehaviour, IStateNew
{
    ZombieStateManager SM;


    public void InitializeSM<T>(T stateManager) where T : IStateManagerNew
    {
        SM = stateManager as ZombieStateManager;
    }

    public void EnterState()
    {
        SM.anim.SetBool("closeToAttack", false);
        SM.anim.SetBool("seePlayer", true);
        // Debug.Log("Chasing");
        SM.agent.speed = SM.chaseSpeed;

    }
    public void UpdateState()
    {
        CheckSwitchState();
        //ChasePlayer();
    }

    public void ExitState()
    {
        SM.agent.speed = SM.speed;
    }

    private void ChasePlayer()
    {
        SM.agent.SetDestination(SM.player.position);
    }

    public void CheckSwitchState()
    {
        if (SM.playerInAttackRange && SM.playerInSightRange)
        {
            SM.SwitchState(SM.Attacking);
        }
        else if (!SM.playerInSightRange && !SM.playerInAttackRange)
        {
            SM.SwitchState(SM.Patrolling);
        }
        else
        {
            ChasePlayer();
        }
        //else
        //{
        //    ChasePlayer();
        //}

    }
}
