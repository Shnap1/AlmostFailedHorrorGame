using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState, IRootState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base(currentContext, playerStateFactory){
        IsRootState = true;
        InitializeSubState();
        Debug.Log("PlayerGroundedState");
    }
    public void HandleGravity()
    {
        Ctx.CurrentMovementY = Ctx.GroundedGravity;
        Ctx.AppliedMovementY = Ctx.GroundedGravity;
    }

    public override void EnterState() {
        HandleGravity();
    }

    public override void UpdateState() { 
    CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void InitializeSubState() {
        if (!Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed) {
            SetSubState(Factory.Walk());
        }
        else {
            SetSubState(Factory.Run());
        }
    }

    public override void CheckSwitchStates(){
        //if player is grounded and jump is pressed,switch to jump state
        if (Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress)
        {
            SwitchStates(Factory.Jump());
        }
        //if player is not grounded and jump is not pressed, switch to fall state 
        else if (!Ctx.CharacterController.isGrounded)
        {
            SwitchStates(Factory.Fall());
        }

    }

}
