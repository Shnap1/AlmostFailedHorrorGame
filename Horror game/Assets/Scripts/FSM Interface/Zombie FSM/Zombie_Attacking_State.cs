using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Attacking_State : MonoBehaviour, IStateNew
{
    ZombieStateManager SM;
    public static Action<bool> onAttack;

    public void InitializeSM<T>(T stateManager) where T : IStateManagerNew
    {
        SM = stateManager as ZombieStateManager;
    }

    public void EnterState()
    {
        SM.anim.SetBool("closeToAttack", true);
        SM.anim.SetBool("seePlayer", true);
        Debug.Log("Attacking");
        onAttack?.Invoke(true);
        SM.healthBar.SetActive(true);


    }
    public void UpdateState()
    {
        CheckSwitchState();
        //AttackPlayer();
    }

    public void ExitState()
    {
        SM.agent.isStopped = false;

        //onAttack?.Invoke(false);
    }



    public void AttackPlayer()
    {
        //SM.agent.SetDestination(transform.position);
        SM.agent.isStopped = true;

        transform.LookAt(SM.player);

        if (!SM.alreadyAttacked)
        {
            ///Attack code here
            //Rigidbody rb = Instantiate(SM.projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code
            onAttack?.Invoke(false);
            SM.alreadyAttacked = true;
            Invoke(nameof(ResetAttack), SM.timeBetweenAttacks);
        }
        onAttack?.Invoke(true);
    }
    private void ResetAttack()
    {
        SM.alreadyAttacked = false;
    }

    public void CheckSwitchState()
    {
        if (SM.playerInSightRange && !SM.playerInAttackRange)
        {
            SM.SwitchState(SM.Chasing);
        }
        else if (!SM.playerInSightRange && !SM.playerInAttackRange)
        {
            SM.SwitchState(SM.Patrolling);
        }
        //else
        //{
        //    AttackPlayer();
        //}

    }
}
