using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) {
    }

    public override void EnterState() {
        //Debug.Log("Player Walk State");
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
    }

    public override void UpdateState(){
        Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x * Ctx.WalkMultiplier;
        Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y * Ctx.WalkMultiplier;
        CheckSwitchStates();
    }
    public override void ExitState() { }
    public override void InitializeSubState() { }

    public override void CheckSwitchStates() {
        if (!Ctx.IsMovementPressed){
            SwitchStates(Factory.Idle());
        } else if (Ctx.IsMovementPressed && Ctx.IsRunPressed) { 
            SwitchStates(Factory.Run());
        }
    }

}
