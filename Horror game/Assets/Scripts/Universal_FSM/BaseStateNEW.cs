using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateNEW
{
    // Start is called before the first frame update
    public ENEMY_STATE_MANAGER stateManager;
    public ENEMY_STATE_MANAGER StateManager { get{ return stateManager; } set { stateManager = value; } }

    public BaseStateNEW(ENEMY_STATE_MANAGER sm ) { 

        stateManager = sm;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
    public abstract void CheckSwitchStates();

    //public abstract void InitializeSubState();

}
