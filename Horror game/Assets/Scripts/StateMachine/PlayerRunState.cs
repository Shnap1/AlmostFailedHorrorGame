using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }      

    public override void EnterState() {
        //Debug.Log("Run State");
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, true);
    }

    public override void UpdateState(){
        Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x * Ctx.RunMultiplier;
        Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y * Ctx.RunMultiplier;
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates() {
        if (!Ctx.IsMovementPressed)
        {
            SwitchStates(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed) {
            SwitchStates(Factory.Walk());
        }
    }

}
