using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GAMEFSM_Base_State : MonoBehaviour
{
    GameStateManager SM;

    public virtual void InitializeSM<T>(T stateManager) where T : IStateManagerNew
    {
        SM = stateManager as GameStateManager;
        Debug.Log("GAMEFSM InitializeSM");
    }
    public abstract void CheckSwitchState()
;

    public virtual void EnterState()
    {
        Debug.Log("GAMEFSM EnterState");

    }

    public virtual void ExitState()
    {
        Debug.Log("GAMEFSM EnterState");

    }

    public virtual void UpdateState()
    {
        CheckSwitchState();
    }
}
