using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) {
    }

    public override void EnterState() {
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0; 
        //Debug.Log("Idle State");

    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() { }
    public override void InitializeSubState() { }
    public override void CheckSwitchStates() {
        if (Ctx.IsMovementPressed && Ctx.IsRunPressed) {
            SwitchStates(Factory.Run());
        } else if (Ctx.IsMovementPressed) {
            SwitchStates(Factory.Walk());
        }
    }

}
