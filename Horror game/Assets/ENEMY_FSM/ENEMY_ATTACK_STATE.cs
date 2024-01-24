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

    }

    public override void EnterState()
    {

    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        StateManager.AttackPlayer();
    }

    // Start is called before the first frame update

}
