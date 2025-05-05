using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateManagerNew
{
    void InitializeStates();


    void Switch_IState(IStateNew state);
    void SwitchState(GAMEFSM_Base_State state);


}

public interface IStateNew
{
    void InitializeSM<T>(T stateManager) where T : IStateManagerNew;
    void EnterState();
    void UpdateState();
    void ExitState();
    void CheckSwitchState();
}