using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSectors : MonoBehaviour, IStateNew
{
    GameStateManager SM;
    public void InitializeSM<T>(T stateManager) where T : IStateManagerNew
    {
        SM = stateManager as GameStateManager;
    }

    public void EnterState()
    {
    }

    public void ExitState()
    {
    }

    public void UpdateState()
    {
    }

    public void CheckSwitchState()
    {
    }

}
