using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState, IRootState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        //Debug.Log("PlayerGroundedState");
    }
    public void HandleGravity()
    {
        InitializeSubState();
        Ctx.CurrentMovementY = Ctx.Gravity;
        Ctx.AppliedMovementY = Ctx.Gravity;
    }

    public override void EnterState()
    {
        HandleGravity();
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void InitializeSubState()
    {
        if (!Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed)
        {
            SetSubState(Factory.Walk());
        }
        else
        {
            SetSubState(Factory.Run());
        }
    }

    public override void CheckSwitchStates()
    {
        //if player is grounded and JUMP is pressed,switch to jump state
        if (Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress)
        {
            Ctx._currentJumpHeight = Ctx._maxJumpHight;
            Ctx._currentJumpTime = Ctx._maxJumpTime;
            Ctx.SetupJumpVariables();
            SwitchStates(Factory.Jump());
        }
        //if player is not grounded and jump is not pressed, switch to fall state 
        else if (!Ctx.CharacterController.isGrounded)
        {
            SwitchStates(Factory.Fall());
        }

        else if (Ctx.isJumpPadCollided) //JUMP PAD
        {

            Debug.Log("CheckSwitchStates() isJumpPadCollided");
            Ctx.isJumpPadCollided = false;

            Ctx._currentJumpHeight = Ctx._maxJumpPadHeight;
            Ctx._currentJumpTime = Ctx._maxJumpPadTime;
            Ctx.SetupJumpVariables();
            SwitchStates(Factory.Jump());
        }

    }

}
