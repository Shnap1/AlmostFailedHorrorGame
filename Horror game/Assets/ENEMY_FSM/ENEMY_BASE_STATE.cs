using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_BASE_STATE : BaseStateNEW
{
    //public StateManager EnemyController { get { return enemyController; } }

    public ENEMY_BASE_STATE (ENEMY_STATE_MANAGER controller) : base (controller)
    {
        //
    }
    public override void EnterState()
    {
        
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
    public override void CheckSwitchStates()
    {
       
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }
}
