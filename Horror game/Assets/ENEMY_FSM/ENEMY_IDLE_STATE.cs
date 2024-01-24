using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMY_IDLE_STATE : BaseStateNEW
{
    public ENEMY_IDLE_STATE(ENEMY_STATE_MANAGER sm) : base(sm)
    { }

    public override void CheckSwitchStates()
    {
        throw new System.NotImplementedException();
    }

    public override void EnterState()
    {
        Debug.Log(" ENEMY in IDLE state");
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        //
    }
}
