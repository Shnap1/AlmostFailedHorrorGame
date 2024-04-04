using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class PlayerFallSate : PlayerBaseState, IRootState
{
    public PlayerFallSate(PlayerStateMachine currentContext, 
    PlayerStateFactory playerStateFactory) 
    : base(currentContext, playerStateFactory) {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        Debug.Log("Fall State");
        //Ctx.Animator.SetBool(Ctx.IsFallingHash, true);
        //StartDelayedExecution();

        if (Ctx.wasGroundedBeforePause == false)
        {
            Debug.Log("Ctx.Animator.SetBool(Ctx.IsFallingHash, true);");
            Ctx.Animator.SetBool(Ctx.IsFallingHash, true);

        }
    }

    public void StartDelayedExecution()
    {
        Task.Delay(1).ContinueWith(_ =>
        {
            DelayedMethod();
        });
    }

    private void DelayedMethod()
    {
        Debug.Log("Delayed for 2 seconds");
        Ctx.Animator.SetBool(Ctx.IsFallingHash, true);
        //if (Ctx.CharacterController.isGrounded == false)
        //{
        //    Debug.Log("Ctx.Animator.SetBool(Ctx.IsFallingHash, true);");
        //    Ctx.Animator.SetBool(Ctx.IsFallingHash, true);

        //}

    }


    public override void UpdateState()
    {
        HandleGravity();
        CheckSwitchStates();
    }
    public override void ExitState(){
        Ctx.Animator.SetBool(Ctx.IsFallingHash, false);
    }

    public void HandleGravity()
    {
        float previousYVelocity = Ctx.CurrentMovementY;
        Ctx.CurrentMovementY = Ctx.CurrentMovementY + Ctx.Gravity * Time.deltaTime;
        Ctx.AppliedMovementY = Mathf.Max((previousYVelocity + Ctx.CurrentMovementY) * .5f, -20.0f);
    }
    public override void CheckSwitchStates()
    {
        if (Ctx.CharacterController.isGrounded)
        {
            SwitchStates(Factory.Grounded());
        }
    }




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

    public override string ToString()
    {
        return base.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
