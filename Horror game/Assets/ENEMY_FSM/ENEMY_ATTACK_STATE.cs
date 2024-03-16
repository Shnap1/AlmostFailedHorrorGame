using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_ATTACK_STATE : BaseStateNEW
{
    public ENEMY_ATTACK_STATE(ENEMY_STATE_MANAGER sm) : base(sm)
    {
    }

    public override void CheckSwitchStates()
    {
        if(StateManager.playerInSightRange && !StateManager.playerInAttackRange)
        {
            StateManager.SwitchStates(StateManager.Chase());
        }
        else if(!StateManager.playerInSightRange && !StateManager.playerInAttackRange)
        {
            StateManager.SwitchStates(StateManager.Patroling());
        }
    }

    public override void EnterState()
    {
        Debug.Log("ATTACK state");
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        //StateManager.AttackPlayer();
        AttackPlayer();
        CheckSwitchStates();

    }

    public void AttackPlayer()
    {
        //Make sure enemy doesn't move
        StateManager.agent.SetDestination(StateManager.transform.position);

        StateManager.transform.LookAt(StateManager.player);

        if (!StateManager.alreadyAttacked)
        {
            ///Attack code here  +++++ Added GameObject.Instantiate()  before Instantiate()
            //Rigidbody rb = GameObject.Instantiate(StateManager.projectile, StateManager.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            //rb.AddForce(StateManager.transform.forward * 32f, ForceMode.Impulse);
            //rb.AddForce(StateManager.transform.up * 8f, ForceMode.Impulse);
            /////End of attack code

            //StateManager.alreadyAttacked = true;
            ////Here Invoke() is only in MonoBehaviour so I put StateManager to perform that method 
            //StateManager.Invoke(nameof(ResetAttack),StateManager.timeBetweenAttacks);
            StateManager.AttackPlayer();
            
        }
    }
    private void ResetAttack()
    {
        StateManager.alreadyAttacked = false;
    }

}
