using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour, IStateNew
{
    GameStateManager SM;

    public void InitializeSM<T>(T stateManager) where T : IStateManagerNew
    {
        SM = stateManager as GameStateManager;
    }
    public void CheckSwitchState()
    {
        throw new System.NotImplementedException();
    }

    public void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState()
    {
        throw new System.NotImplementedException();
    }

}
